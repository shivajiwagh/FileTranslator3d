using System;
using FileTranslator3d.Utility;

namespace FileTranslator3d.FileProcessing.Writer
{
    /// <summary>
    /// Factory for getting the appropriate writer 
    /// </summary>
    public class FileWriterFactory : IFileWriterFactory
    {
        #region Interface Implementations

        /// <summary>
        /// Returns the appropriate writer instance
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public IFileWriter GetFileWriter(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.OBJ:
                    return new ObjWriter();
                case FileType.STLBINARY:
                    return new StlBinaryWriter();
                default:
                    throw new ApplicationException($"'{fileType.ToString()}' reader cannot be created");
            }
        }

        #endregion
    }
}