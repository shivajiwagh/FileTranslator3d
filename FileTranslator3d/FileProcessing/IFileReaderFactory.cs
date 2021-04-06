using FileTranslator3d.Utility;

namespace FileTranslator3d.FileProcessing
{
    public interface IFileReaderFactory
    {
        IFileReader GetFileReader(FileType filetype);
    }
}
