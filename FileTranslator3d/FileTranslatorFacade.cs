using System;
using System.IO;
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
            try
            {
                //Read the input file
                IFileReaderFactory readerFactory = new FileReaderFactory();
                var reader = readerFactory.GetFileReader(GetFileTypeEnum(inputFormat));
                _geometry = reader.ReadFile(inputFile);
            }
            catch (Exception ex)
            {
                throw new IOException("ReadFile: " + ex.Message);
            }
        }

        /// <summary>
        /// Writes the output file
        /// </summary>
        /// <param name="outputFile"></param>
        /// <param name="outputFormat"></param>
        public void WriteFile(string outputFile, string outputFormat)
        {
            try
            {
                //Write the output file
                IFileWriterFactory writerFactory = new FileWriterFactory();
                var writer = writerFactory.GetFileWriter(GetFileTypeEnum(outputFormat));
                writer.Write(_geometry, outputFile);
            }
            catch (Exception ex)
            {
                throw new IOException("WriteFile: " + ex.Message);
            }

        }

        /// <summary>
        /// Returns the surface area for the entire geometry
        /// </summary>
        /// <returns></returns>
        public double GetSurfaceArea()
        {
            try
            {
                return _geometry.GetSurfaceArea();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("GetSurfaceArea: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the surface volume for the entire geometry
        /// </summary>
        /// <returns></returns>
        public double GetSurfaceVolume()
        {
            try
            {
                return _geometry.GetSurfaceVolume();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("GetSurfaceVolume: " + ex.Message);
            }
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
            try
            {
                return _geometry.Translate(new Vector3(0, 0, 0), new Vector3(x, y, z));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Translate: " + ex.Message);
            }
        }

        /// <summary>
        /// Scales the geometry
        /// </summary>
        /// <param name="factor"></param>
        /// <returns></returns>
        public bool Scale(float factor)
        {
            try
            {
                return _geometry.Scale(factor);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Scale: " + ex.Message);
            }
        }

        /// <summary>
        /// Returns true if the given point is inside the convex model
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool IsPointInside(Vector3 point)
        {
            try
            {
                return _geometry.IsPointInside(point);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("IsPointInside: " + ex.Message);
            }
        }

        /// <summary>
        /// The is just for reference purpose - Just to check if it is scaling or not - adds a triangle
        /// </summary>
        public void AddOrigin()
        {
            try
            {
                _geometry.AddOrigin();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("AddOrigin: " + ex.Message);
            }
        }

        /// <summary>
        /// Rotates the geometry with respect to the given axis and angle
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public bool Rotate(RotationAxis axis, double angle)
        {
            try
            {
                return _geometry.Rotate(axis, angle);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Rotate: " + ex.Message);
            }
        }


        /// <summary>
        /// Sets the geometry to null - mainly used for unit testing
        /// </summary>
        public void CleanupGeometry()
        {
            _geometry = null;
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