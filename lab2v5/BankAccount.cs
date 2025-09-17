using System;
using System.Collections.Generic;

public class BankAccount
{
    private decimal balance;
    private List<decimal> transactions = new List<decimal>();

    public decimal Balance
    {
        get { return balance; }
        set
        {
            if (value >= 0)
                balance = value;
            else
                Console.WriteLine("Баланс не може бути від’ємним!");
        }
    }

    // Конструктор
    public BankAccount(decimal initialBalance = 0)
    {
        Balance = initialBalance;
        transactions.Add(initialBalance);
    }

    // Індексатор для історії транзакцій
    public decimal this[int index]
    {
        get
        {
            if (index >= 0 && index < transactions.Count)
                return transactions[index];
            throw new IndexOutOfRangeException("Немає такої транзакції!");
        }
    }

    // Перевантаження оператора +
    public static BankAccount operator +(BankAccount acc, decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Сума повинна бути додатною!");
        acc.Balance += amount;
        acc.transactions.Add(amount);
        return acc;
    }

    // Перевантаження оператора -
    public static BankAccount operator -(BankAccount acc, decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Сума повинна бути додатною!");
        if (acc.Balance >= amount)
        {
            acc.Balance -= amount;
            acc.transactions.Add(-amount);
        }
        else
        {
            Console.WriteLine("Недостатньо коштів на рахунку!");
        }
        return acc;
    }
    public int TransactionCount
    {
        get { return transactions.Count; }
    }
}