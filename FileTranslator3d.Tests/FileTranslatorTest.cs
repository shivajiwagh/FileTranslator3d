using System.IO;
using FileTranslator3d.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileTranslator3d.Tests
{
    [TestClass]
    public class FileTranslatorTest
    {
        private static IFileTranslatorFacade _fileTranslator;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _fileTranslator = new FileTranslatorFacade();
        }

        [TestMethod]
        public void TestReadFile1()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "capsule.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            double area = _fileTranslator.GetSurfaceArea();
            double volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 18.838191298204176);
            Assert.AreEqual(volume, 7.32143134798386);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);
            var stateX = _fileTranslator.Rotate(RotationAxis.X, 90);
            var stateY = _fileTranslator.Rotate(RotationAxis.Y, 90);
            var stateZ = _fileTranslator.Rotate(RotationAxis.Z, 90);
            var transform = _fileTranslator.Translate(5,5,5);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        [TestMethod]
        public void TestReadFile2()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "12221_Cat_v1_l3.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            double area = _fileTranslator.GetSurfaceArea();
            double volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 2684.9414995740117);
            Assert.AreEqual(volume, 5645.971947009409);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);
            var stateX = _fileTranslator.Rotate(RotationAxis.X, 90);
            var stateY = _fileTranslator.Rotate(RotationAxis.Y, 90);
            var stateZ = _fileTranslator.Rotate(RotationAxis.Z, 90);
            var transform = _fileTranslator.Translate(5, 5, 5);
            _fileTranslator.AddOrigin();
            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        [TestMethod]
        public void TestReadFile3()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "cow-nonormals.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            double area = _fileTranslator.GetSurfaceArea();
            double volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 108.8815341168419);
            Assert.AreEqual(volume, 53.60122611950413);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);
            var stateX = _fileTranslator.Rotate(RotationAxis.X, 90);
            var stateY = _fileTranslator.Rotate(RotationAxis.Y, 90);
            var stateZ = _fileTranslator.Rotate(RotationAxis.Z, 90);
            var transform = _fileTranslator.Translate(5, 5, 5);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        [TestMethod]
        public void TestReadFile4()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "teapot.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            double area = _fileTranslator.GetSurfaceArea();
            double volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 52.66079023783854);
            Assert.AreEqual(volume, 25.77010579694139);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);
            var stateX = _fileTranslator.Rotate(RotationAxis.X, 90);
            var stateY = _fileTranslator.Rotate(RotationAxis.Y, 90);
            var stateZ = _fileTranslator.Rotate(RotationAxis.Z, 90);
            var transform = _fileTranslator.Translate(5, 5, 5);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        [TestMethod]
        public void TestReadFile5()
        {
            var filePath = Path.Combine(GetInputFolderPath(), "teddy.obj");
            _fileTranslator.ReadFile(filePath, "obj");

            double area = _fileTranslator.GetSurfaceArea();
            double volume = _fileTranslator.GetSurfaceVolume();
            Assert.AreEqual(area, 3199.3151209428993);
            Assert.AreEqual(volume, 8679.564733800033);
            Assert.AreEqual(_fileTranslator.Scale(2.0f), true);
            var stateX = _fileTranslator.Rotate(RotationAxis.X, 90);
            var stateY = _fileTranslator.Rotate(RotationAxis.Y, 90);
            var stateZ = _fileTranslator.Rotate(RotationAxis.Z, 90);
            var transform = _fileTranslator.Translate(5, 5, 5);

            var outPutFile = filePath.Replace(".obj", ".stl");
            _fileTranslator.WriteFile(outPutFile, "stl");
        }

        private string GetInputFolderPath()
        {
            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (directory != null)
            {
                directory = directory.Replace(@"bin\\Debug\\netcoreapp3.1", "");
                directory = Path.Combine(directory, "InputFiles");
            }

            return directory;
        }
    }
}
