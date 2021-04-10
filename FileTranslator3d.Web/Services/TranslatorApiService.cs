using System.Net.Http;
using System.Threading.Tasks;
using FileTranslator3d.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FileTranslator3d.Web.Services
{
    /// <summary>
    /// Concrete implementation of the api service caller
    /// </summary>
    public class TranslatorApiService : ITranslatorApiService
    {
        #region Fields

        private readonly HttpClient _httpClient;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        public TranslatorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Interface Implementations
        /// <summary>
        /// Uploads the file to server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> UploadFile(HttpContent file)
        {
            return await _httpClient.PostAsync("uploadfile", file);
        }

        /// <summary>
        /// Converts the model -  main api
        /// </summary>
        /// <param name="transformation"></param>
        /// <returns></returns>
        public async Task<TransformationModel> TranslateFile(TransformationModel transformation)
        {
            return await _httpClient.PostJsonAsync<TransformationModel>("translatefile", transformation);
        }

        /// <summary>
        /// Returns the download url
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DownloadFile(string filepath)
        {
            return await _httpClient.GetAsync($"downloadfile/?filename={filepath}");
        }

        /// <summary>
        /// Model transformation api
        /// </summary>
        /// <param name="transformation"></param>
        /// <returns></returns>
        public async Task<TransformationModel> Transform(TransformationModel transformation)
        {
            return await _httpClient.PostJsonAsync<TransformationModel>("transform", transformation);
        }

        /// <summary>
        /// Api to check if the point is inside or outside
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public async Task<ActionResult<PointStateModel>> IsPointInside(PointStateModel point)
        {
            return await _httpClient.PostJsonAsync<PointStateModel>("ispointinside", point);
        }

        /// <summary>
        /// Function returns the download url
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetDownloadUrl(string filename)
        {
            return _httpClient.BaseAddress + "downloadfile/?filename=" + filename;
        }

        #endregion
    }
}