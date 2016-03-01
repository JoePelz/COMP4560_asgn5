using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    class Vec4
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
    }
}
