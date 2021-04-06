using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using FileTranslator3d.Utility;

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

        public bool Translate(Vector3 fromPoint, Vector3 toPoint)
        {
            foreach (Triangle triangle in Triangles)
            {
                Vector3 resultant = toPoint - fromPoint;
                for (int i = 0; i < triangle.Points.Count; i++)
                {
                    triangle.Points[i] = triangle.Points[i] + resultant;
                }
            }
            return true;
        }

        public bool Scale(float sf, float xf = 0, float yf = 0, float zf = 0)
        {
            foreach (Triangle triangle in Triangles)
            {
                for (int i = 0; i < triangle.Points.Count; i++)
                {
                    Vector3 newPoint = new Vector3(triangle.Points[i].X * sf + (1 - sf) * xf,
                        triangle.Points[i].Y * sf + (1 - sf) * yf, triangle.Points[i].Z * sf + (1 - sf) * zf);
                        triangle.Points[i] = newPoint;
                }
            }
            return true;
        }

        public bool Rotate(RotationAxis axis, double angle)
        {
            angle = (Math.PI * angle) / 180;
            foreach (Triangle triangle in Triangles)
            {
                for (int i = 0; i < triangle.Points.Count; i++)
                {
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
                }
            }
            return true;
        }

        private Vector3 RotatePointAboutX(Vector3 point, double angle)
        {
            Vector3 retPoint = new Vector3
            {
                X = point.X,
                Y = (float)(point.Y * Math.Cos(angle) - point.Z * Math.Sin(angle)),
                Z = (float)(point.Y * Math.Sin(angle) + point.Z * Math.Cos(angle))
            };
            return retPoint;
        }

        private Vector3 RotatePointAboutY(Vector3 point, double angle)
        {
            Vector3 retPoint = new Vector3();
            retPoint.Y = point.Y;
            retPoint.Z = (float)(point.Z * Math.Cos(angle) - point.X * Math.Sin(angle));
            retPoint.X = (float)(point.Z * Math.Sin(angle) + point.X * Math.Cos(angle));
            return retPoint;
        }

        private Vector3 RotatePointAboutZ(Vector3 point, double angle)
        {
            Vector3 retPoint = new Vector3();
            retPoint.Z = point.Z;
            retPoint.X = (float)(point.X * Math.Cos(angle) - point.Y * Math.Sin(angle));
            retPoint.Y = (float)(point.X * Math.Sin(angle) + point.Y * Math.Cos(angle));
            return retPoint;
        }

        public void AddOrigin()
        {
            Vector3 pt1 = new Vector3(0, 0, 0);
            Vector3 pt2 = new Vector3(5, 0, 0);
            Vector3 pt3 = new Vector3(5, 5, 0);
            Triangle tr = new Triangle();
            tr.Points.Add(pt1);
            tr.Points.Add(pt2);
            tr.Points.Add(pt3);
            Triangles.Add(tr);
        }

        public bool IsPointInside(Vector3 point)
        {
            throw new System.NotImplementedException();
        }
    }
}
