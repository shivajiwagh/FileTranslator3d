using System;
using FileTranslator3d.Utility;

namespace FileTranslator3d.FileProcessing.Reader
{
    /// <summary>
    /// FileReaderFactory - returns the appropriate class for reading 3d file.
    /// </summary>
    public class FileReaderFactory : IFileReaderFactory
    {
        #region Interface Implementations

        /// <summary>
        /// Returns the appropriate reader instance
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public IFileReader GetFileReader(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.OBJ:
                    return new ObjReader();
                case FileType.STLBINARY:
                    return new StlReader();
                default:
                    throw new ApplicationException($"'{fileType.ToString()}' reader cannot be created"); 
            }
        }

        #endregion
    }
}