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

        //TODO:check negative axis stuff
        [Test]
        [TestCase(0, 1, 0, -1, 0, 0, 90, 0, 0, 1)]
        [TestCase(1, 0, 0, 0, 0, -1, 90, 0, 1, 0)]
        public void CreateRotation_ValidPositionAndValidRotation_ReturnsRotatedPosition(double inputX, double inputY, double inputZ,
                                                                                        double axisX, double axisY, double axisZ, double degrees,
                                                                                        double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var axis = new Vector3(axisX, axisY, axisZ);
            var radians = MathHelper.ToRadians(degrees);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var matrix = Matrix4.CreateRotation(axis, radians);
            var result = matrix * vector;

            result.Should().Be.EqualTo(expected);
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

        [Test]
        [TestCase(1, 0, 0, 2, 0, 0, 2, 1, 1, 6, 0, 0)]
        public void TransformationTranslateAndScale_ValidPosition_ReturnsTranslatedAndScaledPosition(double inputX, double inputY, double inputZ,
                                                                                                     double translateX, double translateY, double translateZ,
                                                                                                     double scaleX, double scaleY, double scaleZ,
                                                                                                     double expectedX, double expectedY, double expectedZ)
        {
            var vector = new Vector4(inputX, inputY, inputZ, 1);
            var translateVector = new Vector3(translateX, translateY, translateZ);
            var scaleVector = new Vector3(scaleX, scaleY, scaleZ);
            var expected = new Vector4(expectedX, expectedY, expectedZ, 1);

            var translate = Matrix4.CreateTranslation(translateVector);
            var scale = Matrix4.CreateScale(scaleVector);
            var transformation = scale * translate;

            var result = transformation * vector;

            result.Should().Be.EqualTo(expected);
        }

        [Test]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
                  16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1,
                  80, 70, 60, 50, 240, 214, 188, 162, 400, 358, 316, 274, 560, 502, 444, 386)]
        public void MatrixMultiplication_TwoValidMatrices_ReturnsMatricesProduct(double m1_11, double m1_12, double m1_13, double m1_14,
                                                                                 double m1_21, double m1_22, double m1_23, double m1_24,
                                                                                 double m1_31, double m1_32, double m1_33, double m1_34,
                                                                                 double m1_41, double m1_42, double m1_43, double m1_44,
                                                                                 double m2_11, double m2_12, double m2_13, double m2_14,
                                                                                 double m2_21, double m2_22, double m2_23, double m2_24,
                                                                                 double m2_31, double m2_32, double m2_33, double m2_34,
                                                                                 double m2_41, double m2_42, double m2_43, double m2_44,
                                                                                 double expected_11, double expected_12, double expected_13, double expected_14,
                                                                                 double expected_21, double expected_22, double expected_23, double expected_24,
                                                                                 double expected_31, double expected_32, double expected_33, double expected_34,
                                                                                 double expected_41, double expected_42, double expected_43, double expected_44)
        {
            var m1 = new Matrix4(m1_11, m1_12, m1_13, m1_14,
                                 m1_21, m1_22, m1_23, m1_24,
                                 m1_31, m1_32, m1_33, m1_34,
                                 m1_41, m1_42, m1_43, m1_44);

            var m2 = new Matrix4(m2_11, m2_12, m2_13, m2_14,
                                 m2_21, m2_22, m2_23, m2_24,
                                 m2_31, m2_32, m2_33, m2_34,
                                 m2_41, m2_42, m2_43, m2_44);

            var expected = new Matrix4(expected_11, expected_12, expected_13, expected_14,
                                       expected_21, expected_22, expected_23, expected_24,
                                       expected_31, expected_32, expected_33, expected_34,
                                       expected_41, expected_42, expected_43, expected_44);

            var result = m1 * m2;

            result.Should().Be.EqualTo(expected);
        }
    }
}