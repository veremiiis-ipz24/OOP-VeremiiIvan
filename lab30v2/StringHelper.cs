using System;
using System.Linq;

namespace lab30v2
{
    public class StringHelper
    {
        public string Reverse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return new string(input.Reverse().ToArray());
        }

        public bool IsPalindrome(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            string normalized = new string(
                input.ToLower()
                     .Where(char.IsLetterOrDigit)
                     .ToArray());

            string reversed = new string(normalized.Reverse().ToArray());

            return normalized == reversed;
        }

        public int WordCount(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            return input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Length;
        }
    }
}
