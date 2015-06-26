using Graphics.Math;
using NUnit.Framework;
using SharpTestsEx;
using System;

namespace Graphics.Math.Tests.Unit
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
        [TestCase(0, 0, 0, -1, -1, -1, -1, -1, -1)]
        [TestCase(-2, -25, 15, -4, 15, -1, -6, -10, 14)]
        public void CreateTranslation_ValidPosition_ReturnsTranslatedPosition(double inputX, double inputY, double inputZ,
                                                                              double offsetX, double offsetY, double offsetZ,
                                                                              double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var offset = new Vector3(offsetX, offsetY, offsetZ);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var matrix = Matrix4.CreateTranslation(offset);
            var result = matrix * vector;

            result.Should().Be.EqualTo(expected);
        }

        [Test]
        [TestCase(0, 0, 0, 2, 1, 1, 0, 0, 0)]
        [TestCase(0, 2, 1, 2, 1, 1, 0, 2, 1)]
        [TestCase(0, 2, 2, 2, 1, 3, 0, 2, 6)]
        [TestCase(1, 0, 0, 2, 1, 1, 2, 0, 0)]
        [TestCase(2, 0, 0, -1, 1, 1, -2, 0, 0)]
        [TestCase(2, 0, 0, -2, 1, 1, -4, 0, 0)]
        [TestCase(10, 0, 0, 2, 1, 1, 20, 0, 0)]
        [TestCase(3, 0, -1, 1, 2, -1, 3, 0, 1)]
        [TestCase(12, -3, -8, 4, 3, 2, 48, -9, -16)]
        public void CreateScale_ValidPositionAndValidFactor_ReturnsScaledPosition(double inputX, double inputY, double inputZ,
                                                                                  double factorX, double factorY, double factorZ,
                                                                                  double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var factor = new Vector3(factorX, factorY, factorZ);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var matrix = Matrix4.CreateScale(factor);
            var result = matrix * vector;

            result.Should().Be.EqualTo(expected);
        }

        [Test]
        [TestCase(2, 0, 0, 0, 1, 1)]
        [TestCase(2, 0, 0, 1, 0, 1)]
        [TestCase(2, 0, 0, 1, 1, 0)]
        [TestCase(2, 2, 2, 2, 0, 1)]
        [TestCase(2, 2, 2, 0, 0, 0)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateScale_ValidPositionAndInvalidFactor_ThrowsException(double inputX, double inputY, double inputZ,
                                                                              double factorX, double factorY, double factorZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var factor = new Vector3(factorX, factorY, factorZ);
            var matrix = Matrix4.CreateScale(factor);
        }

        [Test]
        [TestCase(0, 1, 0, 90, 0, 0, 1)]
        public void CreateXRotation_ValiPositionAndValidRotation_ReturnsRotatedPosition(double inputX, double inputY, double inputZ, double degrees,
                                                                                        double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var radians = MathHelper.ToRadians(degrees);
            var rotation = Matrix4.CreateXRotation(radians);
            var result = rotation * vector;

            result.Should().Be.EqualTo(expected);
        }

        [Test]
        [TestCase(1, 0, 0, 90, 0, 0, 1)]
        public void CreateYRotation_ValiPositionAndValidRotation_ReturnsRotatedPosition(double inputX, double inputY, double inputZ, double degrees,
                                                                                        double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var radians = MathHelper.ToRadians(degrees);
            var rotation = Matrix4.CreateYRotation(radians);
            var result = rotation * vector;

            result.Should().Be.EqualTo(expected);
        }

        [Test]
        [TestCase(1, 0, 0, 90, 0, 1, 0)]
        [TestCase(0.707106, 0.707106, 0, 90, -0.707106, 0.707106, 0)]
        public void CreateZRotation_ValiPositionAndValidRotation_ReturnsRotatedPosition(double inputX, double inputY, double inputZ, double degrees,
                                                                                        double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var radians = MathHelper.ToRadians(degrees);
            var rotation = Matrix4.CreateZRotation(radians);
            var result = rotation * vector;

            result.Should().Be.EqualTo(expected);
        }
    }
}
