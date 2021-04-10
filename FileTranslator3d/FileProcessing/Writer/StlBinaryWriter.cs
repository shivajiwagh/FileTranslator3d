using System;
using System.IO;
using System.Numerics;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.Writer
{
    /// <summary>
    /// Writes the stl in binary format
    /// </summary>
    public class StlBinaryWriter : IFileWriter
    {
        #region Fields

        private readonly int headerLimit = 80;

        #endregion

        #region Properties
        /// <summary>
        /// Header of the stl file
        /// </summary>
        public string Header { get; private set; } = "STL Binary -";

        #endregion

        #region Interface Implementations

        /// <summary>
        ///  Writer the stl file in binary format
        /// </summary>
        /// <param name="primitive"></param>
        /// <param name="filepath"></param>
        public void Write(IPrimitive primitive, string filepath)
        {
            Header += new FileInfo(filepath).Name;

            var geometry = (BrepGeometry) primitive;
            using (var writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                var bytes = new byte[headerLimit]; //File name more than 80 characters will be trimmed. 
                var limit = Header.Length > headerLimit ? headerLimit : Header.Length;
                for (var i = 0; i < limit; i++) bytes[i] = Convert.ToByte(Header[i]);

                writer.Write(bytes);
                writer.Write((uint) geometry.Triangles.Count);

                //Write triangles
                foreach (var triangle in geometry.Triangles)
                {
                    //Write Normal
                    WritePoint(writer, triangle.Normal);

                    //Write points
                    foreach (var point in triangle.Points) WritePoint(writer, point);

                    //Attribute byte count
                    writer.Write(triangle.AttributeByteCount);
                }
            }
        }

        #endregion

        #region Member Functions

        private static void WritePoint(BinaryWriter writer, Vector3 point)
        {
            writer.Write(point.X);
            writer.Write(point.Y);
            writer.Write(point.Z);
        }

        #endregion

    }
}