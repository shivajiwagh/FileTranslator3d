using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace FileTranslator3d.Models
{
    public class TransformationModel
    {
        public float Scale { get; set; }
        public float Rotation { get; set; }
        public float OriginX { get; set; }
        public float OriginY { get; set; }
        public float OriginZ { get; set; }
        public bool Status { get; set; }
        public string OutPutFileType { get; set; }
        public string InputFilePath { get; set; }
        public string OutPutFileName { get; set; }
        public double Area { get; set; }
        public double Volume { get; set; }
    }
}
