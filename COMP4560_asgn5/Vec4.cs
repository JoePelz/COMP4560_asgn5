using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    public class Vec4
    {
        public double x, y, z, w;

        public Vec4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vec4(double x, double y, double z) : this(x, y, z, 1.0) { }
        public Vec4(Vec3 v) : this(v.x, v.y, v.z, 1.0) { }



        public static bool operator ==(Vec4 lhs, Vec4 rhs) {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Vec4 lhs, Vec4 rhs) {
            return !(lhs == rhs);
        }
        public bool Equals(Vec4 rhs) {
            return Math.Abs(x - rhs.x) < 0.001
                && Math.Abs(y - rhs.y) < 0.001
                && Math.Abs(z - rhs.z) < 0.001
                && Math.Abs(w - rhs.w) < 0.001;
        }
        public override string ToString() {
            return String.Format("({0:0.00}, {1:0.00}, {2:0.00}, {3:0.00})", x, y, z, w);
        }
    }
}
