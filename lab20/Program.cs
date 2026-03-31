using System;

class Program
{
    static void Main()
    {
        IOrderValidator validator = new OrderValidator();
        IOrderRepository repository = new InMemoryOrderRepository();
        IEmailService emailService = new ConsoleEmailService();

        OrderService service = new OrderService(validator, repository, emailService);

        Console.WriteLine("=== VALID ORDER ===");

        Order order1 = new Order(1, "Alice", 150);
        service.ProcessOrder(order1);

        Console.WriteLine();
        Console.WriteLine("=== INVALID ORDER ===");

        Order order2 = new Order(2, "Bob", -10);
        service.ProcessOrder(order2);
    }
}