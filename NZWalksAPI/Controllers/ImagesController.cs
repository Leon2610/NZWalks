using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImagesUploadRequestDTO request)
        {
            ValidateFileUpload(request);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


        }

        private void ValidateFileUpload(ImagesUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size exceeds 10MB, please upload a smaller size file");
            }
        }
    }
}
