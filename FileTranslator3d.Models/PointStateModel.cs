namespace FileTranslator3d.Models
{

    /// <summary>
    /// Model class represent the point status
    /// </summary>
    public class PointStateModel
    {
        #region Properties
        /// <summary>
        /// Returns if it is inside
        /// </summary>
        public bool Inside { get; set; }

        /// <summary>
        /// X coordinate value of the point
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y coordinate value of the point
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Z coordinate value of the point
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Error message in case of exception
        /// </summary>
        public string Message { get; set; }

        #endregion
    }
}