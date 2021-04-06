using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorInputFile;
using FileTranslator3d.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileTranslator3d.Web.Services
{
    public interface ITranslatorApiService
    {
        Task<HttpResponseMessage> UploadFile(HttpContent file);

        Task<TransformationModel> TranslateFile(TransformationModel transformation);

        Task<HttpResponseMessage> DownloadFile(string filepath);

        Task<TransformationModel> Transform(TransformationModel transformation);

        string GetDownloadUrl(string filename);
    }
}
