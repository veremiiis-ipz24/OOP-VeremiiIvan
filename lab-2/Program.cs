using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        BankAccount account = new BankAccount(1000);

        Console.WriteLine($"Початковий баланс: {account.Balance}");

        // Поповнення
        account = account + 500;
        Console.WriteLine($"Після поповнення: {account.Balance}");

        // Зняття
        account = account - 300;
        Console.WriteLine($"Після зняття: {account.Balance}");

        // Спроба зняти більше, ніж є
        account = account - 1500;

        // Використання індексатора (історія транзакцій)
        Console.WriteLine("Історія транзакцій:");
        for (int i = 0; i < account.TransactionCount; i++)
        {
            Console.WriteLine($"{i + 1}: {account[i]}");
        }

    }
}