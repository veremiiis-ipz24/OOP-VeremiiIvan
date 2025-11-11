using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Course
    {
        public int Id { get; }
        public string Title { get; }
        public List<Enrollment> Enrollments { get; } = new();

        public Course(int id, string title)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        // Середній бал
        public double GetAverageGrade()
            => Enrollments.Any() ? Enrollments.Average(e => e.Grade) : 0.0;

        // Відсоток зданих
        public double GetPassedPercent()
        {
            if (!Enrollments.Any()) return 0;
            int passed = Enrollments.Count(e => e.Grade >= 60);
            return (double)passed / Enrollments.Count * 100.0;
        }
    }
}
