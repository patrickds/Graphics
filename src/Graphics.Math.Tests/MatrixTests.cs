using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Math.Tests
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void CreateTranslationTest()
        {
            var vector = Vector4.I;
            var offset = new Vector3(9, 0, 0);
            var matrix = Matrix4.CreateTranslation(offset);

            var result = matrix * vector;
            var expecteResult = new Vector4(10, 0, 0, 1);

            Assert.AreEqual(expecteResult, result);
        }

        [TestMethod]
        public void CreateScaleTest()
        {
            var vector = new Vector4(10, 0, 0, 1);
            var factor = new Vector3(2, 0, 0);
            var matrix = Matrix4.CreateTranslation(factor);

            var result = matrix * vector;
            var expecteResult = new Vector4(20, 0, 0, 1);

            Assert.AreEqual(expecteResult, result);
        }
    }
}
