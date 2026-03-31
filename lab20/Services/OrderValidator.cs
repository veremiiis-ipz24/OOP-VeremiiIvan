using System;

public class OrderValidator : IOrderValidator
{
    public bool IsValid(Order order)
    {
        Console.WriteLine("Validating order...");
        return order.TotalAmount > 0;
    }
}