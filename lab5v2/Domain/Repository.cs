using System;
using System.Collections.Generic;
using System.Linq;
using Exceptions;
using Domain;

namespace Common
{
    public class Repository<T>
    {
        private readonly List<T> _items = new();

        public void Add(T item)
        {
            // спеціальна перевірка для Enrollment
            if (item is Enrollment e)
            {
                if (_items.OfType<Enrollment>()
                    .Any(x => x.StudentName == e.StudentName && x.CourseId == e.CourseId))
                {
                    throw new DuplicateEnrollmentException(
                        $"Слухач '{e.StudentName}' уже зареєстрований на курс (ID {e.CourseId}).");
                }
            }
            _items.Add(item);
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
            => _items.Where(predicate);

        public IReadOnlyList<T> All() => _items.AsReadOnly();
    }
}
