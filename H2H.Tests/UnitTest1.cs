using FluentAssertions;
using Xunit;

namespace H2H.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sut = 5;
            var expected = 5;

            sut.Should().Be(expected);
        }
    }
}
