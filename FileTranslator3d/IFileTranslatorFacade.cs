using System.Numerics;
using FileTranslator3d.Utility;

namespace FileTranslator3d
{
    /// <summary>
    /// Facade interface -  Single class represents the entire file conversion functionality
    /// </summary>
    public interface IFileTranslatorFacade
    {
        #region Member Functions
        /// <summary>
        /// Reads the given file
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="inputFormat"></param>
        void ReadFile(string inputFile, string inputFormat);

        /// <summary>
        /// Writes the output file
        /// </summary>
        /// <param name="outputFile"></param>
        /// <param name="outputFormat"></param>
        void WriteFile(string outputFile, string outputFormat);

        /// <summary>
        /// Returns the surface area for the entire geometry
        /// </summary>
        /// <returns></returns>
        double GetSurfaceArea();

        /// <summary>
        /// Returns the surface volume for the entire geometry
        /// </summary>
        /// <returns></returns>
        double GetSurfaceVolume();

        /// <summary>
        /// Moves the entire geometry from one point to another
        /// Here from point is considered as center for reference
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        bool Translate(float x, float y, float z);

        /// <summary>
        /// Rotates the geometry with respect to the given axis and angle
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        bool Rotate(RotationAxis axis, double angle);

        /// <summary>
        /// Scales the geometry
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        bool Scale(float factor);

        /// <summary>
        /// Returns true if the given point is inside the convex model
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool IsPointInside(Vector3 point);

        /// <summary>
        /// The is just for reference purpose - Just to check if it is scaling or not - adds a triangle
        /// </summary>
        void AddOrigin();

        #endregion
    }
}