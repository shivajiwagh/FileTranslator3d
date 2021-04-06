using System.Numerics;

namespace FileTranslator3d.Geometry
{
    public interface IPrimitive
    {
        double GetSurfaceArea();

        double GetSurfaceVolume();

        bool IsPointInside(Vector3 point);
    }
}
