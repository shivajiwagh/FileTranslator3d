using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using FileTranslator3d.Models;
using FileTranslator3d.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FileTranslator3d.API.Controllers
{
    /// <summary>
    /// API controller class 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FileTranslatorController : ControllerBase
    {
        #region Fields

        private readonly IWebHostEnvironment _environment;
        private readonly IFileTranslatorFacade _fileTranslator;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor - initializes the translator and environment
        /// </summary>
        /// <param name="fileTranslator"></param>
        /// <param name="environment"></param>
        public FileTranslatorController(IFileTranslatorFacade fileTranslator,
            IWebHostEnvironment environment)
        {
            _fileTranslator = fileTranslator;
            _environment = environment;
        }

        #endregion

        #region Properties
        /// <summary>
        /// File Argument options - for file name and type of file
        /// </summary>
        public FileArgumentOptions FileDetails { get; private set; }

        #endregion

        #region Member Functions

        /// <summary>
        /// Uploads the file to server
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("uploadfile")]
        public async Task<ActionResult<FileArgumentOptions>> UploadFile()
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();

            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(_environment.ContentRootPath, "wwwroot", file.FileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            FileDetails = new FileArgumentOptions {InputFileName = path, Status = "Success"};

            return Ok(FileDetails);
        }

        /// <summary>
        /// Download the file after conversion
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [Route("downloadfile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            //var path = Path.Combine(Directory.GetCurrentDirectory(),
            //    "wwwroot", filename);

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, GetContentType(filename), Path.GetFileName(filename));
        }

        /// <summary>
        /// api to convert the file in the given format 
        /// </summary>
        /// <param name="transformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("translatefile")]
        public async Task<ActionResult<TransformationModel>> TranslateFile(TransformationModel transformation)
        {
            var outputFile = string.Empty;
            double area = 0;
            double volume = 0;
            try
            {
                var args = GetTranslationFileDetails(transformation);
                if (args != null)
                {
                    await Task.Run(() =>
                        _fileTranslator.ReadFile(args.InputFileName, args.InputFileType));
                    await Task.Run(() => area = _fileTranslator.GetSurfaceArea());
                    await Task.Run(() => volume = _fileTranslator.GetSurfaceVolume());

                    Enum.TryParse(transformation.RotationAxis, out RotationAxis axis);

                    await Task.Run(() => _fileTranslator.Scale(transformation.Scale));

                    await Task.Run(() => _fileTranslator.Rotate(axis, transformation.Rotation));

                    await Task.Run(() =>
                        _fileTranslator.Translate(transformation.OriginX, transformation.OriginY,
                            transformation.OriginZ));

                    //await Task.Run(() => _fileTranslator.AddOrigin());

                    await Task.Run(() => _fileTranslator.WriteFile(args.OutPutFileName, args.OutPutFileType));

                    outputFile = args.OutPutFileName;
                }
            }
            catch (Exception ex)
            {
                return NotFound(new TransformationModel {Status = false, Message = ex.ToString()});
            }

            return Ok(new TransformationModel
                {Status = true, OutPutFileName = outputFile, Area = area, Volume = volume});
        }

        /// <summary>
        /// Api to check if the given point is inside or outside the 3d surface
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ispointinside")]
        public async Task<ActionResult<PointStateModel>> IsPointInside(PointStateModel point)
        {
            bool state;
            try
            {
                state = await Task.Run(() => _fileTranslator.IsPointInside(new Vector3(point.X, point.Y, point.Z)));
            }
            catch (Exception ex)
            {
                return NotFound(new PointStateModel {Inside = false, Message = ex.ToString()});
            }

            return Ok(new PointStateModel {Inside = state});
        }


        /// <summary>
        /// Api to move geometry to the specified point - base point is center
        /// </summary>
        /// <param name="transformation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("transform")]
        public async Task<ActionResult<TransformationModel>> Transform(TransformationModel transformation)
        {
            try
            {
                await Task.Run(() =>
                    _fileTranslator.Translate(transformation.OriginX, transformation.OriginY, transformation.OriginZ));
            }
            catch (Exception ex)
            {
                return NotFound(new TransformationModel {Status = false, Message = ex.ToString()});
            }

            return Ok(new TransformationModel {Status = true});
        }

        private FileArgumentOptions GetTranslationFileDetails(TransformationModel transformation)
        {
            var argument = new FileArgumentOptions();
            if (transformation != null)
            {
                argument.InputFileName = transformation.InputFilePath;
                var ext = Path.GetExtension(transformation.InputFilePath);
                if (ext != null) argument.InputFileType = ext.Substring(1, ext.Length - 1);
                var directory = Path.GetDirectoryName(transformation.InputFilePath);
                argument.OutPutFileName = Path.Combine(directory ?? throw new InvalidOperationException(),
                    Path.GetFileNameWithoutExtension(transformation.InputFilePath) + "." +
                    transformation.OutPutFileType.ToLower());
                argument.OutPutFileType = transformation.OutPutFileType;
                return argument;
            }

            return null;
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".stl", "application/stl"},
                {".iges", "application/iges"},
                {".step", "application/step"},
                {".obj", "application/obj"}
            };
        }

        #endregion
    }
}