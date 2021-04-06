using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing
{
    public interface IFileReader
    {
        IPrimitive ReadFile(string filename);
    }
}
