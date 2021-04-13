using System;
using System.Collections.Generic;
using System.Numerics;

namespace FileTranslator3d.Geometry
{
    /// <summary>
    /// Triangle class - holds array of points and normal
    /// </summary>
    public class Triangle
    {
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor - initializes the points to zero
        /// </summary>
        public Triangle()
        {
            Points = new List<Vector3>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Array of points 
        /// </summary>
        public List<Vector3> Points { get; set; }

        /// <summary>
        /// Triangle normal
        /// </summary>
        public Vector3 Normal { get; set; }

        /// <summary>
        /// This is for writing the stl binary format
        /// </summary>
        public ushort AttributeByteCount => 0;

        #endregion

        #region Member Functions

        /// <summary>
        /// Returns the area of the triangle
        /// </summary>
        /// <returns></returns>
        public double GetArea()
        {
            //https://byjus.com/maths/area-triangle-coordinate-geometry/
            double a = (Points[0] - Points[1]).Length();
            double b = (Points[1] - Points[2]).Length();
            double c = (Points[2] - Points[0]).Length();
            var s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        /// <summary>
        /// Returns the volume
        /// </summary>
        /// <returns></returns>
        public double GetVolume()
        {
            //http://chenlab.ece.cornell.edu/Publication/Cha/icip01_Cha.pdf
            //https://stackoverflow.com/questions/1406029/how-to-calculate-the-volume-of-a-3d-mesh-object-the-surface-of-which-is-made-up
            return Vector3.Dot(Points[0], Vector3.Cross(Points[1], Points[2])) * 1.0 / 6.0;
        }

        /// <summary>
        /// Calculates the normal based on the vertices on the triangle
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        public void AddNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var uDirection = p2 - p1;
            var vDirection = p3 - p1;
            var norm = Vector3.Cross(uDirection, vDirection);
            Normal = Vector3.Normalize(norm);
        }

        #endregion
    }
}