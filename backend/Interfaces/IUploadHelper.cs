using PHIRedaction.Models;

namespace PHIRedaction.Interfaces
{
    public interface IUploadHelper
    {
        /// <summary>
        /// Redacts personal infomration from the given file
        /// </summary>
        /// <param name="file">File to upload and redact</param>
        Task<FileInformation> RedactFile(IFormFile file);

        /// <summary>
        /// Encodes file information to a byte array
        /// </summary>
        /// <param name="fileInfo">File information to encode</param>
        byte[] EncodeFile(FileInformation fileInfo);
    }
}