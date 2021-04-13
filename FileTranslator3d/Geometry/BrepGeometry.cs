using System;
using System.Collections.Generic;
using System.Numerics;
using FileTranslator3d.Utility;

namespace FileTranslator3d.Geometry
{
    /// <summary>
    ///     Represents 3d geometry model which can be used across all file formats
    /// </summary>
    public class BrepGeometry : IPrimitive
    {
        #region Constructor

        /// <summary>
        ///     Constructor
        /// </summary>
        public BrepGeometry()
        {
            Triangles = new List<Triangle>();
        }

        /// <summary>
        ///     Constructor which take triangle as argument
        /// </summary>
        /// <param name="triangles"></param>
        public BrepGeometry(List<Triangle> triangles)
        {
            Triangles = triangles;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Array of triangles geometry has
        /// </summary>
        public List<Triangle> Triangles { get; set; }

        #endregion

        #region Interface Implementations

        /// <summary>
        ///     Returns the surface area  - sum of area of all triangles
        /// </summary>
        /// <returns></returns>
        public double GetSurfaceArea()
        {
            double area = 0;
            Triangles.ForEach(t => { area += t.GetArea(); });
            return area;
        }

        /// <summary>
        ///     Returns the surface volume
        /// </summary>
        /// <returns></returns>
        public double GetSurfaceVolume()
        {
            double volume = 0;
            Triangles.ForEach(t => { volume += t.GetVolume(); });
            return volume;
        }

        /// <summary>
        ///     Translates/Moves the geometry from one point to another
        /// </summary>
        /// <param name="fromPoint"></param>
        /// <param name="toPoint"></param>
        /// <returns></returns>
        public bool Translate(Vector3 fromPoint, Vector3 toPoint)
        {
            foreach (var triangle in Triangles)
            {
                var resultant = toPoint - fromPoint;
                for (var i = 0; i < triangle.Points.Count; i++) triangle.Points[i] = triangle.Points[i] + resultant;
            }

            return true;
        }

        /// <summary>
        ///     Scales the geometry - default is 1
        /// </summary>
        /// <param name="sf"></param>
        /// <param name="xf"></param>
        /// <param name="yf"></param>
        /// <param name="zf"></param>
        /// <returns></returns>
        public bool Scale(float sf, float xf = 0, float yf = 0, float zf = 0)
        {
            foreach (var triangle in Triangles)
                for (var i = 0; i < triangle.Points.Count; i++)
                {
                    var newPoint = new Vector3(triangle.Points[i].X * sf + (1 - sf) * xf,
                        triangle.Points[i].Y * sf + (1 - sf) * yf, triangle.Points[i].Z * sf + (1 - sf) * zf);
                    triangle.Points[i] = newPoint;
                }

            return true;
        }

        /// <summary>
        ///     Rotates the entire geometry to a specified angle with respect to the axis
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public bool Rotate(RotationAxis axis, double angle)
        {
            angle = Math.PI * angle / 180;
            foreach (var triangle in Triangles)
                for (var i = 0; i < triangle.Points.Count; i++)
                    switch (axis)
                    {
                        case RotationAxis.X:
                            triangle.Points[i] = RotatePointAboutX(triangle.Points[i], angle);
                            break;
                        case RotationAxis.Y:
                            triangle.Points[i] = RotatePointAboutY(triangle.Points[i], angle);
                            break;
                        case RotationAxis.Z:
                            triangle.Points[i] = RotatePointAboutZ(triangle.Points[i], angle);
                            break;
                        default:
                            triangle.Points[i] = RotatePointAboutX(triangle.Points[i], angle);
                            break;
                    }

            return true;
        }

        /// <summary>
        ///     The is just for reference purpose - Just to check if it is scaling or not - adds a triangle
        /// </summary>
        public void AddOrigin()
        {
            var pt1 = new Vector3(0, 0, 0);
            var pt2 = new Vector3(5, 0, 0);
            var pt3 = new Vector3(5, 5, 0);
            var tr = new Triangle();
            tr.Points.Add(pt1);
            tr.Points.Add(pt2);
            tr.Points.Add(pt3);
            Triangles.Add(tr);
        }

        /// <summary>
        ///     Returns true if the given point is inside the convex geometry.
        ///     ref:https://www.youtube.com/watch?v=qBo5oVFqnPc
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointInside(Vector3 point)
        {
            var tolerance = 1e-6;
            double totalAngle = 0;
            foreach (var triangle in Triangles)
            {
                var ptA = triangle.Points[0];
                var ptB = triangle.Points[1];
                var ptC = triangle.Points[2];

                var a = point - ptA;
                var b = point - ptB;
                var c = point - ptC;

                var angle = GetSolidAngle(a, b, c);
                var center = GetCentroid(ptA, ptB, ptC);

                var faceVector = center - point;

                var dot = Vector3.Dot(triangle.Normal, faceVector);
                double factor = dot > 0 ? 1 : -1;

                totalAngle += angle * factor;
            }

            var absTotal = Math.Abs(totalAngle);
            var inside = Math.Abs(absTotal - 4 * Math.PI) < tolerance;
            return inside;
        }

        #endregion

        #region Member Functions

        private Vector3 GetCentroid(Vector3 p, Vector3 q, Vector3 r)
        {
            var sum = Vector3.Add(Vector3.Add(p, q), r);
            return Vector3.Multiply(sum, 1 / 3.0f);
        }

        private double GetSolidAngle(Vector3 a, Vector3 b, Vector3 c)
        {
            a = Vector3.Normalize(a);
            b = Vector3.Normalize(b);
            c = Vector3.Normalize(c);

            var numerator = Vector3.Dot(Vector3.Cross(a, b), c);
            var denominator = 1.0f + Vector3.Dot(a, b) + Vector3.Dot(b, c) + Vector3.Dot(c, a);

            var angle = 2 * Math.Atan2(numerator, denominator);
            return Math.Abs(angle);
        }

        private Vector3 RotatePointAboutX(Vector3 point, double angle)
        {
            var retPoint = new Vector3
            {
                X = point.X,
                Y = (float) (point.Y * Math.Cos(angle) - point.Z * Math.Sin(angle)),
                Z = (float) (point.Y * Math.Sin(angle) + point.Z * Math.Cos(angle))
            };
            return retPoint;
        }

        private Vector3 RotatePointAboutY(Vector3 point, double angle)
        {
            var retPoint = new Vector3
            {
                Y = point.Y,
                Z = (float) (point.Z * Math.Cos(angle) - point.X * Math.Sin(angle)),
                X = (float) (point.Z * Math.Sin(angle) + point.X * Math.Cos(angle))
            };
            return retPoint;
        }

        private Vector3 RotatePointAboutZ(Vector3 point, double angle)
        {
            var retPoint = new Vector3
            {
                Z = point.Z,
                X = (float) (point.X * Math.Cos(angle) - point.Y * Math.Sin(angle)),
                Y = (float) (point.X * Math.Sin(angle) + point.Y * Math.Cos(angle))
            };
            return retPoint;
        }

        #endregion
    }
}