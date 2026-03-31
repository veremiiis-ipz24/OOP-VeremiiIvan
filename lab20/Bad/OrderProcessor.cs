using System;

public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine("Starting order processing...");

        if (order.TotalAmount <= 0)
        {
            Console.WriteLine("Invalid order amount!");
            order.Status = OrderStatus.Cancelled;
            return;
        }

        Console.WriteLine("Order validated.");

        Console.WriteLine("Saving order to database...");
        Console.WriteLine($"INSERT INTO Orders VALUES ({order.Id}, '{order.CustomerName}', {order.TotalAmount})");

        Console.WriteLine($"Sending confirmation email to {order.CustomerName}...");

        order.Status = OrderStatus.Processed;

        Console.WriteLine("Order processed successfully.");
    }
}