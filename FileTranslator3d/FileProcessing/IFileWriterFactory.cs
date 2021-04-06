using FileTranslator3d.Utility;

namespace FileTranslator3d.FileProcessing
{
    internal interface IFileWriterFactory
    {
        IFileWriter GetFileWriter(FileType filetype);
    }
}
