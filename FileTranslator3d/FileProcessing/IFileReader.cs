using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing
{
    /// <summary>
    /// Interface represents the reader generic implementation
    /// </summary>
    public interface IFileReader
    {
        #region Member Functions

        /// <summary>
        /// Function to read the input file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        IPrimitive ReadFile(string filename);

        #endregion
    }
}