using System;
using System.Collections.Generic;

namespace PatternsDemo
{
    // =========================
    // ===== COMPOSITE =========
    // =========================

    public interface IGraphic
    {
        void Draw();
    }

    public class Circle : IGraphic
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a Circle");
        }
    }

    public class Rectangle : IGraphic
    {
        public void Draw()
        {
            Console.WriteLine("Drawing a Rectangle");
        }
    }

    public class Group : IGraphic
    {
        private List<IGraphic> _graphics = new List<IGraphic>();

        public void Add(IGraphic graphic)
        {
            _graphics.Add(graphic);
        }

        public void Remove(IGraphic graphic)
        {
            _graphics.Remove(graphic);
        }

        public void Draw()
        {
            Console.WriteLine("Drawing a Group:");
            foreach (var graphic in _graphics)
            {
                graphic.Draw();
            }
        }
    }

    // =========================
    // ===== DECORATOR =========
    // =========================

    public interface ICoffee
    {
        string GetDescription();
        double GetCost();
    }

    public class SimpleCoffee : ICoffee
    {
        public string GetDescription()
        {
            return "Simple Coffee";
        }

        public double GetCost()
        {
            return 5.0;
        }
    }

    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public virtual string GetDescription()
        {
            return _coffee.GetDescription();
        }

        public virtual double GetCost()
        {
            return _coffee.GetCost();
        }
    }

    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", Milk";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 1.5;
        }
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", Sugar";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 0.5;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== COMPOSITE ===");

            IGraphic circle1 = new Circle();
            IGraphic rectangle1 = new Rectangle();

            Group group1 = new Group();
            group1.Add(circle1);
            group1.Add(rectangle1);

            IGraphic circle2 = new Circle();

            Group group2 = new Group();
            group2.Add(circle2);
            group2.Add(group1);

            group2.Draw();

            Console.WriteLine();
            Console.WriteLine("=== DECORATOR ===");

            ICoffee coffee = new SimpleCoffee();
            PrintCoffee(coffee);

            coffee = new MilkDecorator(coffee);
            PrintCoffee(coffee);

            coffee = new SugarDecorator(coffee);
            PrintCoffee(coffee);
        }

        static void PrintCoffee(ICoffee coffee)
        {
            Console.WriteLine($"{coffee.GetDescription()} - {coffee.GetCost():C}");
        }
    }
}
