using System;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.Writer
{
    /// <summary>
    /// TODO - Implement concrete obj writer
    /// </summary>
    public class ObjWriter : IFileWriter
    {
        #region Properties

        /// <summary>
        /// Obj hear string
        /// </summary>
        public string Header => throw new NotImplementedException();

        #endregion

        #region Interface Implementations

        /// <summary>
        /// TODO - Add the obj writer implementation
        /// </summary>
        /// <param name="primitive"></param>
        /// <param name="filepath"></param>
        public void Write(IPrimitive primitive, string filepath)
        {
        }

        #endregion
    }
}