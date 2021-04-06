using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using FileTranslator3d.FileProcessing;
using FileTranslator3d.FileProcessing.Reader;
using FileTranslator3d.FileProcessing.Writer;
using FileTranslator3d.Geometry;
using FileTranslator3d.Utility;

namespace FileTranslator3d
{
    public class FileTranslator3d : IFileTranslator3d
    {
        private IPrimitive _geometry;

        #region Implementation of IFileTranslator3d

        public void TranslateFile(string inputFile, string inputFormat, string outputFile, string outputFormat)
        {
            //Read the input file
            IFileReaderFactory readerFactory = new FileReaderFactory();
            IFileReader reader = readerFactory.GetFileReader(GetFileTypeEnum(inputFormat));
            _geometry = reader.ReadFile(inputFile);

            //Write the output file
            IFileWriterFactory writerFactory = new FileWriterFactory();
            IFileWriter writer = writerFactory.GetFileWriter(GetFileTypeEnum(outputFormat));
            writer.Write(_geometry, outputFile);
        }

        public double GetSurfaceArea()
        {
            return _geometry.GetSurfaceArea();
        }

        public double GetSurfaceVolume()
        {
            return _geometry.GetSurfaceVolume();
        }

        public bool IsPointInside(int x, int y, int z)
        {
            return _geometry.IsPointInside(new Vector3(x, y, z));
        }

        private FileType GetFileTypeEnum(string format)
        {
            FileType type = FileType.STLBINARY;
            //var ext = Path.GetExtension(filename).ToLowerInvariant();
            switch (format)
            {
                case "obj":
                    type = FileType.OBJ;
                    break;
                case "stl":
                    type = FileType.STLBINARY;
                    break;
                case "iges":
                    type = FileType.IGES;
                    break;
            }

            return type;
        }

        #endregion
    }
}
