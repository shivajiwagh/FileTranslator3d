using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using FileTranslator3d.Utility;

namespace FileTranslator3d
{
    public interface IFileTranslatorFacade
    {
        void ReadFile(string inputFile, string inputFormat);

        void WriteFile(string outputFile, string outputFormat);

        double GetSurfaceArea();

        double GetSurfaceVolume();

        bool Translate(float x, float y, float z);

        bool Rotate(RotationAxis axis, double angle);

        bool Scale(float sf);

        bool IsPointInside(Vector3 point);

        void AddOrigin();
    }
}
