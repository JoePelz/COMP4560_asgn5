using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP4560_asgn5;

namespace MatrixTester {
    [TestClass]
    public class MatrixTests {
        [TestMethod]
        public void TestIdentity() {
            Matrix a = new Matrix();
            Matrix b = new Matrix(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            Matrix c = new Matrix(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            Assert.IsTrue(a == b, "Equality failed");
            Assert.IsTrue(c == b, "Equality failed");
        }

        [TestMethod]
        public void TestMultiply() {
            Matrix a = new Matrix(); //identity
            Matrix b = Matrix.translationMatrix(1, 2, 3);
            Assert.IsTrue((a * b) == b, "identity multiplication Ib failed.");
            Assert.IsTrue((b * a) == b, "identity multiplication bI failed.");

            Matrix c = Matrix.scaleMatrix(10, 20, 30);
            Matrix expectedCB = new Matrix(
                10, 0, 0, 0,
                0, 20, 0, 0,
                0, 0, 30, 0,
                1, 2, 3, 1);
            Matrix expectedBC = new Matrix(
                10, 0, 0, 0,
                0, 20, 0, 0,
                0, 0, 30, 0,
                10, 40, 90, 1);

            Assert.IsTrue(b * c == expectedBC, "BC matrix multiplication failed.");
            Assert.IsTrue(c * b == expectedCB, "CB matrix multiplication failed.");
        }
    }
}
