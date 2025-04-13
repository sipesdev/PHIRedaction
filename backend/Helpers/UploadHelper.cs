using System.Text;
using Microsoft.AspNetCore.Mvc;
using PHIRedaction.Interfaces;
using PHIRedaction.Models;
using PHIRedaction.Services;

namespace PHIRedaction.Helpers
{
    public partial class UploadHelper : IUploadHelper
    {
        public UploadHelper()
        {
            // No Constructor needed in this instance
        }

        /// <summary>
        /// Redacts personal information from the given file
        /// </summary>
        /// <param name="file">File to redact from</param>
        /// <returns>Redacted file information</returns>
        public async Task<FileInformation> RedactFile(IFormFile file)
        {
            // Create a FileInformation object to store file information
            FileInformation originalFileInfo = new()
            {
                FileName = file.FileName,
                FileContent = await FileService.ReadFileContent(file)
            };
            
            // Create the new FileInformation object to store the new file information
            FileInformation newFileInfo = new()
            {
                FileName = originalFileInfo.FileName.Replace(".txt", "_redacted.txt"),
                FileContent = RedactionService.RedactText(originalFileInfo.FileContent)
            };

            // Store both files for logging
            await FileService.StoreFile(originalFileInfo);
            await FileService.StoreFile(newFileInfo);
            
            // Return the new file information
            return newFileInfo;
        }

        /// <summary>
        /// Encodes file information to a byte array
        /// </summary>
        /// <param name="fileInfo">File information to be encoded</param>
        /// <returns>File content in bytes</returns>
        /// <exception cref="ArgumentException"></exception>
        public byte[] EncodeFile(FileInformation fileInfo)
        {
            // Check if file information is null
            if (fileInfo == null || string.IsNullOrEmpty(fileInfo.FileContent))
            {
                throw new ArgumentException("File information is invalid."); // This shouldn't ever happen but just in case
            }

            // Convert the file content to a byte array
            byte[] fileBytes = Encoding.UTF8.GetBytes(fileInfo.FileContent);
            return fileBytes;
        }
    }
}