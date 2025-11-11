using System;

namespace Exceptions
{
    public class DuplicateEnrollmentException : Exception
    {
        public DuplicateEnrollmentException(string message) : base(message) { }
    }
}
