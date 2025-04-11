using Microsoft.AspNetCore.Mvc;
using PHIRedaction.Models;
using PHIRedaction.Services;

namespace PHIRedaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        /// <summary>
        /// Uploads a file to the server
        /// </summary>
        /// <param name="file">File to upload and analyze</param>
        [HttpPost("upload")] // POST api/File/upload
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // Check if file is null or empty
            if (file == null || file.Length == 0)
            {
                var message = "No file provided, or file is empty.";
                Console.Error.WriteLine(message);
                return BadRequest(message);
            }

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
                FileContent = await RedactionService.RedactText(originalFileInfo.FileContent)
            };
            
            return FileStreamResult();
        }

        /// <summary>
        /// Simple test endpoint to make sure API is working
        /// </summary>
        [HttpGet("test")] // GET api/File/test
        public IActionResult Test()
        {
            return Ok("Test endpoint is working.");
        }
    }
}