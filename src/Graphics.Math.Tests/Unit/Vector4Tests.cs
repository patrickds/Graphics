using Graphics.Extensions;
using System;
using NUnit.Framework;
using SharpTestsEx;

namespace Graphics.Math.Tests.Unit
{
    [TestFixture]
    public class Vector4Tests
    {
        [Test]
        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 1)]
        [TestCase(2, 0, 0, 2)]
        [TestCase(0, 0.707106, 0.707106, 1)]
        [TestCase(0.707106, 0.707106, 0, 1)]
        [TestCase(-0.707106, 0.707106, 0, 1)]
        [TestCase(0, -0.707106, 0.707106, 1)]
        [TestCase(0, -0.707106, -0.707106, 1)]
        [TestCase(-0.422618, -0.906307, 0, 1)]
        public void Magnitude_ValidVector_ReturnsValidMagnitude(double inputX, double inputY, double inputZ, double expected)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var result = vector.Magnitude;

            Assert.IsTrue(result.IsEqualsTo(expected));
        }
    }
}
