using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP4560_asgn5;
using System.Diagnostics;

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
        public void TestMatrixMultiply() {
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

        [TestMethod]
        public void TestVectorMultiply() {
            Vec4 a = new Vec4(10, 20, 30, 1);
            Matrix mt = Matrix.translationMatrix(1, 2, 3);

            Vec4 expected = new Vec4(11, 22, 33, 1);
            Vec4 actual = a * mt;

            Console.WriteLine("\nTranslation:");
            Console.WriteLine("Expected is: " + expected);
            Console.WriteLine("Actual is: " + actual);

            Assert.IsTrue(actual == expected, "Translation matrix multiplication failed");

            Matrix ms = Matrix.scaleMatrix(1.5, 3, -2);
            expected = new Vec4(15, 60, -60, 1);
            actual = a * ms;
            Console.WriteLine("\nScaling:");
            Console.WriteLine("Expected is: " + expected);
            Console.WriteLine("Actual is: " + actual);
            Assert.IsTrue(actual == expected, "Translation matrix multiplication failed");
        }

        [TestMethod]
        public void TestRotationMatrix() {
            Vec4 xAxis = new Vec4(1, 0, 0, 1);
            Vec4 yAxis = new Vec4(0, 1, 0, 1);
            Vec4 zAxis = new Vec4(0, 0, 1, 1);
            Vec4 nxAxis = new Vec4(-1, 0, 0, 1);
            Vec4 nyAxis = new Vec4(0, -1, 0, 1);
            Vec4 nzAxis = new Vec4(0, 0, -1, 1);

            Matrix mrx = Matrix.rotateMatrix(Matrix.Axis.X, Math.PI / 2);
            Matrix mry = Matrix.rotateMatrix(Matrix.Axis.Y, Math.PI / 2);
            Matrix mrz = Matrix.rotateMatrix(Matrix.Axis.Z, Math.PI / 2);
            Matrix mrnx = Matrix.rotateMatrix(Matrix.Axis.X, -Math.PI / 2);
            Matrix mrny = Matrix.rotateMatrix(Matrix.Axis.Y, -Math.PI / 2);
            Matrix mrnz = Matrix.rotateMatrix(Matrix.Axis.Z, -Math.PI / 2);
            
            Console.WriteLine("X axis * mrz = " + (xAxis * mrz));
            Assert.IsTrue(yAxis == xAxis * mrz, "mrz rotation Failed.");
            Console.WriteLine("Y axis * mrx = " + (yAxis * mrx));
            Assert.IsTrue(zAxis == yAxis * mrx, "mrx rotation Failed.");
            Console.WriteLine("Z axis * mry = " + (zAxis * mry));
            Assert.IsTrue(xAxis == zAxis * mry, "mry rotation Failed.");

            Console.WriteLine("X axis * mrnz = " + (xAxis * mrnz));
            Assert.IsTrue(nyAxis == xAxis * mrnz, "mrz rotation Failed.");
            Console.WriteLine("Y axis * mrnx = " + (yAxis * mrnx));
            Assert.IsTrue(nzAxis == yAxis * mrnx, "mrx rotation Failed.");
            Console.WriteLine("Z axis * mrny = " + (zAxis * mrny));
            Assert.IsTrue(nxAxis == zAxis * mrny, "mry rotation Failed.");
        }
    }
}
