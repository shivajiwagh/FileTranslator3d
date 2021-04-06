using System.Collections.Generic;
using System.Numerics;

namespace FileTranslator3d.Geometry
{
    public class BrepGeometry : IPrimitive
    {
        public List<Triangle> Triangles { get; set; }

        public BrepGeometry()
        {
            Triangles = new List<Triangle>();
        }

        public BrepGeometry(List<Triangle> triangles)
        {
            Triangles = triangles;
        }

        //public double Area => GetArea();
        //public double Volume => GetVolume();

        public double GetSurfaceArea()
        {
            double area = 0;
            Triangles.ForEach(t =>
            {
                area += t.GetArea();
            });
            return area;
        }

        public double GetSurfaceVolume()
        {
            double volume = 0;
            Triangles.ForEach(t =>
            {
                volume += t.GetVolume();
            });
            return volume;
        }

        public bool IsPointInside(Vector3 point)
        {
            throw new System.NotImplementedException();
        }
    }
}
