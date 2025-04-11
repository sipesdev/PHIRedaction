namespace PHIRedaction.Models
{
    public class FileInformation
    {
        /// <summary>
        /// The name of the file
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// The content of the file as a string
        /// </summary>
        public string? FileContent { get; set; }
    }
}