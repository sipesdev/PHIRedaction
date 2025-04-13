using System.Text;
using Microsoft.AspNetCore.Mvc;
using PHIRedaction.Models;
using PHIRedaction.Helpers;
using PHIRedaction.Interfaces;

namespace PHIRedaction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private IUploadHelper uploadHelper;

        public FileController(IUploadHelper uploadHelper)
        {
            this.uploadHelper = uploadHelper;
        }

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

            // Redact the file
            FileInformation newFileInfo = await uploadHelper.RedactFile(file);

            // Encode the file
            byte[] fileBytes = uploadHelper.EncodeFile(newFileInfo);

            // Return the new file as a downloadable file
            Console.WriteLine($"File {newFileInfo.FileName} redacted, sending to client...");
            return File(fileBytes, "text/plain", newFileInfo.FileName);
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