using System;
using Xunit;

namespace TinyBlog.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void IsEqualTo()
        {
            Assert.Equal(1, 1);
        }
    }
}
