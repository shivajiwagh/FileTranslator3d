using System.Numerics;

namespace FileTranslator3d.Geometry
{
    /// <summary>
    /// Plane class - for calculating the face to point distance
    /// ref : https://www.codeproject.com/Articles/1065730/Point-Inside-Convex-Polygon-in-Cplusplus#:~:text=A%20point%20is%20determined%20to,basic%20idea%20of%20this%20algorithm.
    /// </summary>
    public class Plane
    {
        #region Fields

        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly double _d;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="triangle"></param>
        public Plane(Triangle triangle)
        {
            var normal = triangle.Normal;
            // normal vector		
            double a = normal.X;
            double b = normal.Y;
            double c = normal.Z;
            var d = -(a * triangle.Points[0].X + b * triangle.Points[0].Y + c * triangle.Points[0].Z);

            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }

        #endregion

        #region Member Functions
        /// <summary>
        /// Operator overload to calculate the plane to point distance
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="pl"></param>
        /// <returns></returns>
        public static double operator *(Vector3 pt, Plane pl)
        {
            return pt.X * pl._a + pt.Y * pl._b + pt.Z * pl._c + pl._d;
        }

        #endregion
    }
}