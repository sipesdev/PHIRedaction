namespace PHIRedaction.Services
{
    public class FileService
    {
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
    }
}