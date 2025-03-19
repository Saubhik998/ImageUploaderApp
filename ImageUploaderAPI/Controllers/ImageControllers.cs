using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using ImageUploaderAPI.Services;
using ImageUploaderAPI.Models;
using System.Collections.Generic;

namespace ImageUploaderAPI.Controllers
{
    /// <summary>
    /// Controller for handling image upload, retrieval, and deletion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;

        /// <summary>
        /// Constructor to initialize ImageService dependency.
        /// </summary>
        /// <param name="imageService">The service responsible for handling image operations.</param>
        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        /// <summary>
        /// Uploads an image file to the server.
        /// </summary>
        /// <param name="file">The image file uploaded by the user.</param>
        /// <returns>Returns the image ID and a success message.</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Check if file is null or empty
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Read the file stream and upload
            using var stream = file.OpenReadStream();
            var imageId = await _imageService.UploadImageAsync(file.FileName, file.ContentType, stream);

            return Ok(new { Id = imageId, Message = "Image uploaded successfully." });
        }

        /// <summary>
        /// Retrieves a list of all uploaded images.
        /// </summary>
        /// <returns>Returns a list of image metadata.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ImageModel>>> GetImages()
        {
            return await _imageService.GetAllImagesAsync();
        }

        /// <summary>
        /// Retrieves an image by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the image.</param>
        /// <returns>Returns the image file if found; otherwise, returns 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            // Retrieve image data by ID
            var imageData = await _imageService.GetImageByIdAsync(id);
            if (imageData == null)
                return NotFound("Image not found.");

            // Return the image file
            return File(imageData, "image/jpeg");
        }

        /// <summary>
        /// Deletes an image by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the image.</param>
        /// <returns>Returns success or error message based on the deletion result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(string id)
        {
            // Attempt to delete the image
            var deleted = await _imageService.DeleteImageAsync(id);
            return deleted ? Ok("Image deleted successfully.") : NotFound("Image not found.");
        }
    }
}
