using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorInputFile;
using FileTranslator3d.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FileTranslator3d.Web.Services
{
    public class TranslatorApiService : ITranslatorApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TranslatorApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        #region Implementation of ITranslatorApiService

        public async Task<HttpResponseMessage> UploadFile(HttpContent file)
        {
            return await _httpClient.PostAsync("uploadfile", file);
        }

        public async Task<TransformationModel> TranslateFile(TransformationModel transformation)
        {
            return await _httpClient.PostJsonAsync<TransformationModel>("translatefile", transformation);
        }

        public async Task<HttpResponseMessage> DownloadFile(string filepath)
        {
            return await _httpClient.GetAsync($"downloadfile/?filename={filepath}");
        }

        public async Task<TransformationModel> Transform(TransformationModel transformation)
        {
            return await _httpClient.PostJsonAsync<TransformationModel>("transform", transformation);
        }

        public string GetDownloadUrl(string filename)
        {
            return _httpClient.BaseAddress.ToString() + "downloadfile/?filename=" + filename;
        }

        #endregion
    }
}
