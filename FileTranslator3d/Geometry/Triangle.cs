using System;
using System.Collections.Generic;
using System.Numerics;

namespace FileTranslator3d.Geometry
{
    public class Triangle
    {
        public List<Vector3> Points { get; set; }

        private Vector3 normal;
        public Vector3 Normal
        {
            get
            {
                if (normal == null)
                {
                    GetDefaultNormal();
                }

                return normal;
            }
        }

        public ushort AttributeByteCount => 0;

        public Triangle()
        {
            Points = new List<Vector3>();
        }

        public double GetArea()
        {
            //https://byjus.com/maths/area-triangle-coordinate-geometry/
            double a = (Points[0] - Points[1]).Length();
            double b = (Points[1] - Points[2]).Length();
            double c = (Points[2] - Points[0]).Length();
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        public double GetVolume()
        {
            //http://chenlab.ece.cornell.edu/Publication/Cha/icip01_Cha.pdf
            //https://stackoverflow.com/questions/1406029/how-to-calculate-the-volume-of-a-3d-mesh-object-the-surface-of-which-is-made-up
            return Vector3.Dot(Points[0], Vector3.Cross(Points[1], Points[2])) * 1.0 / 6.0;
        }

        private void GetDefaultNormal()
        {
            AddNormal(Points[0], Points[1], Points[2]);
        }

        public void AddNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 uDirection = p2 - p1;
            Vector3 vDirection = p3 - p1;
            Vector3 norm = Vector3.Cross(uDirection, vDirection);
            normal = Vector3.Normalize(norm);
        }
    }
}
