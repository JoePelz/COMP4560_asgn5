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
            for (int i = 0; i < 5; i++)
            {
                matrix[i, i] = 1;
            }
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

            for (int m = 0; m < 5; m++)
            {
                for (int n = 0; n < 5; n++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        //todo: test this!
                        result.matrix[m, n] += lhs.matrix[m, c] * rhs.matrix[c, n];
                    }
                }
            }
            return result;
        }
    }
}
