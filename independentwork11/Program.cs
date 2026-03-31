using System;
using System.Net.Http;
using System.Threading;
using Polly;
using Polly.Timeout;

class Program
{
    static int apiAttempts = 0;
    static int dbAttempts = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("=== IndependentWork11 ===\n");

        Scenario1_RetryApi();
        Scenario2_CircuitBreakerDatabase();
        Scenario3_TimeoutOperation();

        Console.WriteLine("\n=== End ===");
    }

    /*
    ============================================================
    СЦЕНАРІЙ 1: Виклик зовнішнього API
    ============================================================

    Проблема:
    Зовнішній API може тимчасово повертати помилки.

    Рішення:
    Використати Retry policy — повторні спроби виклику.

    Очікувана поведінка:
    Перші два виклики падають, третій — успішний.
    */

    static void Scenario1_RetryApi()
    {
        Console.WriteLine("---- Scenario 1: Retry API ----");

        var retryPolicy = Policy
            .Handle<HttpRequestException>()
            .WaitAndRetry(
                3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine(
                        $"Retry {retryCount} через {timeSpan.TotalSeconds}s: {exception.Message}");
                });

        try
        {
            string result = retryPolicy.Execute(() => CallExternalApi());
            Console.WriteLine($"Result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed: {ex.Message}");
        }

        Console.WriteLine();
    }

    static string CallExternalApi()
    {
        apiAttempts++;

        Console.WriteLine($"API attempt {apiAttempts}");

        if (apiAttempts <= 2)
            throw new HttpRequestException("Temporary API error");

        Console.WriteLine("API success");

        return "API DATA";
    }

    /*
    ============================================================
    СЦЕНАРІЙ 2: Доступ до бази даних
    ============================================================

    Проблема:
    База даних може бути тимчасово недоступною.

    Рішення:
    Використати Circuit Breaker.

    Якщо виникає кілька помилок підряд,
    подальші виклики тимчасово блокуються.
    */

    static void Scenario2_CircuitBreakerDatabase()
    {
        Console.WriteLine("---- Scenario 2: Circuit Breaker ----");

        var breakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreaker(
                2,
                TimeSpan.FromSeconds(5),
                onBreak: (ex, time) =>
                {
                    Console.WriteLine($"Circuit OPEN for {time.TotalSeconds}s");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit CLOSED");
                });

        for (int i = 0; i < 5; i++)
        {
            try
            {
                breakerPolicy.Execute(() => QueryDatabase());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }

    static void QueryDatabase()
    {
        dbAttempts++;

        Console.WriteLine($"DB attempt {dbAttempts}");

        if (dbAttempts <= 3)
            throw new Exception("Database connection failed");

        Console.WriteLine("Database success");
    }

    /*
    ============================================================
    СЦЕНАРІЙ 3: Довга операція
    ============================================================

    Проблема:
    Деякі операції можуть виконуватись занадто довго.

    Рішення:
    Використати Timeout policy.
    */

    static void Scenario3_TimeoutOperation()
    {
        Console.WriteLine("---- Scenario 3: Timeout ----");

        var timeoutPolicy = Policy.Timeout(
            TimeSpan.FromSeconds(2),
            TimeoutStrategy.Pessimistic,
            onTimeout: (context, time, task) =>
            {
                Console.WriteLine($"Timeout after {time.TotalSeconds}s");
            });

        try
        {
            timeoutPolicy.Execute(() => LongOperation());
        }
        catch (TimeoutRejectedException)
        {
            Console.WriteLine("Operation cancelled");
        }

        Console.WriteLine();
    }

    static void LongOperation()
    {
        Console.WriteLine("Long operation started");

        Thread.Sleep(4000);

        Console.WriteLine("Long operation finished");
    }
}