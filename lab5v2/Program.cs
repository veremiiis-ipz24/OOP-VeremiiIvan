using System;
using Domain;
using Common;
using Exceptions;

namespace Lab5v2
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- Курси ---
            var cSharp = new Course(1, "C# Fundamentals");
            var python = new Course(2, "Python for Data Science");

            // --- Репозиторій записів слухачів ---
            var repo = new Repository<Enrollment>();

            // --- Додавання слухачів ---
            try
            {
                repo.Add(new Enrollment(1, "Іван Іванов", 78, 1)); // C#
                repo.Add(new Enrollment(2, "Петро Петренко", 92, 1)); // C#
                repo.Add(new Enrollment(3, "Марія Коваль", 58, 1)); // C#
                repo.Add(new Enrollment(4, "Іван Іванов", 65, 2)); // Python

                // дубльований слухач (та ж людина, той самий курс)
                repo.Add(new Enrollment(5, "Іван Іванов", 80, 1));
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            // --- Вивід по курсу ---
            Console.WriteLine();
            PrintHeader($"Курс: {cSharp.Title}");
            var enrollmentsCSharp = repo.Where(e => e.CourseId == cSharp.Id);
            cSharp.Enrollments.AddRange(enrollmentsCSharp);
            PrintCourseStats(cSharp);

            Console.WriteLine();
            PrintHeader($"Курс: {python.Title}");
            var enrollmentsPython = repo.Where(e => e.CourseId == python.Id);
            python.Enrollments.AddRange(enrollmentsPython);
            PrintCourseStats(python);
        }

        static void PrintHeader(string text)
        {
            Console.WriteLine(new string('-', text.Length));
            Console.WriteLine(text);
            Console.WriteLine(new string('-', text.Length));
        }

        static void PrintCourseStats(Course course)
        {
            Console.WriteLine($"Кількість слухачів: {course.Enrollments.Count}");
            Console.WriteLine($"Середній бал: {course.GetAverageGrade():N2}");
            Console.WriteLine($"% зданих (>=60): {course.GetPassedPercent():N2}%");
        }
    }
}
