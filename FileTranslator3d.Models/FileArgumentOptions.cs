namespace FileTranslator3d.Models
{
    /// <summary>
    /// Model class represents file arguments
    /// </summary>
    public class FileArgumentOptions
    {
        #region Properties
        /// <summary>
        /// Name and path of the input file
        /// </summary>
        public string InputFileName { get; set; }

        /// <summary>
        /// Type of the input file - obj or stl
        /// </summary>
        public string InputFileType { get; set; }

        /// <summary>
        /// Name and path of the output file
        /// </summary>
        public string OutPutFileName { get; set; }

        /// <summary>
        /// Type of the output file - obj or stl
        /// </summary>
        public string OutPutFileType { get; set; }

        /// <summary>
        /// Status for returning from the api
        /// </summary>
        public string Status { get; set; }

        #endregion
    }
}