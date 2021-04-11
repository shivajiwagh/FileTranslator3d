using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using FileTranslator3d.FileProcessing.FileStructure;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.Reader
{
    /// <summary>
    /// Concrete implementation of the reader
    /// </summary>
    public class ObjReader : IFileReader
    {
        #region Interface Implementations

        /// <summary>
        /// Reads the specified obj file and return geometry model
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public IPrimitive ReadFile(string filename)
        {
            var objModel = new ObjectFileModel();

            using (var sr = new StreamReader(File.Open(filename, FileMode.Open)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line)) continue;

                    var lines = line.Trim().Split(' ');
                    var result = (from item in lines
                        where !string.IsNullOrEmpty(item)
                        select item).ToArray();

                    switch (lines[0])
                    {
                        //The OBJ file support should be limited to v, vt, vn and f elements.
                        case "#": continue;
                        case "v":
                            ParseVertex(result, objModel);
                            break;
                        case "vt":
                            ParseTexture(result, objModel);
                            break;
                        case "vn":
                            ParseNormals(result, objModel);
                            break;
                        case "f":
                            ParseFaces(result, objModel);
                            break;
                    }
                }
            }

            return objModel.GetGeometryModel();
        }

        #endregion

        #region Member Functions

        private void ParseVertex(string[] lines, ObjectFileModel objModel)
        {
            if (lines.Length == 4)
                objModel.Vertices.Add(GetVertexFromCoordinates(lines));
            else
                throw new FormatException($"Failed to parse vertex information at line {string.Concat(lines)}");
        }

        private Vector3 GetVertexFromCoordinates(string[] lines)
        {
            if (lines.Length == 4)
            {
                var statusX = float.TryParse(lines[1], out var x);
                var statusY = float.TryParse(lines[2], out var y);
                var statusZ = float.TryParse(lines[3], out var z);
                if (statusX && statusY && statusZ) return new Vector3(x, y, z);
            }
            else if (lines.Length == 3)
            {
                var statusX = float.TryParse(lines[1], out var x);
                var statusY = float.TryParse(lines[2], out var y);
                if (statusX && statusY) return new Vector3(x, y, 1.0f);
            }

            throw new FormatException($"Error converting co-ordinates at line {string.Concat(lines)}");
        }

        private void ParseTexture(string[] lines, ObjectFileModel objModel)
        {
            if (lines.Length >= 3)
                objModel.Textures.Add(GetVertexFromCoordinates(lines));
            else
                throw new FormatException($"Failed to parse texture information at line {string.Concat(lines)}");
        }

        private void ParseNormals(string[] lines, ObjectFileModel objModel)
        {
            if (lines.Length == 4)
                objModel.Normals.Add(GetVertexFromCoordinates(lines));
            else
                throw new FormatException($"Failed to parse normals information at line {string.Concat(lines)}");
        }

        private void ParseFaces(string[] lines, ObjectFileModel objModel)
        {
            var type = GetFacetype(lines[1]);
            if (type == FaceType.None)
                throw new FormatException($"Incorrect face format found at {string.Concat(lines)}");

            var face = new Face
            {
                FaceType = type
            };
            foreach (var line in lines.Skip(1))
                switch (type)
                {
                    case FaceType.VertexIndices:
                        face.VertexIndices.Add(ParseNumber(line));
                        break;
                    case FaceType.VertexNormals:
                        ParseVertexAndNormals(line, face);
                        break;
                    case FaceType.VertexTextures:
                        ParseVertexAndTextures(line, face);
                        break;
                    case FaceType.VertexTextureNormals:
                        ParseVertexTexturesAndNormals(line, face);
                        break;
                }

            objModel.Faces.Add(face);
        }

        private int ParseNumber(string str)
        {
            int.TryParse(str, out var number);
            return number;
        }

        private void ParseVertexAndNormals(string facestr, Face face)
        {
            var items = facestr.Split('/');
            face.VertexIndices.Add(ParseNumber(items[0]));
            face.NormalIndices.Add(ParseNumber(items[2]));
        }

        private void ParseVertexAndTextures(string facestr, Face face)
        {
            var items = facestr.Split('/');
            face.VertexIndices.Add(ParseNumber(items[0]));
            face.TextureIndices.Add(ParseNumber(items[2]));
        }

        private void ParseVertexTexturesAndNormals(string facestr, Face face)
        {
            var items = facestr.Split('/');
            face.VertexIndices.Add(ParseNumber(items[0]));
            face.TextureIndices.Add(ParseNumber(items[1]));
            face.NormalIndices.Add(ParseNumber(items[2]));
        }

        private FaceType GetFacetype(string firstFace)
        {
            /*  https://en.wikipedia.org/wiki/Wavefront_.obj_file#Face_elements
               # Polygonal face element (see below)
              f 1 2 3              -- Vertex indices 
              f 3/1 4/2 5/3        -- Vertex texture coordinate indices  
              f 6/4/1 3/5/3 7/6/5  -- Vertex normal indices 
              f 7//1 8//2 9//3     -- Vertex normal indices without texture coordinate indices  
              https://coffeespace.org.uk/projects/javafx-import-stl-and-obj.html   */

            var items = firstFace.Split('/');
            if (items.Length == 2) return FaceType.VertexTextures;

            if (items.Length == 1) return FaceType.VertexIndices;

            if (items.Length == 3)
            {
                if (string.IsNullOrEmpty(items[1]))
                    return FaceType.VertexNormals;
                return FaceType.VertexTextureNormals;
            }

            return FaceType.None;
        }

        #endregion
    }
}