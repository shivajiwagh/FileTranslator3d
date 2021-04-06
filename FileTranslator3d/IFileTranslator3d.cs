using System;
using System.Collections.Generic;
using System.Text;

namespace FileTranslator3d
{
    public interface IFileTranslator3d
    {
        void TranslateFile(string inputFile, string inputFormat, string outputFile, string outputFormat);

        double GetSurfaceArea();

        double GetSurfaceVolume();
    }
}
