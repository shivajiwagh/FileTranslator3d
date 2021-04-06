using System.Collections.Generic;

namespace FileTranslator3d.Geometry
{
    public class Face
    {
        public List<int> VertexIndices { get; set; }
        public List<int> TextureIndices { get; set; }
        public List<int> NormalIndices { get; set; }
        public FaceType Facetype { get; set; }

        public Face()
        {
            VertexIndices = new List<int>();
            TextureIndices = new List<int>();
            NormalIndices = new List<int>();
        }
    }

    public enum FaceType
    {
        VertexIndices, //f v1 v2 v3 ....
        VertexTextures, //f v1/vt1 v2/vt2 v3/vt3 ...
        VertexNormals, //f v1//vn1 v2//vn2 v3//vn3 ...
        VertexTextureNormals, //f v1/vt1/vn1 v2/vt2/vn2 v3/vt3/vn3 ...
        None // in case of error
    }
}
