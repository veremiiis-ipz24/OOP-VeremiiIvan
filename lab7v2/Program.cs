using System;
using System.IO;
using System.Net.Http;
using System.Threading;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Лабораторна робота 7. Варіант 2: запис у файл та завантаження");
Console.WriteLine();

// Ініціалізація
var fileProcessor = new FileProcessor();
var networkClient = new NetworkClient();

// 1) Сценарій: запис у файл (перші 3 спроби кидають IOException, 4-я успіх)
try
{
    Console.WriteLine("=== Сценарій 1: запис у файл з тимчасовими помилками io ===");

    // Використовуємо overload для Action (void)
    RetryHelper.ExecuteWithRetry(
        operation: () => fileProcessor.WriteToFile("output/demo.txt", "Це тестовий вміст файлу."),
        retryCount: 4, // 3 невдачі + 1 успіх
        initialDelay: TimeSpan.FromMilliseconds(300),
        shouldRetry: ex => ex is IOException || ex is HttpRequestException
    );

    Console.WriteLine("Успішно записано файл");
}
catch (Exception ex)
{
    Console.WriteLine($"Кінцева помилка при записі файлу: {ex.GetType().Name} - {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Натисніть будь-яку клавішу для переходу до завантаження файлу...");
Console.ReadKey();
Console.WriteLine();

// 2) Сценарій: завантаження файлу (перші 2 спроби кидають HttpRequestException, 3-я успіх)
try
{
    Console.WriteLine("=== Сценарій 2: завантаження файлу з тимчасовими мережевими помилками ===");

    bool uploaded = RetryHelper.ExecuteWithRetry(
        operation: () => networkClient.UploadFile("https://upload.example.com/", "output/demo.txt"),
        retryCount: 3, // 2 невдачі + 1 успіх
        initialDelay: TimeSpan.FromMilliseconds(300),
        shouldRetry: ex => ex is IOException || ex is HttpRequestException
    );

    Console.WriteLine(uploaded ? "Файл успішно завантажено" : "Файл не завантажено");
}
catch (Exception ex)
{
    Console.WriteLine($"Кінцева помилка при завантаженні файлу: {ex.GetType().Name} - {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Програма завершена. Натисніть будь-яку клавішу для виходу...");
Console.ReadKey();


// ======================= Класи =======================

public class FileProcessor
{
    private int _writeAttempts = 0;

    // Метод: void WriteToFile(string path, string content)
    // Поводження: перші 3 виклики кидають IOException, потім успіх (запис у файл)
    public void WriteToFile(string path, string content)
    {
        _writeAttempts++;
        Console.WriteLine($"FileProcessor.WriteToFile. Виклик {_writeAttempts} для шляху \"{path}\"");

        if (_writeAttempts <= 3)
        {
            throw new IOException("Тимчасова помилка запису у файл");
        }

        // Створюємо директорію, якщо її немає
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, content);
    }
}

public class NetworkClient
{
    private int _uploadAttempts = 0;

    // Метод: bool UploadFile(string url, string filePath)
    // Поводження: перші 2 виклики кидають HttpRequestException, потім повертає true
    public bool UploadFile(string url, string filePath)
    {
        _uploadAttempts++;
        Console.WriteLine($"NetworkClient.UploadFile. Виклик {_uploadAttempts} для url \"{url}\" і файлу \"{filePath}\"");

        if (_uploadAttempts <= 2)
        {
            throw new HttpRequestException("Тимчасова мережева помилка при завантаженні файлу");
        }

        // Імітація успішного завантаження (в реальному коді тут був би HttpClient)
        return true;
    }
}

// ======================= RetryHelper =======================

public static class RetryHelper
{
    // Узагальнений метод для операцій, які повертають значення
    public static T ExecuteWithRetry<T>(
        Func<T> operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool>? shouldRetry = null)
    {
        if (operation == null)
            throw new ArgumentNullException(nameof(operation));

        if (retryCount < 1)
            throw new ArgumentOutOfRangeException(nameof(retryCount), "retryCount має бути не менше 1");

        if (initialDelay == default)
            initialDelay = TimeSpan.FromMilliseconds(500);

        Func<Exception, bool> retryPredicate = shouldRetry ?? (ex => true);

        int attempt = 0;

        while (true)
        {
            try
            {
                attempt++;
                Console.WriteLine();
                Console.WriteLine($"RetryHelper. Спроба {attempt}");

                T result = operation();

                Console.WriteLine($"RetryHelper. Успіх на спробі {attempt}");
                return result;
            }
            catch (Exception ex) when (attempt <= retryCount && retryPredicate(ex))
            {
                Console.WriteLine($"RetryHelper. Спроба {attempt} завершилась помилкою");
                Console.WriteLine($"  Тип: {ex.GetType().Name}");
                Console.WriteLine($"  Повідомлення: {ex.Message}");

                if (attempt == retryCount)
                {
                    Console.WriteLine("Досягнуто максимальну кількість спроб");
                    throw;
                }

                double multiplier = Math.Pow(2, attempt - 1);
                var delay = TimeSpan.FromMilliseconds(initialDelay.TotalMilliseconds * multiplier);

                Console.WriteLine($"Очікування {delay.TotalMilliseconds} мс перед наступною спробою");
                Thread.Sleep(delay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("RetryHelper. Виняток не підлягає повтору");
                Console.WriteLine($"  Тип: {ex.GetType().Name}");
                Console.WriteLine($"  Повідомлення: {ex.Message}");
                throw;
            }
        }
    }

    // Перевантаження для операцій void (Action)
    public static void ExecuteWithRetry(
        Action operation,
        int retryCount = 3,
        TimeSpan initialDelay = default,
        Func<Exception, bool>? shouldRetry = null)
    {
        if (operation == null)
            throw new ArgumentNullException(nameof(operation));

        // Використовуємо ту ж логіку, але для Action
        ExecuteWithRetry<object>(
            operation: () =>
            {
                operation();
                return null!;
            },
            retryCount: retryCount,
            initialDelay: initialDelay,
            shouldRetry: shouldRetry
        );
    }
}
