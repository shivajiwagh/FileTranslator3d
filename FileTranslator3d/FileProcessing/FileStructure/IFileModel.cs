using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.FileStructure
{
    /// <summary>
    /// For inheritance purpose only
    /// </summary>
    public interface IFileModel
    {
        #region Member Functions
        /// <summary>
        /// Returns the generic geometry model structure so that it can be reused across all formats
        /// </summary>
        /// <returns></returns>
        IPrimitive GetGeometryModel();

        #endregion
    }
}