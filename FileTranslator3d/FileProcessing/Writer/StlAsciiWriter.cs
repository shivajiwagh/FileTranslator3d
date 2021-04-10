using System;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.Writer
{
    /// <summary>
    /// TODO - Stl ASCII writer
    /// </summary>
    public class StlAsciiWriter : IFileWriter
    {
        #region Properties

        /// <summary>
        /// stl header
        /// </summary>
        public string Header => throw new NotImplementedException();

        #endregion

        #region Interface Implementations

        /// <summary>
        /// TODO - implement the ascii writer
        /// </summary>
        /// <param name="primitive"></param>
        /// <param name="filepath"></param>
        public void Write(IPrimitive primitive, string filepath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}