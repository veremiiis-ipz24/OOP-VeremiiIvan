using System;
using lab30v2;
using Xunit;

namespace lab30v2.Tests
{
    public class StringHelperTests
    {
        private readonly StringHelper _helper = new StringHelper();

        [Fact]
        public void Reverse_NormalString_ReturnsReversed()
        {
            var result = _helper.Reverse("hello");
            Assert.Equal("olleh", result);
        }

        [Fact]
        public void Reverse_EmptyString_ReturnsEmpty()
        {
            var result = _helper.Reverse("");
            Assert.Equal("", result);
        }

        [Fact]
        public void Reverse_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _helper.Reverse(null));
        }

        [Theory]
        [InlineData("madam", true)]
        [InlineData("racecar", true)]
        [InlineData("hello", false)]
        [InlineData("level", true)]
        public void IsPalindrome_CheckCases(string input, bool expected)
        {
            var result = _helper.IsPalindrome(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsPalindrome_WithSpacesAndCase_ReturnsTrue()
        {
            var result = _helper.IsPalindrome("A man a plan a canal Panama");
            Assert.True(result);
        }

        [Fact]
        public void IsPalindrome_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _helper.IsPalindrome(null));
        }

        [Theory]
        [InlineData("Hello world", 2)]
        [InlineData("one two three", 3)]
        [InlineData("single", 1)]
        public void WordCount_NormalCases(string input, int expected)
        {
            var result = _helper.WordCount(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WordCount_EmptyString_ReturnsZero()
        {
            var result = _helper.WordCount("");
            Assert.Equal(0, result);
        }

        [Fact]
        public void WordCount_Null_ReturnsZero()
        {
            var result = _helper.WordCount(null);
            Assert.Equal(0, result);
        }
    }
}
