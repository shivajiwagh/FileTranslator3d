using System;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.Reader
{
    /// <summary>
    /// TODO - Implement concrete stl reader
    /// </summary>
    public class StlReader : IFileReader
    {
        #region Interface Implementations

        /// <summary>
        /// TODO - read the stl file and return geometry model
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IPrimitive ReadFile(string filename)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}