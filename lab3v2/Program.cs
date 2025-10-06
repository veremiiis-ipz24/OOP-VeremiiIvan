using System;
using System.Collections.Generic;
using System.Text;

// Абстрактний базовий клас транспорту
abstract class Transport
{
    protected string Name; // Назва транспорту

    public Transport(string name)
    {
        Name = name;
        Console.WriteLine($"[+] Створено транспорт: {Name}");
    }

    // Абстрактний метод, який реалізується у похідних класах
    public abstract double Move(double distance);

    // Віртуальний метод, який можна перевизначити у похідних класах
    public virtual void Info()
    {
        Console.WriteLine($"Тип транспорту: {Name}");
    }

    // Деструктор — демонструє знищення об’єкта
    ~Transport()
    {
        Console.WriteLine($"[-] Знищено транспорт: {Name}");
    }
}

// Клас автомобіля — похідний від Transport
class Car : Transport
{
    private double FuelConsumptionPer100Km; // Витрати пального на 100 км

    // Конструктор із викликом базового конструктора
    public Car(string name, double consumption) : base(name)
    {
        FuelConsumptionPer100Km = consumption;
    }

    // Перевизначення абстрактного методу Move()
    public override double Move(double distance)
    {
        double fuelUsed = (FuelConsumptionPer100Km / 100) * distance;
        Console.WriteLine($"{Name} проїхав {distance} км, витративши {fuelUsed:F2} л пального.");
        return fuelUsed;
    }

    // Перевизначення віртуального методу Info()
    public override void Info()
    {
        Console.WriteLine($"{Name} — автомобіль з витратою {FuelConsumptionPer100Km} л/100 км.");
    }
}

// Клас велосипеда — похідний від Transport
class Bike : Transport
{
    // Конструктор із викликом базового
    public Bike(string name) : base(name) { }

    // Реалізація абстрактного методу Move()
    public override double Move(double distance)
    {
        Console.WriteLine($"{Name} проїхав {distance} км без витрат пального (людська сила).");
        return 0;
    }

    // Перевизначення віртуального методу Info()
    public override void Info()
    {
        Console.WriteLine($"{Name} — велосипед, не потребує пального.");
    }
}

// Основний клас програми
class Program
{
    static void Main()
    {
        // Увімкнення правильного відображення українських символів
        Console.OutputEncoding = Encoding.UTF8;

        // Колекція транспортних засобів (демонстрація поліморфізму)
        List<Transport> transports = new List<Transport>
        {
            new Car("Toyota Corolla", 7.5),
            new Bike("Trek FX 3")
        };

        double totalFuel = 0;
        double distance = 100;

        Console.WriteLine("\n=== Розрахунок витрат пального на 100 км ===\n");

        // Виклик методів для кожного елемента списку
        foreach (Transport t in transports)
        {
            t.Info();
            totalFuel += t.Move(distance);
            Console.WriteLine();
        }

        Console.WriteLine($"Загальні витрати пального: {totalFuel:F2} л\n");
        Console.WriteLine("=== Кінець виконання програми ===");
    }
}
