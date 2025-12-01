using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6_v2
{
    // ===== Власний делегат: приклад базового делегата =====
    // Делегат приймає студента і повертає рядок (демонстрація власного делегата)
    public delegate string StudentFormatter(Student s);

    public class Student
    {
        public string Name { get; }
        public int Grade { get; private set; } // оцінка (0-100)
        public string Group { get; }

        public Student(string name, int grade, string group)
        {
            Name = name;
            Grade = grade;
            Group = group;
        }

        public override string ToString() => $"{Name} ({Group}) : {Grade}";
    }

    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var students = new List<Student>
            {
                new Student("Іваненко Іван", 92, "А-1"),
                new Student("Петренко Петро", 78, "А-1"),
                new Student("Сидоренко Олена", 85, "Б-2"),
                new Student("Коваль Андрій", 67, "Б-2"),
                new Student("Ткачук Марія", 88, "А-1"),
            };

            Console.WriteLine("=== Усі студенти ===");
            students.ForEach(s => Console.WriteLine(s));

            // ===========================
            // Predicate<Student> - вибір студентів з балом > 80
            // ===========================
            // Predicate<T> — булева перевірка
            Predicate<Student> hasHighGrade = s => s.Grade > 80;

            var topStudents = students.Where(s => hasHighGrade(s)).ToList();

            Console.WriteLine("\n=== Студенти з оцінкою > 80 (Predicate) ===");
            topStudents.ForEach(s => Console.WriteLine(s));

            // ===========================
            // Func<Student, string> - текстовий звіт
            // ===========================
            // Func<T, TResult> — метод із результатом
            Func<Student, string> reportLine = s => $"{s.Name} | {s.Group} | Grade: {s.Grade}";

            Console.WriteLine("\n=== Текстовий звіт (Func) ===");
            var reports = topStudents.Select(reportLine).ToList();
            reports.ForEach(r => Console.WriteLine(r));

            // ===========================
            // Власний делегат + анонімний метод + лямбда
            // ===========================
            StudentFormatter fmtAnonymous = delegate (Student s) // анонімний метод
            {
                return $"{s.Name} - {s.Grade}";
            };

            StudentFormatter fmtLambda = s => $"{s.Name.ToUpper()} => {s.Grade}"; // лямбда

            Console.WriteLine("\n=== Власний делегат: анонімний метод ===");
            Console.WriteLine(fmtAnonymous(students[0]));

            Console.WriteLine("\n=== Власний делегат: лямбда ===");
            Console.WriteLine(fmtLambda(students[2]));

            // ===========================
            // Action<string> — приклад дії (вивід)
            // ===========================
            Action<string> print = s => Console.WriteLine(s);
            print("\n=== Приклад Action (друк рядка) ===");

            // ===========================
            // LINQ: OrderBy, Average, Aggregate
            // ===========================
            Console.WriteLine("\n=== LINQ: Сортування, середній бал, сумарний бал ===");

            var ordered = students.OrderByDescending(s => s.Grade).ToList();
            Console.WriteLine("Сортування за балом (спадання):");
            ordered.ForEach(s => Console.WriteLine(reportLine(s)));

            var avg = students.Average(s => s.Grade);
            Console.WriteLine($"\nСередній бал групи: {avg:F2}");

            var total = students.Select(s => s.Grade).Aggregate(0, (acc, g) => acc + g);
            Console.WriteLine($"Сумарний бал: {total}");

            // ===========================
            // Додатково: знайти студента з мінімальним балом (лінк + лямбда)
            // ===========================
            var weakest = students.MinBy(s => s.Grade);
            Console.WriteLine($"\nСтудент з мінімальним балом: {weakest}");

            // Кінець
            Console.WriteLine("\n=== Кінець ===");
        }
    }
}
