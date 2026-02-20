# HW1 -- God Object та принцип SRP

## 1. Що таке "God Object" анти-патерн?

God Object (Божественний об'єкт) --- це анти-патерн проєктування, коли
один клас бере на себе занадто багато відповідальностей.

### Основні характеристики:

-   Клас має велику кількість методів.
-   Клас контролює різні частини системи.
-   Має доступ до великої кількості даних.
-   Часто змінюється.
-   Важко тестується.
-   Порушує принцип єдиної відповідальності (SRP).

Такий клас стає центром всієї логіки системи, що ускладнює підтримку та
масштабування коду.

------------------------------------------------------------------------

## 2. Приклад класу, що порушує SRP

``` csharp
public class UserManager
{
    public void CreateUser(string name)
    {
        Console.WriteLine("User created");
    }

    public void SaveToDatabase()
    {
        Console.WriteLine("Saved to database");
    }

    public void SendEmail()
    {
        Console.WriteLine("Email sent");
    }

    public void GenerateReport()
    {
        Console.WriteLine("Report generated");
    }
}
```

### Чому це порушує SRP?

SRP (Single Responsibility Principle) говорить, що клас повинен мати
лише одну причину для змін.

У цьому прикладі клас: - створює користувача - працює з базою даних -
відправляє email - генерує звіт

Це різні відповідальності. Якщо зміниться логіка email або бази даних
--- доведеться змінювати один і той самий клас.

------------------------------------------------------------------------

## 3. Рефакторинг для дотримання SRP

``` csharp
public class UserService
{
    public void CreateUser(string name)
    {
        Console.WriteLine("User created");
    }
}

public class UserRepository
{
    public void Save()
    {
        Console.WriteLine("Saved to database");
    }
}

public class EmailService
{
    public void SendEmail()
    {
        Console.WriteLine("Email sent");
    }
}

public class ReportService
{
    public void GenerateReport()
    {
        Console.WriteLine("Report generated");
    }
}
```

Тепер кожен клас має одну відповідальність.

------------------------------------------------------------------------

## 4. Висновок

God Object ускладнює підтримку коду, тестування та масштабування.

Дотримання принципу SRP: - робить код зрозумілішим - полегшує
тестування - зменшує зв'язність - покращує архітектуру системи

Тому важливо уникати God Object та розділяти відповідальності між
класами.
