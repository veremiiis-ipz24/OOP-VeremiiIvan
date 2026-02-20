# HW3 -- Принципи ISP та DIP

## 1. Принцип ISP (Interface Segregation Principle)

ISP (Принцип розділення інтерфейсів) стверджує:

> Клієнти не повинні залежати від методів, які вони не використовують.

Це означає, що замість одного "великого" інтерфейсу потрібно створювати
кілька вузьких (спеціалізованих) інтерфейсів.

------------------------------------------------------------------------

## Приклад порушення ISP

``` csharp
public interface IWorker
{
    void Work();
    void Eat();
}

public class Robot : IWorker
{
    public void Work()
    {
        Console.WriteLine("Robot working");
    }

    public void Eat()
    {
        throw new NotImplementedException(); // Робот не їсть
    }
}
```

### Проблема:

Клас Robot змушений реалізовувати метод Eat(), який йому не потрібен. Це
порушує ISP.

------------------------------------------------------------------------

## Вирішення (дотримання ISP)

``` csharp
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public class Human : IWorkable, IEatable
{
    public void Work() => Console.WriteLine("Human working");
    public void Eat() => Console.WriteLine("Human eating");
}

public class Robot : IWorkable
{
    public void Work() => Console.WriteLine("Robot working");
}
```

Тепер кожен клас реалізує тільки потрібний інтерфейс.

------------------------------------------------------------------------

# 2. Принцип DIP (Dependency Inversion Principle)

DIP говорить:

> Модулі верхнього рівня не повинні залежати від модулів нижнього рівня.
> Обидва повинні залежати від абстракцій.

> Абстракції не повинні залежати від деталей. Деталі повинні залежати
> від абстракцій.

------------------------------------------------------------------------

## Приклад без DIP (поганий варіант)

``` csharp
public class EmailService
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

public class Notification
{
    private EmailService _emailService = new EmailService();

    public void Notify(string message)
    {
        _emailService.Send(message);
    }
}
```

### Проблема:

Notification жорстко залежить від EmailService. Неможливо легко замінити
реалізацію або протестувати клас.

------------------------------------------------------------------------

## Приклад з DIP + Dependency Injection

``` csharp
public interface IMessageService
{
    void Send(string message);
}

public class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

public class Notification
{
    private readonly IMessageService _messageService;

    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message)
    {
        _messageService.Send(message);
    }
}
```

Тепер залежність передається через конструктор (Dependency Injection).

------------------------------------------------------------------------

## Переваги застосування DIP

-   Зменшується зв'язність (loose coupling)
-   Легко замінювати реалізації
-   Спрощується тестування (можна передати mock-об'єкт)
-   Код стає гнучким і масштабованим

------------------------------------------------------------------------

# 3. Як ISP допомагає DI та тестуванню

"Вузькі" інтерфейси (ISP):

-   Спрощують створення mock-об'єктів
-   Зменшують кількість залежностей
-   Роблять код більш зрозумілим
-   Полегшують використання Dependency Injection

Якщо інтерфейс містить лише необхідні методи, тестові реалізації (mocks
або stubs) стають простішими та чистішими.

------------------------------------------------------------------------

# Висновок

ISP допомагає створювати вузькі, спеціалізовані інтерфейси. DIP дозволяє
залежати від абстракцій замість конкретних класів.

Разом ці принципи: - Покращують архітектуру системи - Зменшують
зв'язність - Спрощують тестування - Роблять код більш гнучким та
підтримуваним
