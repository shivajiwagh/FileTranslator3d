using System.Collections.Generic;

namespace FileTranslator3d.Geometry
{
    /// <summary>
    /// Face class - a face can hold more than one triangles
    /// </summary>
    public class Face
    {
        #region Constructor

        /// <summary>
        /// Constructor - initializes the properties
        /// </summary>
        public Face()
        {
            VertexIndices = new List<int>();
            TextureIndices = new List<int>();
            NormalIndices = new List<int>();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Stores the vertices indexes
        /// </summary>
        public List<int> VertexIndices { get; set; }

        /// <summary>
        /// Stores the texture indexes
        /// </summary>
        public List<int> TextureIndices { get; set; }

        /// <summary>
        /// Stores the normal indexes
        /// </summary>
        public List<int> NormalIndices { get; set; }

        /// <summary>
        /// Holds the type of face based on enum
        /// </summary>
        public FaceType FaceType { get; set; }

        #endregion
    }

    /// <summary>
    /// Enum represents the type of face - based on obj file format 
    /// </summary>
    public enum FaceType
    {
        /// <summary>
        /// Represents vertex only - f v1 v2 v3 ....
        /// </summary>
        VertexIndices,

        /// <summary>
        /// Represents vertex and textures - f v1/vt1 v2/vt2 v3/vt3 ...
        /// </summary>
        VertexTextures,

        /// <summary>
        /// Represents vertex and normals - f v1//vn1 v2//vn2 v3//vn3 ...
        /// </summary>
        VertexNormals,

        /// <summary>
        /// Face type represents vertex, textures and normals - f v1/vt1/vn1 v2/vt2/vn2 v3/vt3/vn3 ...
        /// </summary>
        VertexTextureNormals, //

        /// <summary>
        /// If no matching format found
        /// </summary>
        None // in case of error
    }
}