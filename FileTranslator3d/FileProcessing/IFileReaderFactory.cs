using FileTranslator3d.Utility;

namespace FileTranslator3d.FileProcessing
{
    /// <summary>
    /// Reader factory which returns the appropriate reader
    /// </summary>
    public interface IFileReaderFactory
    {
        #region Member Functions
        /// <summary>
        /// Method returns the appropriate reader based on file extension
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        IFileReader GetFileReader(FileType fileType);

        #endregion
    }
}