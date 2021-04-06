using FileTranslator3d.Utility;
using System;

namespace FileTranslator3d.FileProcessing.Writer
{
    public class FileWriterFactory : IFileWriterFactory
    {
        public IFileWriter GetFileWriter(FileType filetype)
        {
            switch (filetype)
            {
                case FileType.OBJ:
                    return new ObjWriter();
                case FileType.STLBINARY:
                    return new StlBinaryWriter();
                default:
                    throw new ApplicationException(string.Format("'{0}' reader cannot be created", filetype.ToString())); //TODO: need to modify based on requirement
            }
        }
    }
}
