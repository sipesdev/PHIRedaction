using System.Text;
using PHIRedaction.Models;

namespace PHIRedaction.Services
{
    public class FileService
    {
        // Easy to access variable so I don't have to dig through code to change it :)
        private static readonly string _directory = "uploads";

        /// <summary>
        /// Reads the content of a file and returns it as a string
        /// </summary>
        /// <param name="file">File to read</param>
        /// <returns>Contents of the file as a string</returns>
        /// <exception cref="ArgumentNullException">Thrown when the file is null</exception>
        public static async Task<string> ReadFileContent(IFormFile file)
        {
            // Create a temporary reader to read the file content
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = await reader.ReadToEndAsync();
                return content;
            }
        }

        /// <summary>
        /// Stores the file in the upload directory
        /// </summary>
        /// <param name="fileInfo">File information to be stored</param>
        public static async Task StoreFile(FileInformation fileInfo)
        {
            // Validate input
            if (fileInfo == null || string.IsNullOrEmpty(fileInfo.FileName) || fileInfo.FileContent == null)
            {
                Console.WriteLine("FileService: Invalid FileInformation provided (null or missing FileName/FileContent).");
                // Consider throwing an ArgumentNullException or ArgumentException here
                return;
            }

            // Construct the file path
            var filePath = Path.Combine(_directory, fileInfo.FileName);

            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(_directory))
                {
                    Console.WriteLine("FileService: Directory does not exist. Creating directory...");
                    Directory.CreateDirectory(_directory);
                }

                // If the file exists, delete it
                if (File.Exists(filePath))
                {
                    // This could be changed to renaming, but for this implementation I'll delete the file
                    Console.WriteLine($"FileService: File '{filePath}' already exists. Deleting existing file...");
                    File.Delete(filePath);
                }

                // Write the file content to a new file
                await File.WriteAllTextAsync(filePath, fileInfo.FileContent, Encoding.UTF8);
                Console.WriteLine($"FileService: File '{filePath}' stored successfully.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"FileService: General Error storing file '{filePath}'. {ex.Message}");
            }
        }
    }
}