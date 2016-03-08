using System;
using System.Collections.Generic;
using System.Text;

namespace COMP4560_asgn5 {
    public class BBox
    {
        public double xmin, xmax;
        public double ymin, ymax;
        public double zmin, zmax;

        public BBox(double[,] vertices)
        {
            if (vertices == null || vertices.Length == 0)
            {
                throw new ArgumentException();
            }
            xmin = xmax = vertices[0, 0];
            ymin = ymax = vertices[0, 1];
            zmin = zmax = vertices[0, 2];

            for (int v = 1; v < vertices.GetLength(0) - 1; v++)
            {
                if (vertices[v, 0] < xmin) xmin = vertices[v, 0];
                else if (vertices[v, 0] > xmax) xmax = vertices[v, 0];

                if (vertices[v, 1] < ymin) ymin = vertices[v, 1];
                else if (vertices[v, 1] > ymax) ymax = vertices[v, 1];

                if (vertices[v, 2] < zmin) zmin = vertices[v, 2];
                else if (vertices[v, 2] > zmax) zmax = vertices[v, 2];
            }
        }
        public Vec3 getCenter()
        {
            return new Vec3((xmax + xmin) / 2, (ymax + ymin) / 2, (zmax + zmin) / 2);
        }

    }
}
