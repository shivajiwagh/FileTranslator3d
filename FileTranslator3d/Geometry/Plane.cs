using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FileTranslator3d.Geometry
{
    public class Plane
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly double _d;

        public Plane(double a, double b, double c, double d)
        {
            this._a = a;
            this._b = b;
            this._c = c;
            this._d = d;
        }
        public Plane(Triangle triangle)
        {
            Vector3 v = triangle.Points[1] - triangle.Points[0];
            Vector3 u = triangle.Points[2] - triangle.Points[0];

            Vector3 n = u * v;

            // normal vector		
            double a = n.X;
            double b = n.Y;
            double c = n.Z;
            double d = -(a * triangle.Points[0].X + b * triangle.Points[0].Y + c * triangle.Points[0].Z);

            this._a = a;
            this._b = b;
            this._c = c;
            this._d = d;
        }

        public static Plane operator -(Plane pl)
        {
            return new Plane(-pl._a, -pl._b, -pl._c, -pl._d);
        }

        public static double operator *(Vector3 pt, Plane pl)
        {
            return (pt.X * pl._a + pt.Y * pl._b + pt.Z * pl._c + pl._d);
        }
    }
}
