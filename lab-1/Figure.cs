using System;

namespace OOP_Demo
{
    public class Figure
    {
        private double area;

        public double Area
        {
            get { return area; }
            set
            {
                if (value >= 0)
                    area = value;
                else
                    throw new ArgumentException("Площа не є від’ємною");
            }
        }

        public Figure(double area)
        {
            this.Area = area;
            Console.WriteLine("Конструктор Figure викликаний");
        }

        ~Figure()
        {
            Console.WriteLine("Деструктор Figure викликаний");
        }

        public string GetFigure()
        {
            return $"Фігура має площу {Area}";
        }
    }
}
