using System;
using System.IO;
using System.Numerics;
using System.Reflection;
using FileTranslator3d.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileTranslator3d.Tests
{
    /// <summary>
    /// Test case contains various test for file conversion
    /// </summary>
    [TestClass]
    public class FileTranslatorTest
    {
        #region Static Fields

        private static IFileTranslatorFacade _fileTranslator;

        #endregion

        #region Member Functions
        /// <summary>
        /// Initializes the translator
        /// </summary>
        /// <param name="context"></param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _fileTranslator = new FileTranslatorFacade();
        }

        /// <summary>
        /// Capsule model tests all the features - contains concave shape
        /// </summary>
        [TestMethod]
        public void TestReadFile1()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "capsule.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            var area = _fileTranslator.GetSurfaceArea();
            var volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 18.838191298204176);
            Assert.AreEqual(volume, 7.32143134798386);
            Assert.AreEqual(_fileTranslator.Scale(1.0f), true);

            var pip = _fileTranslator.IsPointInside(new Vector3(0, 0, 0));
            Assert.AreEqual(pip, true);
            var stateX = _fileTranslator.Rotate(RotationAxis.X, 90);
            Assert.AreEqual(stateX, true);
            var stateY = _fileTranslator.Rotate(RotationAxis.Y, 90);
            Assert.AreEqual(stateY, true);
            var stateZ = _fileTranslator.Rotate(RotationAxis.Z, 90);
            Assert.AreEqual(stateZ, true);
            var transform = _fileTranslator.Translate(5, 5, 0);
            Assert.AreEqual(transform, true);

            _fileTranslator.AddOrigin();
            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        /// <summary>
        /// Model containing convex shapes
        /// </summary>
        [TestMethod]
        public void TestReadFile2()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "12221_Cat_v1_l3.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            var area = _fileTranslator.GetSurfaceArea();
            var volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 2684.9414995740117);
            Assert.AreEqual(volume, 5645.971947009409);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);
            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        /// <summary>
        /// Another model containing convex shapes
        /// </summary>
        [TestMethod]
        public void TestReadFile3()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "cow-nonormals.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            var area = _fileTranslator.GetSurfaceArea();
            var volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 108.8815341168419);
            Assert.AreEqual(volume, 53.60122611950413);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }


        /// <summary>
        /// One move concave shape model
        /// </summary>
        [TestMethod]
        public void TestReadFile4()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "Sphere.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            var area = _fileTranslator.GetSurfaceArea();
            var volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 313.929633021052);
            Assert.AreEqual(volume, 521.7110835108883);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        /// <summary>
        /// Another obj file format
        /// </summary>
        [TestMethod]
        public void TestReadFile5()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "teddy.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            var area = _fileTranslator.GetSurfaceArea();
            var volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 3199.3151209428993);
            Assert.AreEqual(volume, 8679.564733800033);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        private string GetInputFolderPath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (directory != null)
            {
                directory = directory.Replace(@"bin\\Debug\\netcoreapp3.1", "");
                directory = Path.Combine(directory, "InputFiles");
            }

            return directory;
        }

        /// <summary>
        /// Exception while reading the file - invalid file name
        /// </summary>
        [TestMethod]
        public void TestReadException()
        {
            try
            {
                var filePath = Path.Combine(GetInputFolderPath(), "nofile.obj");
                _fileTranslator.ReadFile(filePath, "obj");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is IOException);
            }
        }

        /// <summary>
        /// Exception while calculating the volume
        /// </summary>
        [TestMethod]
        public void NullGeometryExceptionVolume()
        {
            try
            {
                var filePath = Path.Combine(GetInputFolderPath(), "capsule.obj");
                _fileTranslator.ReadFile(filePath, "obj");

                _fileTranslator.CleanupGeometry();
                _fileTranslator.GetSurfaceVolume();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidOperationException);
            }
        }

        /// <summary>
        /// Exception while calculating the area
        /// </summary>
        [TestMethod]
        public void NullGeometryExceptionArea()
        {
            try
            {
                var filePath = Path.Combine(GetInputFolderPath(), "capsule.obj");
                _fileTranslator.ReadFile(filePath, "obj");

                _fileTranslator.CleanupGeometry();
                _fileTranslator.GetSurfaceArea();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is InvalidOperationException);
            }
        }

        #endregion
    }
}