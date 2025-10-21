using System;

namespace lab4v2
{
    // Інтерфейс конвертера
    interface IConverter
    {
        double Convert(double amount); // Метод для конвертації
    }

    // Абстрактний клас із базовою логікою
    abstract class CurrencyConverter : IConverter
    {
        public double Rate { get; protected set; } // Курс валюти

        public abstract double Convert(double amount); // Абстрактний метод

        // Метод для обчислення середньої та підсумкової суми
        public void ProcessAmounts(double[] values)
        {
            double sum = 0;
            foreach (double val in values)
                sum += Convert(val);

            double average = sum / values.Length;
            Console.WriteLine($"Середня сума в гривнях: {average:F2}");
            Console.WriteLine($"Підсумкова сума в гривнях: {sum:F2}");
        }
    }

    // Реалізація: USD → UAH
    class UsdToUah : CurrencyConverter
    {
        public UsdToUah(double rate)
        {
            Rate = rate;
        }

        public override double Convert(double amount)
        {
            return amount * Rate;
        }
    }

    // Реалізація: EUR → UAH
    class EurToUah : CurrencyConverter
    {
        public EurToUah(double rate)
        {
            Rate = rate;
        }

        public override double Convert(double amount)
        {
            return amount * Rate;
        }
    }

    // Клас, що використовує композицію
    class ConverterProcessor
    {
        private CurrencyConverter converter; // Композиція

        public ConverterProcessor(CurrencyConverter converter)
        {
            this.converter = converter;
        }

        public void Execute(double[] amounts)
        {
            Console.WriteLine($"Конвертація {amounts.Length} значень:");
            converter.ProcessAmounts(amounts);
        }
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double[] usdValues = { 10, 25, 50 };
            double[] eurValues = { 5, 10, 20 };

            var usdConverter = new ConverterProcessor(new UsdToUah(41.75));
            var eurConverter = new ConverterProcessor(new EurToUah(48.50));

            Console.WriteLine("=== Конвертер USD → UAH ===");
            usdConverter.Execute(usdValues);

            Console.WriteLine("\n=== Конвертер EUR → UAH ===");
            eurConverter.Execute(eurValues);
        }
    }
}
