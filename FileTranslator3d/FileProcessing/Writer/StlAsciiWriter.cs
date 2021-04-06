using FileTranslator3d.Geometry;
using System;

namespace FileTranslator3d.FileProcessing.Writer
{
    public class StlAsciiWriter : IFileWriter
    {
        public string Header => throw new NotImplementedException();

        public void Write(IPrimitive primitive, string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
