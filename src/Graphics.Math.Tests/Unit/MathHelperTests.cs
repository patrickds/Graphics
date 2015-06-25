using Graphics.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpTestsEx;

namespace Graphics.Math.Tests.Unit
{
    [TestFixture]
    public class MathHelperTests
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 0.017453)]
        [TestCase(10, 0.174533)]
        [TestCase(90, 1.570796)]
        [TestCase(45, 0.785398)]
        [TestCase(270, 4.712389)]
        [TestCase(-45, -0.785398)]
        [TestCase(-360, -6.283185)]
        public void ToRadians_ValidAngleInDegrees_ReturnsAngleInRadians(double degrees, double expectedRadians)
        {
            var result = MathHelper.Round(MathHelper.ToRadians(degrees));

            result.IsEqualsTo(expectedRadians).Should().Be.True();
        }

        [Test]
        [TestCase(0.017453, 1)]
        [TestCase(0.174533, 10)]
        [TestCase(1.570796, 90)]
        [TestCase(0.785398, 45)]
        [TestCase(4.712389, 270)]
        [TestCase(-0.785398, -45)]
        [TestCase(-6.283185 , -360)]
        public void ToDegrees_ValidAngleInRadians_ReturnsAngleInDegrees(double radians, double expectedDegrees)
        {
            var result = MathHelper.Round(MathHelper.ToDegrees(radians));

            result.IsEqualsTo(expectedDegrees).Should().Be.True();
        }

        [Test]
        [TestCase(1.570796326795, 1.570796)]
        [TestCase(-5.9873468134687, -5.987347)]
        [TestCase(27.71679513474647, 27.716795)]
        public void Round_ValidUnroundedNumber_ReturnsRoundedNumber(double number, double expected)
        {
            var result = MathHelper.Round(number);

            result.Should().Be(expected);
        }
    }
}
