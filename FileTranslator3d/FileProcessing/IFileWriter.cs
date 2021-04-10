using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing
{
    /// <summary>
    /// IFileWriter interface for generic writer
    /// </summary>
    public interface IFileWriter
    {
        #region Properties

        /// <summary>
        /// File header string
        /// </summary>
        string Header { get; }

        #endregion

        #region Member Functions

        /// <summary>
        /// Method to override 
        /// </summary>
        /// <param name="primitive"></param>
        /// <param name="filepath"></param>
        void Write(IPrimitive primitive, string filepath);

        #endregion
    }
}