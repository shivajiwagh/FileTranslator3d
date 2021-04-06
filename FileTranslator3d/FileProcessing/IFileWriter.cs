using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing
{
    public interface IFileWriter
    {
        string Header { get; }
        void Write(IPrimitive primitive, string filepath);
    }
}
