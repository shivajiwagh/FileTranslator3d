using System.Collections.Generic;
using System.Numerics;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.FileStructure
{
     /// <summary>
     /// Concrete implementation of IFileModel
     /// </summary>
    public class ObjectFileModel : IFileModel
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectFileModel()
        {
            Faces = new List<Face>();
            Vertices = new List<Vector3>();
            Normals = new List<Vector3>();
            Textures = new List<Vector3>();
        }

        #endregion

        #region Properties

        //The OBJ file support should be limited to v, vt, vn and f elements.
        /// <summary>
        /// Holds the vertices - Vector3 class represents a point
        /// v element in Obj file
        /// </summary>
        public List<Vector3> Vertices { get; set; }

        /// <summary>
        /// f element - face in obj file
        /// </summary>
        public List<Face> Faces { get; set; }

        /// <summary>
        /// vn element - represents all normals
        /// </summary>
        public List<Vector3> Normals { get; set; }

        /// <summary>
        /// vt element - holds texture coordinates
        /// </summary>
        public List<Vector3> Textures { get; set; }

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Returns the generic geometry model
        /// </summary>
        /// <returns></returns>
        public IPrimitive GetGeometryModel()
        {
            var mesh = new BrepGeometry();
            foreach (var face in Faces) TriangulateFace(face, mesh);
            return mesh;
        }

        #endregion

        #region Member Functions

        private void TriangulateFace(Face face, BrepGeometry mesh)
        {
            if (face.VertexIndices.Count == 3)
            {
                //Points
                var triangle = new Triangle();
                var p1 = Vertices[face.VertexIndices[0] - 1];
                var p2 = Vertices[face.VertexIndices[1] - 1];
                var p3 = Vertices[face.VertexIndices[2] - 1];
                triangle.Points.Add(p1);
                triangle.Points.Add(p2);
                triangle.Points.Add(p3);

                ////Normal
                if (face.NormalIndices.Count > 0)
                    triangle.Normal = Normals[face.NormalIndices[0] - 1];
                else
                    triangle.AddNormal(p1, p2, p3);

                mesh.Triangles.Add(triangle);
            }
            else if (face.VertexIndices.Count > 3)
            {
                for (var i = 1; i < face.VertexIndices.Count - 1; i++)
                {
                    //Points
                    var triangle = new Triangle();
                    var p1 = Vertices[face.VertexIndices[0] - 1];
                    var p2 = Vertices[face.VertexIndices[i] - 1];
                    var p3 = Vertices[face.VertexIndices[i + 1] - 1];

                    triangle.Points.Add(p1);
                    triangle.Points.Add(p2);
                    triangle.Points.Add(p3);

                    //Normal
                    if (face.NormalIndices.Count > 0)
                        triangle.Normal = Normals[face.NormalIndices[0] - 1];
                    else
                        triangle.AddNormal(p1, p2, p3);

                    mesh.Triangles.Add(triangle);
                }
            }
        }

        #endregion
    }
}