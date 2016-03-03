using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    public class Matrix
    {
        public enum Axis { X, Y, Z };

        double[,] matrix;

        /// <summary>
        /// Creates a new identity matrix
        /// </summary>
        public Matrix()
        {
            matrix = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                matrix[i, i] = 1;
            }
        }
        public Matrix(
            double aa, double ab, double ac, double ad,
            double ba, double bb, double bc, double bd,
            double ca, double cb, double cc, double cd,
            double da, double db, double dc, double dd) {
            matrix = new double[4, 4];
            matrix[0, 0] = aa;
            matrix[0, 1] = ab;
            matrix[0, 2] = ac;
            matrix[0, 3] = ad;
            matrix[1, 0] = ba;
            matrix[1, 1] = bb;
            matrix[1, 2] = bc;
            matrix[1, 3] = bd;
            matrix[2, 0] = ca;
            matrix[2, 1] = cb;
            matrix[2, 2] = cc;
            matrix[2, 3] = cd;
            matrix[3, 0] = da;
            matrix[3, 1] = db;
            matrix[3, 2] = dc;
            matrix[3, 3] = dd;
        }

        public static Matrix translationMatrix(double x, double y, double z)
        {
            Matrix m = new Matrix();
            m.matrix[3, 0] = x;
            m.matrix[3, 1] = y;
            m.matrix[3, 2] = z;
            return m;
        }

        public static Matrix scaleMatrix(double x, double y, double z)
        {
            Matrix m = new Matrix();
            m.matrix[0, 0] = x;
            m.matrix[1, 1] = y;
            m.matrix[2, 2] = z;
            return m;
        }

        public static Matrix rotateMatrix(Axis a, double theta)
        {
            Matrix m = new Matrix();
            double cos, sin;
            cos = Math.Cos(theta);
            sin = Math.Sin(theta);
            switch (a)
            {
                case Axis.X:
                    m.matrix[1, 1] = cos;
                    m.matrix[2, 2] = cos;
                    m.matrix[1, 2] = -sin;
                    m.matrix[2, 1] = sin;
                    break;
                case Axis.Y:
                    m.matrix[0, 0] = cos;
                    m.matrix[2, 2] = cos;
                    m.matrix[0, 2] = -sin;
                    m.matrix[2, 0] = sin;
                    break;
                case Axis.Z:
                    m.matrix[0, 0] = cos;
                    m.matrix[1, 1] = cos;
                    m.matrix[0, 1] = -sin;
                    m.matrix[1, 0] = sin;
                    break;
            }
            return m;
        }

        public static Matrix operator *(Matrix lhs, Matrix rhs) 
        {
            Matrix result = new Matrix();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    result.matrix[row, col] = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        result.matrix[row, col] += lhs.matrix[row, i] * rhs.matrix[i, col];
                    }
                }
            }
            return result;
        }

        public static bool operator ==(Matrix lhs, Matrix rhs) {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Matrix lhs, Matrix rhs) {
            return !(lhs == rhs);
        }
        public bool Equals(Matrix obj) {
            for(int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    if (Math.Abs(matrix[x,y] - obj.matrix[x,y]) > 0.00001) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
