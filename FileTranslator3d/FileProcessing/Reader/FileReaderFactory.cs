using FileTranslator3d.Utility;
using System;

namespace FileTranslator3d.FileProcessing.Reader
{
    public class FileReaderFactory : IFileReaderFactory
    {
        public IFileReader GetFileReader(FileType filetype)
        {
            switch (filetype)
            {
                case FileType.OBJ:
                    return new ObjReader();
                case FileType.STLBINARY:
                    return new StlReader();
                default:
                    throw new ApplicationException(string.Format("'{0}' reader cannot be created", filetype.ToString())); //TODO: need to modify based on requirement
            }
        }
    }
}
