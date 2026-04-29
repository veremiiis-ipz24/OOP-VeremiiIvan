# 📘 Code Smells та рефакторинг: практичний аналіз

## 📌 Опис

У цьому проєкті проведено аналіз якості коду з використанням підходів рефакторингу. Було виявлено кілька code smells (запахів коду) та запропоновано способи їх усунення відповідно до принципів чистого коду.

---

## 🎯 Мета

* Виявити проблемні місця в коді
* Покращити читабельність і підтримуваність
* Застосувати техніки рефакторингу на практиці

---

## 🔍 Виявлені Code Smells

### 1. ❗ Long Method

Проблема:
Метод виконує надто багато дій (валідація, обчислення, збереження).

До рефакторингу:

```
public void ProcessOrder(Order order)
{
    // валідація
    // обчислення
    // збереження
    // відправка повідомлення
}
```

Рішення: Extract Method

Після рефакторингу:

```
public void ProcessOrder(Order order)
{
    ValidateOrder(order);
    CalculateTotal(order);
    SaveOrder(order);
    SendConfirmation(order);
}
```

---

### 2. ❗ Duplicate Code

Проблема:
Однакова логіка розрахунку використовується в кількох методах.

До:

```
public decimal GetRetailPrice(decimal basePrice)
{
    return basePrice * 1.2m;
}

public decimal GetWholesalePrice(decimal basePrice)
{
    return basePrice * 1.2m * 0.9m;
}
```

Рішення: Extract Method

Після:

```
private decimal ApplyMarkup(decimal price)
{
    return price * 1.2m;
}

public decimal GetRetailPrice(decimal basePrice) => ApplyMarkup(basePrice);
public decimal GetWholesalePrice(decimal basePrice) => ApplyMarkup(basePrice) * 0.9m;
```

---

### 3. ❗ Magic Numbers

Проблема:
Незрозумілі числові значення в коді.

До:

```
if (user.Age > 18)
{
    discount = total * 0.15m;
}
```

Рішення: Extract Constant

Після:

```
private const int AdultAge = 18;
private const decimal VipDiscountRate = 0.15m;

if (user.Age > AdultAge)
{
    discount = total * VipDiscountRate;
}
```

---

## ⚠️ Чому рефакторинг без тестів — це ризик?

Рефакторинг змінює структуру коду, але не повинен змінювати поведінку. Без тестів немає гарантії, що:

* логіка залишилась правильною
* не з’явились нові баги
* старий функціонал працює як раніше

### 🔥 Приклад:

Було:
```
return price * 1.2m;
```

Після помилкового рефакторингу:
```
return price * 1.02m; // помилка!
```

Без тестів така помилка може залишитись непоміченою і призвести до фінансових втрат.

---

## 🛠 Використані техніки рефакторингу

* Extract Method
* Extract Constant
* Code Deduplication
* Покращення імен змінних

---

## 📊 Висновки

Рефакторинг дозволяє:

* зробити код зрозумілішим
* зменшити складність
* полегшити підтримку та розширення

Регулярне застосування рефакторингу разом із тестами значно підвищує якість програмного забезпечення.

---

## 📎 Примітка

Для аналізу використовувався власний навчальний проєкт. При рефакторингу дотримувались принципів чистого коду та рекомендацій із літератури.