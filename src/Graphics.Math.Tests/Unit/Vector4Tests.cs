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

            result.IsEqualsTo(expected).Should().Be.True();
        }

        [Test]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(10, 8, 15)]
        [TestCase(-5, 3, 0.5)]
        [TestCase(1.1, -5, 12)]
        [TestCase(0.7, 0.7, 0.7)]
        [TestCase(-51, 33, -1.5)]
        [TestCase(-511, 3653, -1.25)]
        public void Normalize_ValidVector_ReturnsNormalizedVector(double x, double y, double z)
        {
            var vector = new Vector4(x, y, z, 1);

            var normalized = vector.Normalize();
            normalized.Magnitude.IsEqualsTo(1).Should().Be.True();
        }
    }
}
