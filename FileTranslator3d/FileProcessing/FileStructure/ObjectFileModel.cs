using System.Collections.Generic;
using System.Numerics;
using FileTranslator3d.Geometry;

namespace FileTranslator3d.FileProcessing.FileStructure
{
    public class ObjectFileModel : IFileModel
    {
        #region Constructor

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
        public List<Vector3> Vertices { get; set; } //v element
        public List<Face> Faces { get; set; } //f element
        public List<Vector3> Normals { get; set; } // vn element
        public List<Vector3> Textures { get; set; } // vt element

        #endregion

        #region Interface Implementations

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
                triangle.Points.Add(Vertices[face.VertexIndices[0] - 1]);
                triangle.Points.Add(Vertices[face.VertexIndices[1] - 1]);
                triangle.Points.Add(Vertices[face.VertexIndices[2] - 1]);

                //Vector3 p1 = Vertices[face.VertexIndices[0] - 1];
                //Vector3 p2 = Vertices[face.VertexIndices[1] - 1];
                //Vector3 p3 = Vertices[face.VertexIndices[2] - 1];
                //triangle.Points.Add(p1);
                //triangle.Points.Add(p2);
                //triangle.Points.Add(p3);
                //triangle.AddNormal(p1, p2, p3);
                ////Normal
                if (face.NormalIndices.Count == 3)
                {
                    var p1 = Normals[face.NormalIndices[0] - 1];
                    var p2 = Normals[face.NormalIndices[1] - 1];
                    var p3 = Normals[face.NormalIndices[2] - 1];
                    triangle.AddNormal(p1, p2, p3);
                }

                mesh.Triangles.Add(triangle);
            }
            else if (face.VertexIndices.Count > 3)
            {
                for (var i = 1; i < face.VertexIndices.Count - 1; i++)
                {
                    //Points
                    var triangle = new Triangle();
                    triangle.Points.Add(Vertices[face.VertexIndices[0] - 1]);
                    triangle.Points.Add(Vertices[face.VertexIndices[i] - 1]);
                    triangle.Points.Add(Vertices[face.VertexIndices[i + 1] - 1]);

                    //Normal
                    if (face.NormalIndices.Count > 3)
                    {
                        var p1 = Normals[face.NormalIndices[0] - 1];
                        var p2 = Normals[face.NormalIndices[i] - 1];
                        var p3 = Normals[face.NormalIndices[i + 1] - 1];
                        triangle.AddNormal(p1, p2, p3);
                    }

                    mesh.Triangles.Add(triangle);
                }
            }
        }

        #endregion
    }
}