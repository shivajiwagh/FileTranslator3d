using System.Numerics;
using FileTranslator3d.Utility;

namespace FileTranslator3d.Geometry
{
    /// <summary>
    /// Interface representation of generic geometry model. 
    /// </summary>
    public interface IPrimitive
    {
        #region Member Functions

        /// <summary>
        /// Returns the surface area
        /// </summary>
        /// <returns></returns>
        double GetSurfaceArea();

        /// <summary>
        /// Returns the surface volume
        /// </summary>
        /// <returns></returns>
        double GetSurfaceVolume();

        /// <summary>
        /// Checks if the given point is inside or outside the 3d geometry
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool IsPointInside(Vector3 point);

        /// <summary>
        /// Moves the geometry from one point to another
        /// </summary>
        /// <param name="fromPoint"></param>
        /// <param name="toPoint"></param>
        /// <returns></returns>
        bool Translate(Vector3 fromPoint, Vector3 toPoint);

        /// <summary>
        /// Rotates the geometry to given angle with respect to an axis
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        bool Rotate(RotationAxis axis, double angle);

        /// <summary>
        /// Scales the geometry - default scale factor is 1
        /// </summary>
        /// <param name="sf"></param>
        /// <param name="xf"></param>
        /// <param name="yf"></param>
        /// <param name="zf"></param>
        /// <returns></returns>
        bool Scale(float sf, float xf = 0, float yf = 0, float zf = 0);

        /// <summary>
        /// The is just for reference purpose - Just to check if it is scaling or not - adds a triangle
        /// </summary>
        void AddOrigin();

        #endregion
    }
}