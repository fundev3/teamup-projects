using System;
using Xunit;
using static test_pipeline.Program;

namespace TestCase
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Triangulo triangulo = new Triangulo();
            var result = triangulo.CalcularArea(5, 8);
            Assert.Equal(20, result);
        }
    }
}
