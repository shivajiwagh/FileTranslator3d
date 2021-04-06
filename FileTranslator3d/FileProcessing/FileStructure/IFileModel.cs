using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.FileStructure
{
    //For inheritance purpose only
    public interface IFileModel
    {
        #region Member Functions

        IPrimitive GetGeometryModel();

        #endregion
    }
}