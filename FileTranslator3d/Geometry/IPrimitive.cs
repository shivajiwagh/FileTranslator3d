using System.Numerics;
using FileTranslator3d.Utility;

namespace FileTranslator3d.Geometry
{
    public interface IPrimitive
    {
        double GetSurfaceArea();

        double GetSurfaceVolume();

        bool IsPointInside(Vector3 point);

        bool Translate(Vector3 fromPoint, Vector3 toPoint);

        bool Rotate(RotationAxis axis, double angle);

        bool Scale(float sf, float xf = 0, float yf = 0, float zf = 0);

        void AddOrigin();
    }
}
