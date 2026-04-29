# 📁 Лабораторна: Файловий I/O та серіалізація (JSON/XML)

## 📌 Мета

Освоїти:

* роботу з файлами в C#
* потоки (Stream)
* JSON та XML серіалізацію
* обробку помилок

---

# 📂 1. Робота з файлами

## 🔹 Базові операції (File)

Файловий I/O — це читання та запис даних у файл.

**Синхронне читання:**

```csharp
string content = File.ReadAllText("file.txt");
```

➡️ Блокує програму, поки файл не прочитається.

**Асинхронне читання:**

```csharp
string content = await File.ReadAllTextAsync("file.txt");
```

➡️ Не блокує виконання — краще для UI та великих файлів.

**Висновок:**
Синхронне — прості задачі.
Асинхронне — реальні додатки.

---

**Запис у файл:**

```csharp
File.WriteAllText("output.txt", data);
File.AppendAllText("log.txt", "log\n");
```

---

**Перевірка:**

```csharp
if (File.Exists("file.txt"))
{
    File.Delete("file.txt");
}
```

---

## 🔹 Потоки (Streams)

Потрібні для великих файлів.

```csharp
using (StreamReader reader = new StreamReader("large.txt"))
{
    string line;
    while ((line = await reader.ReadLineAsync()) != null)
    {
        // обробка
    }
}
```

➡️ Читає по частинах, не вантажить все в RAM.

**Чому using важливий:**

* закриває файл автоматично
* запобігає витокам пам’яті
* працює навіть при помилках

---

## 🔹 Директорії

```csharp
Directory.CreateDirectory("data");
var files = Directory.GetFiles("data");
```

---

# 🔄 2. JSON серіалізація

JSON — основний формат у вебі.

## 🔹 Клас

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
}
```

---

## 🔹 Серіалізація

```csharp
var product = new Product { Id = 1, Name = "Laptop", Price = 1200 };

string json = JsonSerializer.Serialize(product, new JsonSerializerOptions
{
    WriteIndented = true
});
```

➡️ JSON:

```json
{
  "Id": 1,
  "Name": "Laptop",
  "Price": 1200
}
```

---

## 🔹 Десеріалізація + обробка помилок

```csharp
try
{
    string json = File.ReadAllText("data.json");
    Product? product = JsonSerializer.Deserialize<Product>(json);

    if (product == null)
    {
        Console.WriteLine("Порожні дані");
    }
}
catch (JsonException)
{
    Console.WriteLine("Невалідний JSON");
}
catch (FileNotFoundException)
{
    Console.WriteLine("Файл не знайдено");
}
```

➡️ Важливо:

* перевірка на `null`
* обробка `JsonException`

---

## 🔹 Ігнорування полів

```csharp
public class Product
{
    public string Name { get; set; }

    [JsonIgnore]
    public string Secret { get; set; }
}
```

---

## 🔹 Висновок по JSON

* швидкий
* компактний
* стандарт для REST API

---

# 📄 3. XML серіалізація

XML — використовується в enterprise.

## 🔹 Клас

```csharp
[XmlRoot("Product")]
public class Product
{
    [XmlElement("ID")]
    public int Id { get; set; }

    [XmlElement("Name")]
    public string Name { get; set; }

    [XmlIgnore]
    public string Secret { get; set; }
}
```

---

## 🔹 Серіалізація

```csharp
XmlSerializer serializer = new XmlSerializer(typeof(Product));

using (StreamWriter writer = new StreamWriter("product.xml"))
{
    serializer.Serialize(writer, product);
}
```

---

## 🔹 Десеріалізація

```csharp
using (StreamReader reader = new StreamReader("product.xml"))
{
    Product p = (Product)serializer.Deserialize(reader);
}
```

---

## 🔹 Висновок по XML

* більший розмір
* повільніший
* зате є XSD (строга структура)

---

# ⚖️ 4. JSON vs XML (реально)

|              | JSON     | XML        |
| ------------ | -------- | ---------- |
| Розмір       | менший   | більший    |
| Швидкість    | швидше   | повільніше |
| Простота     | проста   | складніша  |
| Використання | веб, API | enterprise |

---

## 📌 Коли що використовувати

**JSON:**

* API
* фронтенд/бекенд
* мобільні додатки

**XML:**

* банки
* SOAP
* старі системи

---

# 🧠 Висновок

* File → для простих операцій
* Stream → для великих файлів
* JSON → основний формат зараз
* XML → потрібен для складних систем
* try-catch → обов’язково
