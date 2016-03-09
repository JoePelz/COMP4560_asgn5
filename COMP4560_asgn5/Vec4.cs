using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    public class Vec4
    {
        public double x, y, z, h;

        public Vec4(double x, double y, double z, double h)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.h = h;
        }
        public Vec4(double x, double y, double z) : this(x, y, z, 1.0) { }
        public Vec4(Vec3 v) : this(v.x, v.y, v.z, 1.0) { }
        
        public void offset(Vec4 v) {
            x += v.x;
            y += v.y;
            z += v.z;
            //skipping h intentionally
        }
        
        public void normalizeH() {
            x /= h;
            y /= h;
            z /= h;
            h = 1;
        }

        public static Vec4 operator -(Vec4 unary) {
            return new Vec4(-unary.x, -unary.y, -unary.z, -unary.h);
        }

        public static bool operator ==(Vec4 lhs, Vec4 rhs) {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Vec4 lhs, Vec4 rhs) {
            return !(lhs == rhs);
        }
        public override bool Equals(Object obj) {
            // If parameter is null return false.
            if (obj == null) {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Vec4 rhs = obj as Vec4;
            if ((Object)rhs == null) {
                return false;
            }

            // Return true if the fields match:
            return Math.Abs(x - rhs.x) < 0.001
                && Math.Abs(y - rhs.y) < 0.001
                && Math.Abs(z - rhs.z) < 0.001
                && Math.Abs(h - rhs.h) < 0.001;
        }
        public bool Equals(Vec4 rhs) {
            if ((Object)rhs == null) return false;

            return Math.Abs(x - rhs.x) < 0.001
                && Math.Abs(y - rhs.y) < 0.001
                && Math.Abs(z - rhs.z) < 0.001
                && Math.Abs(h - rhs.h) < 0.001;
        }
        public override string ToString() {
            return String.Format("({0:0.00}, {1:0.00}, {2:0.00}, {3:0.00})", x, y, z, h);
        }

        public override int GetHashCode() {
            return (int)(x * 1000) ^ (int)(y * 1000) ^ (int)(z * 1000) ^ (int)(h * 1000);
        }
    }
}
