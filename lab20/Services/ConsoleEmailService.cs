using System;

public class ConsoleEmailService : IEmailService
{
    public void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"Sending email confirmation to {order.CustomerName}");
    }
}