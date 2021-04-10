using System;
using System.Numerics;

namespace FileTranslator3d.Utility
{
    /// <summary>
    /// Extenstion class for the vector
    /// </summary>
    public static class Extensions
    {
        #region Member Functions

        /// <summary>
        /// Returns the magnitude of the vector
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static double Magnitude(this Vector3 vec)
        {
            return Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z);
        }

        #endregion
    }
}