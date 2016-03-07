using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    public class Vec3
    {
        public double x, y, z;
        public Vec3(double x, double y, double z) 
        {
            this.x = x; 
            this.y = y; 
            this.z = z;
        }


        public static Vec3 operator -(Vec3 unary) {
            return new Vec3(-unary.x, -unary.y, -unary.z);
        }
    }
}
