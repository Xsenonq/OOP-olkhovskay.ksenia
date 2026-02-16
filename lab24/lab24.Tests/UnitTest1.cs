using Xunit;
using lab24.Strategies;
using lab24.Core;

namespace lab24.Tests
{
    public class NumericProcessorTests
    {
        [Fact]
        public void SquareStrategy_ShouldReturnCorrectResult()
        {
            var processor = new NumericProcessor(new SquareOperationStrategy());

            var result = processor.Process(5);

            Assert.Equal(25, result);
        }

        [Fact]
        public void CubeStrategy_ShouldReturnCorrectResult()
        {
            var processor = new NumericProcessor(new CubeOperationStrategy());

            var result = processor.Process(3);

            Assert.Equal(27, result);
        }

        [Fact]
        public void SquareRootStrategy_ShouldReturnCorrectResult()
        {
            var processor = new NumericProcessor(new SquareRootOperationStrategy());

            var result = processor.Process(9);

            Assert.Equal(3, result);
        }
    }
}
