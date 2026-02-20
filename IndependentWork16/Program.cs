using System;

namespace IndependentWork16
{
    class ProductManager
    {
        public void AddProduct(string name, decimal price, int stock)
        {
            Validate(name, price);
            Save(name, price, stock);
            Log("Product added");
            Notify(stock);
        }

        private void Validate(string name, decimal price) => Console.WriteLine("Validating...");
        private void Save(string name, decimal price, int stock) => Console.WriteLine("Saving to DB...");
        private void Log(string msg) => Console.WriteLine($"LOG: {msg}");
        private void Notify(int stock)
        {
            if (stock < 5) Console.WriteLine("Low stock notification");
        }
    }

    public interface IProductValidator { bool Validate(string name, decimal price); }
    public interface IProductRepository { void Save(string name, decimal price, int stock); }
    public interface ILogger { void Log(string message); }
    public interface IStockNotifier { void Notify(int stock); }

    class ProductValidator : IProductValidator
    {
        public bool Validate(string name, decimal price)
        {
            Console.WriteLine("Validating...");
            return !string.IsNullOrEmpty(name) && price > 0;
        }
    }

    class ProductRepository : IProductRepository
    {
        public void Save(string name, decimal price, int stock)
        {
            Console.WriteLine("Saved to DB");
        }
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"LOG: {message}");
    }

    class StockNotifier : IStockNotifier
    {
        public void Notify(int stock)
        {
            if (stock < 5) Console.WriteLine("Low stock notification");
        }
    }

    class ProductService
    {
        private readonly IProductValidator _validator;
        private readonly IProductRepository _repository;
        private readonly ILogger _logger;
        private readonly IStockNotifier _notifier;

        public ProductService(IProductValidator v, IProductRepository r, ILogger l, IStockNotifier n)
        {
            _validator = v;
            _repository = r;
            _logger = l;
            _notifier = n;
        }

        public void AddProduct(string name, decimal price, int stock)
        {
            if (!_validator.Validate(name, price))
            {
                _logger.Log("Validation failed");
                return;
            }

            _repository.Save(name, price, stock);
            _logger.Log("Product added");
            _notifier.Notify(stock);
        }
    }

    class Program
    {
        static void Main()
        {
            var service = new ProductService(
                new ProductValidator(),
                new ProductRepository(),
                new ConsoleLogger(),
                new StockNotifier());

            service.AddProduct("Laptop", 1200, 3);
        }
    }
}
