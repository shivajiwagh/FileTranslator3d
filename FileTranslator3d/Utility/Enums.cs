namespace FileTranslator3d.Utility
{
    /// <summary>
    /// Type of file to be converted
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// Obj format
        /// </summary>
        OBJ,

        /// <summary>
        /// Stl binary
        /// </summary>
        STLBINARY,

        /// <summary>
        /// Stl ascii - just for representation
        /// </summary>
        STLASCII,

        /// <summary>
        /// iges - just for representation
        /// </summary>
        IGES //Just for resprentation
    }

    /// <summary>
    /// Enum represents which axis is selected for rotation
    /// </summary>
    public enum RotationAxis
    {
        /// <summary>
        /// With respect to x axis
        /// </summary>
        X,
        /// <summary>
        /// With respect to y axis
        /// </summary>
        Y,
        /// <summary>
        /// With respect to z axis
        /// </summary>
        Z
    }
}