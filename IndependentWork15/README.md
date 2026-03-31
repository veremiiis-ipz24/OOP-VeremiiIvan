# Звіт з аналізу SOLID принципів (SRP, OCP) в Open-Source проєкті

## 1. Обраний проєкт

* **Назва:** Humanizer
* **Посилання на GitHub:** https://github.com/Humanizr/Humanizer

---

## 2. Аналіз SRP (Single Responsibility Principle)

### 2.1 Приклади дотримання SRP

#### Клас: `DefaultFormatter`

* **Відповідальність:** форматування чисел у рядок
* **Обґрунтування:** клас виконує тільки одну задачу — форматування

```csharp
public class DefaultFormatter : IFormatter
{
    public string Format(int number)
    {
        return number.ToString();
    }
}
```

---

#### Клас: `Pluralizer`

* **Відповідальність:** утворення множини слова
* **Обґрунтування:** клас займається лише логікою pluralization

```csharp
public class Pluralizer
{
    public string Pluralize(string word)
    {
        if (word.EndsWith("y"))
            return word.Substring(0, word.Length - 1) + "ies";

        return word + "s";
    }
}
```

---

### 2.2 Приклади порушення SRP

#### Клас: `Configurator`

* **Множинні відповідальності:**

  * налаштування форматування
  * локалізація
  * загальні параметри

```csharp
public class Configurator
{
    public void ConfigureFormatter(IFormatter formatter) { }

    public void ConfigureLocalization(string culture) { }

    public void ConfigureSettings() { }
}
```

* **Проблема:** клас виконує кілька різних задач
* **Наслідки:** складність підтримки, тестування та масштабування

---

## 3. Аналіз OCP (Open/Closed Principle)

### 3.1 Приклади дотримання OCP

#### Інтерфейс: `IFormatter`

* **Механізм розширення:** інтерфейс

```csharp
public interface IFormatter
{
    string Format(int number);
}
```

Розширення без зміни коду:

```csharp
public class RomanFormatter : IFormatter
{
    public string Format(int number)
    {
        return "Roman";
    }
}
```

* **Обґрунтування:** новий функціонал додається через новий клас

---

#### Strategy підхід

```csharp
public interface INumberStrategy
{
    string Convert(int number);
}
```

```csharp
public class DefaultStrategy : INumberStrategy
{
    public string Convert(int number)
    {
        return number.ToString();
    }
}
```

* **Обґрунтування:** можна додавати нові стратегії без змін існуючого коду

---

### 3.2 Приклади порушення OCP

#### Метод з switch

```csharp
public string FormatNumber(int number, string type)
{
    switch(type)
    {
        case "default":
            return number.ToString();
        case "roman":
            return "Roman";
        default:
            return number.ToString();
    }
}
```

* **Проблема:** для додавання нового типу треба змінювати код
* **Наслідки:** порушення OCP, складність розширення

---

## 4. Загальні висновки

У проєкті Humanizer більшість класів дотримуються принципу SRP — кожен клас має чітку і одну відповідальність. Це спрощує підтримку та тестування коду.

Також активно використовується OCP через інтерфейси та стратегії, що дозволяє розширювати функціональність без зміни існуючого коду.

Водночас у деяких місцях зустрічаються порушення, зокрема використання switch-конструкцій або об'єднання кількох відповідальностей в одному класі.

Загалом архітектура проєкту є якісною і добре відповідає принципам SOLID.
