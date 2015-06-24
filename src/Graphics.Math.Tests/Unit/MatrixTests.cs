using NUnit.Framework;
using SharpTestsEx;

namespace Atlas.Math.Tests.Unit
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        [TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0)]
        [TestCase(0, 0, 0, 1, 1, 1, 1, 1, 1)]
        [TestCase(1, 1, 1, 0, 0, 0, 1, 1, 1)]
        [TestCase(1, 0, 0, 10, 0, 0, 11, 0, 0)]
        [TestCase(0, 1, 0, 0, 10, 0, 0, 11, 0)]
        [TestCase(0, 0, 1, 0, 0, 10, 0, 0, 11)]
        [TestCase(10, 5, 0, 1, 9, 10, 11, 14, 10)]
        [TestCase(-2, -25, 15, -4, 15, -1, -6, -10, 14)]
        public void CreateTranslation_ValidPositionTranslation_ReturnsTranslatedObject(double inputX, double inputY, double inputZ,
                                                                                       double offsetX, double offsetY, double offsetZ,
                                                                                       double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var offset = new Vector3(offsetX, offsetY, offsetZ);
            var matrix = Matrix4.CreateTranslation(offset);

            var result = matrix * vector;
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            result.Should().Be.EqualTo(expected);
        }

        [Test]
        public void CreateScale()
        {
            var vector = new Vector4(10, 0, 0, 1);
            var factor = new Vector3(2, 1, 1);
            var matrix = Matrix4.CreateScale(factor);

            var result = matrix * vector;
            var expected  = new Vector4(20, 0, 0, 1);

            result.Should().Be.EqualTo(expected);
        }
    }
}
