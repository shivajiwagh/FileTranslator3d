namespace FileTranslator3d.Models
{
    /// <summary>
    /// Model represents the file conversion along with the transformation details
    /// </summary>
    public class TransformationModel
    {
        #region Properties
        /// <summary>
        /// Scale factor
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Rotation angle
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Rotation axis X
        /// </summary>
        public float OriginX { get; set; }

        /// <summary>
        /// Rotation axis Y
        /// </summary>
        public float OriginY { get; set; }

        /// <summary>
        /// Rotation axis Z
        /// </summary>
        public float OriginZ { get; set; }

        /// <summary>
        /// Return status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Type of input file
        /// </summary>
        public string OutPutFileType { get; set; }

        /// <summary>
        /// Path of the input file
        /// </summary>
        public string InputFilePath { get; set; }

        /// <summary>
        /// Output file path
        /// </summary>
        public string OutPutFileName { get; set; }

        /// <summary>
        /// Area
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Rotation axis in string format
        /// </summary>
        public string RotationAxis { get; set; }

        /// <summary>
        /// Status message in case or exception or errors
        /// </summary>
        public string Message { get; set; }

        #endregion
    }
}