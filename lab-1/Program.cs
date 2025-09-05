using System;

namespace OOP_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Figure f = new Figure(30.3);
            Console.WriteLine(f.GetFigure());

            f.Area = 50.8;
            Console.WriteLine(f.GetFigure());
        }
    }
}