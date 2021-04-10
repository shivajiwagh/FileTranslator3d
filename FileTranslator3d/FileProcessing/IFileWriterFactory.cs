using FileTranslator3d.Utility;

namespace FileTranslator3d.FileProcessing
{
    /// <summary>
    /// Writer factory
    /// </summary>
    internal interface IFileWriterFactory
    {
        #region Member Functions
        /// <summary>
        /// Returns the writer based on file extension
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        IFileWriter GetFileWriter(FileType fileType);

        #endregion
    }
}