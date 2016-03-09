using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    public class Matrix
    {
        public enum Axis { X, Y, Z };

        /// <summary>
        /// A 4x4 matrix, where the first coordinate is the row, and the second is the column.
        /// </summary>
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

        public double[,] getArray() {
            return matrix;
        }

        public static Matrix translationMatrix(Vec3 v) { return translationMatrix(v.x, v.y, v.z); }
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

        /// <summary>
        /// Generates a matrix representing a counter-clockwise rotation 
        /// around the origin when looking from the positive axis given.
        /// </summary>
        /// <param name="a">The axis of rotation. Counter-clockwise is 
        /// determined by looking from this point toward the origin.</param>
        /// <param name="theta">The angle of rotation in radians.</param>
        /// <returns>A new matrix representing the given rotation.</returns>
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
                    m.matrix[1, 2] = sin;
                    m.matrix[2, 1] = -sin;
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
                    m.matrix[0, 1] = sin;
                    m.matrix[1, 0] = -sin;
                    break;
            }
            return m;
        }
        
        public static Matrix shearMatrix(double amount) {
            Matrix m = new Matrix();
            m.matrix[1, 0] = amount;
            return m;
        }

        public void translate(Vec3 vec) { translate(vec.x, vec.y, vec.z); }
        public void translate(Vec4 vec) { translate(vec.x, vec.y, vec.z); }
        public void translate(double x, double y, double z) {
            Matrix trans = translationMatrix(x, y, z);
            Matrix result = this * trans;
            matrix = result.matrix;
        }

        public void rotate(Axis a, double theta) {
            Matrix rot = rotateMatrix(a, theta);
            Matrix result = this * rot;
            matrix = result.matrix;
        }

        public void scale(Vec4 v) { scale(v.x, v.y, v.z); }
        public void scale(Vec3 v) { scale(v.x, v.y, v.z); }
        public void scale(double s) { scale(s, s, s); }
        public void scale(double x, double y, double z) {
            Matrix scale = scaleMatrix(x, y, z);
            Matrix result = this * scale;
            matrix = result.matrix;
        }

        public void shear(double amount) {
            matrix[1, 0] += amount;
        }

        public Vec3 getTranslate() {
            return new Vec3(matrix[3, 0], matrix[3, 1], matrix[3, 2]);
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

        public static Vec4 operator *(Vec4 lhs, Matrix rhs) {
            Vec4 result = new Vec4(0, 0, 0);
            result.x
                = rhs.matrix[0, 0] * lhs.x
                + rhs.matrix[1, 0] * lhs.y
                + rhs.matrix[2, 0] * lhs.z
                + rhs.matrix[3, 0] * lhs.h;
            result.y
                = rhs.matrix[0, 1] * lhs.x
                + rhs.matrix[1, 1] * lhs.y
                + rhs.matrix[2, 1] * lhs.z
                + rhs.matrix[3, 1] * lhs.h;
            result.z
                = rhs.matrix[0, 2] * lhs.x
                + rhs.matrix[1, 2] * lhs.y
                + rhs.matrix[2, 2] * lhs.z
                + rhs.matrix[3, 2] * lhs.h;
            result.h
                = rhs.matrix[0, 3] * lhs.x
                + rhs.matrix[1, 3] * lhs.y
                + rhs.matrix[2, 3] * lhs.z
                + rhs.matrix[3, 3] * lhs.h;
            return result;
        }

        public static bool operator ==(Matrix lhs, Matrix rhs) {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Matrix lhs, Matrix rhs) {
            return !(lhs == rhs);
        }
        public override bool Equals(object obj) {
            if (obj == null) return false;

            Matrix m = obj as Matrix;
            if ((Object)m == null) {
                return false;
            }

            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    if (Math.Abs(matrix[x, y] - m.matrix[x, y]) > 0.00001) {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool Equals(Matrix obj) {
            if ((Object)obj == null) return false;

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
