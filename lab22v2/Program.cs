using System;

namespace Lab22
{
    // LSP violation
    class Bird
    {
        public virtual void Fly()
        {
            Console.WriteLine("Bird is flying");
        }
    }

    class Penguin : Bird
    {
        public override void Fly()
        {
            throw new NotImplementedException("Penguins cannot fly");
        }
    }

    // LSP compliant solution
    interface IBird { void Move(); }
    interface IFlyingBird { void Fly(); }

    class Sparrow : IBird, IFlyingBird
    {
        public void Move() => Console.WriteLine("Sparrow moves");
        public void Fly() => Console.WriteLine("Sparrow flies");
    }

    class RealPenguin : IBird
    {
        public void Move() => Console.WriteLine("Penguin swims");
    }

    class Program
    {
        static void MakeBirdFly(Bird bird)
        {
            bird.Fly();
        }

        static void TestFlying(IFlyingBird bird)
        {
            bird.Fly();
        }

        static void Main()
        {
            Console.WriteLine("LSP violation:");
            Bird b1 = new Bird();
            Bird b2 = new Penguin();

            MakeBirdFly(b1);
            try
            {
                MakeBirdFly(b2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\nLSP correct solution:");
            IFlyingBird sparrow = new Sparrow();
            TestFlying(sparrow);

            IBird penguin = new RealPenguin();
            penguin.Move();
        }
    }
}
