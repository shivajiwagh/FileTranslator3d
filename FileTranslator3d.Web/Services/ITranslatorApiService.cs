using System.Net.Http;
using System.Threading.Tasks;
using FileTranslator3d.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileTranslator3d.Web.Services
{
    /// <summary>
    /// Interface for calling the rest apis
    /// </summary>
    public interface ITranslatorApiService
    {
        #region Member Functions
        /// <summary>
        /// Uploads the file to server
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UploadFile(HttpContent file);

        /// <summary>
        /// Converts the model -  main api
        /// </summary>
        /// <param name="transformation"></param>
        /// <returns></returns>
        Task<TransformationModel> TranslateFile(TransformationModel transformation);

        /// <summary>
        /// Returns the download url
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DownloadFile(string filepath);

        /// <summary>
        /// Model transformation api
        /// </summary>
        /// <param name="transformation"></param>
        /// <returns></returns>
        Task<TransformationModel> Transform(TransformationModel transformation);

        /// <summary>
        /// Api to check if the point is inside or outside
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        Task<ActionResult<PointStateModel>> IsPointInside(PointStateModel point);

        /// <summary>
        /// Function returns the download url
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetDownloadUrl(string filename);

        #endregion
    }
}