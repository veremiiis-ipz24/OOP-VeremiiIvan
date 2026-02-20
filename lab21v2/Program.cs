using System;

namespace lab21
{
    // Strategy interface
    public interface IShippingStrategy
    {
        decimal CalculateCost(decimal distance, decimal weight);
    }

    // Standard strategy
    public class StandardShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight)
        {
            return distance * 1.5m + weight * 0.5m;
        }
    }

    // Express strategy
    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight)
        {
            return (distance * 2.5m + weight * 1.0m) + 50m;
        }
    }

    // International strategy
    public class InternationalShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight)
        {
            decimal baseCost = distance * 5.0m + weight * 2.0m;
            return baseCost + baseCost * 0.15m;
        }
    }

    // New strategy (OCP extension)
    public class NightShippingStrategy : IShippingStrategy
    {
        public decimal CalculateCost(decimal distance, decimal weight)
        {
            decimal baseCost = distance * 1.5m + weight * 0.5m;
            return baseCost + 20m;
        }
    }

    // Factory
    public static class ShippingStrategyFactory
    {
        public static IShippingStrategy CreateStrategy(string type)
        {
            switch (type.ToLower())
            {
                case "standard": return new StandardShippingStrategy();
                case "express": return new ExpressShippingStrategy();
                case "international": return new InternationalShippingStrategy();
                case "night": return new NightShippingStrategy();
                default: throw new Exception("Unknown delivery type");
            }
        }
    }

    // Service uses abstraction only
    public class DeliveryService
    {
        public decimal CalculateDeliveryCost(decimal distance, decimal weight, IShippingStrategy strategy)
        {
            return strategy.CalculateCost(distance, weight);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type (Standard, Express, International, Night):");
            string type = Console.ReadLine();

            Console.WriteLine("Distance:");
            decimal distance = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Weight:");
            decimal weight = decimal.Parse(Console.ReadLine());

            IShippingStrategy strategy = ShippingStrategyFactory.CreateStrategy(type);

            DeliveryService service = new DeliveryService();
            decimal cost = service.CalculateDeliveryCost(distance, weight, strategy);

            Console.WriteLine("Cost: " + cost);
        }
    }
}
