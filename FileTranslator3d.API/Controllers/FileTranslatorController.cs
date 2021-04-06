using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileTranslator3d.Models;
using FileTranslator3d.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;

namespace FileTranslator3d.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileTranslatorController : ControllerBase
    {
        #region Static Fields

        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        #endregion

        #region Fields

        private readonly ILogger<FileTranslatorController> _logger;
        private readonly IFileTranslatorFacade _fileTranslator;
        private readonly IWebHostEnvironment _environment;
        public FileArgumentOptions FileDetails { get; private set; }

        #endregion

        #region Constructor

        public FileTranslatorController(ILogger<FileTranslatorController> logger, IFileTranslatorFacade fileTranslator,
            IWebHostEnvironment environment)
        {
            _logger = logger;
            _fileTranslator = fileTranslator;
            _environment = environment;
        }

        #endregion

        #region Member Functions

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

            FileDetails = new FileArgumentOptions() {InputFileName = path, Status = "Success"};

            return Ok(FileDetails);
        }

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

        [HttpPost]
        [Route("translatefile")]
        public async Task<ActionResult<TransformationModel>> TranslateFile(TransformationModel transformation)
        {
            string outputFile = string.Empty;
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

                    await Task.Run(() => _fileTranslator.AddOrigin());

                    await Task.Run(() => _fileTranslator.WriteFile(args.OutPutFileName, args.OutPutFileType));

                    outputFile = args.OutPutFileName;
                }
            }
            catch (Exception ex)
            {
                return NotFound(new TransformationModel() { Status = false});
            }
            return Ok(new TransformationModel() { Status= true, OutPutFileName = outputFile, Area = area, Volume = volume});
        }

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
                return NotFound(new TransformationModel() {Status = false});
            }

            return Ok(new TransformationModel() { Status = true});
        }

        private FileArgumentOptions GetTranslationFileDetails(TransformationModel transformation)
        {
            FileArgumentOptions argument = new FileArgumentOptions();
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
            else return null;
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }

        #endregion
    }
}