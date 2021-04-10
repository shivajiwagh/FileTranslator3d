using System.Numerics;
using FileTranslator3d.FileProcessing;
using FileTranslator3d.FileProcessing.Reader;
using FileTranslator3d.FileProcessing.Writer;
using FileTranslator3d.Geometry;
using FileTranslator3d.Utility;

namespace FileTranslator3d
{
    /// <summary>
    /// Single class represents the entire file conversion functionality
    /// </summary>
    public class FileTranslatorFacade : IFileTranslatorFacade
    {
        #region Fields

        private IPrimitive _geometry;

        #endregion

        #region Interface Implementations

        /// <summary>
        /// Reads the given file
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="inputFormat"></param>
        public void ReadFile(string inputFile, string inputFormat)
        {
            //Read the input file
            IFileReaderFactory readerFactory = new FileReaderFactory();
            var reader = readerFactory.GetFileReader(GetFileTypeEnum(inputFormat));
            _geometry = reader.ReadFile(inputFile);
        }

        /// <summary>
        /// Writes the output file
        /// </summary>
        /// <param name="outputFile"></param>
        /// <param name="outputFormat"></param>
        public void WriteFile(string outputFile, string outputFormat)
        {
            //Write the output file
            IFileWriterFactory writerFactory = new FileWriterFactory();
            var writer = writerFactory.GetFileWriter(GetFileTypeEnum(outputFormat));
            writer.Write(_geometry, outputFile);
        }

        /// <summary>
        /// Returns the surface area for the entire geometry
        /// </summary>
        /// <returns></returns>
        public double GetSurfaceArea()
        {
            return _geometry.GetSurfaceArea();
        }

        /// <summary>
        /// Returns the surface volume for the entire geometry
        /// </summary>
        /// <returns></returns>
        public double GetSurfaceVolume()
        {
            return _geometry.GetSurfaceVolume();
        }

        /// <summary>
        /// Moves the entire geometry from one point to another
        /// Here from point is considered as center for reference
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public bool Translate(float x, float y, float z)
        {
            return _geometry.Translate(new Vector3(0, 0, 0), new Vector3(x, y, z));
        }

        /// <summary>
        /// Scales the geometry
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public bool Scale(float factor)
        {
            return _geometry.Scale(factor);
        }

        /// <summary>
        /// Returns true if the given point is inside the convex model
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointInside(Vector3 point)
        {
            return _geometry.IsPointInside(point);
        }

        /// <summary>
        /// The is just for reference purpose - Just to check if it is scaling or not - adds a triangle
        /// </summary>
        public void AddOrigin()
        {
            _geometry.AddOrigin();
        }

        /// <summary>
        /// Rotates the geometry with respect to the given axis and angle
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public bool Rotate(RotationAxis axis, double angle)
        {
            return _geometry.Rotate(axis, angle);
        }

        #endregion

        #region Member Functions
        private FileType GetFileTypeEnum(string format)
        {
            var type = FileType.STLBINARY;
            //var ext = Path.GetExtension(filename).ToLowerInvariant();
            switch (format)
            {
                case "obj":
                    type = FileType.OBJ;
                    break;
                case "stl":
                    type = FileType.STLBINARY;
                    break;
                case "iges":
                    type = FileType.IGES;
                    break;
            }

            return type;
        }

        #endregion
    }
}