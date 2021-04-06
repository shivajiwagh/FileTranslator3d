using FileTranslator3d.Geometry;
using System;
using System.IO;
using System.Numerics;

namespace FileTranslator3d.FileProcessing.Writer
{
    public class StlBinaryWriter : IFileWriter
    {
        private string _header = "STL Binary -";
        public string Header => _header;

        private readonly int headerLimit = 80;

        public void Write(IPrimitive primitive, string filepath)
        {
            _header += new FileInfo(filepath).Name;

            BrepGeometry geometry = (BrepGeometry)primitive;
            using (BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                byte[] bytes = new byte[headerLimit]; //File name more than 80 characters will be trimmed. 
                int limit = _header.Length > headerLimit ? headerLimit : _header.Length;
                for (int i = 0; i < limit; i++)
                {
                    bytes[i] = Convert.ToByte(_header[i]);
                }

                writer.Write(bytes);
                writer.Write((uint)geometry.Triangles.Count);

                //Write triangles
                foreach (Triangle triangle in geometry.Triangles)
                {
                    //Write Normal
                    WritePoint(writer, triangle.Normal);

                    //Write points
                    foreach (Vector3 point in triangle.Points)
                    {
                        WritePoint(writer, point);
                    }

                    //Attribute byte count
                    writer.Write(triangle.AttributeByteCount);
                }
            }
        }

        private static void WritePoint(BinaryWriter writer, Vector3 point)
        {
            writer.Write(point.X);
            writer.Write(point.Y);
            writer.Write(point.Z);
        }

        //UINT8[80]    – Header                 -     80 bytes
        //UINT32       – Number of triangles    -      4 bytes

        //foreach triangle                      - 50 bytes:
        //REAL32[3] – Normal vector             - 12 bytes
        //REAL32[3] – Vertex 1                  - 12 bytes
        //REAL32[3] – Vertex 2                  - 12 bytes
        //REAL32[3] – Vertex 3                  - 12 bytes
        //UINT16    – Attribute byte count      -  2 bytes
        //end
    }
}
