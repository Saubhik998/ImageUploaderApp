using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageUploaderAPI.Models;

namespace ImageUploaderAPI.Services
{
    /// <summary>
    /// Service for handling image storage, retrieval, and deletion using MongoDB GridFS.
    /// </summary>
    public class ImageService
    {
        private readonly IGridFSBucket _gridFS;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="database">MongoDB database instance.</param>
        public ImageService(IMongoDatabase database)
        {
            _gridFS = new GridFSBucket(database);
        }

        /// <summary>
        /// Uploads an image to MongoDB GridFS.
        /// </summary>
        /// <param name="fileName">The name of the file being uploaded.</param>
        /// <param name="contentType">The MIME type of the file (e.g., image/jpeg, image/png).</param>
        /// <param name="fileStream">The file stream containing image data.</param>
        /// <returns>The unique identifier (ObjectId) of the uploaded file as a string.</returns>
        public async Task<string> UploadImageAsync(string fileName, string contentType, Stream fileStream)
        {
            var uploadOptions = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "ContentType", contentType }
                }
            };

            ObjectId fileId = await _gridFS.UploadFromStreamAsync(fileName, fileStream, uploadOptions);
            return fileId.ToString();
        }

        /// <summary>
        /// Retrieves an image by its unique identifier.
        /// </summary>
        /// <param name="id">The ObjectId of the image.</param>
        /// <returns>The image as a byte array, or null if not found.</returns>
        public async Task<byte[]> GetImageByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return null;

            try
            {
                return await _gridFS.DownloadAsBytesAsync(objectId);
            }
            catch (GridFSFileNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves all images stored in MongoDB GridFS.
        /// </summary>
        /// <returns>A list of image metadata, including ID, filename, and content type.</returns>
        public async Task<List<ImageModel>> GetAllImagesAsync()
        {
            var filter = Builders<GridFSFileInfo>.Filter.Empty;
            using var cursor = await _gridFS.FindAsync(filter);
            var files = await cursor.ToListAsync();

            return files.ConvertAll(file => new ImageModel
            {
                Id = file.Id.ToString(),
                FileName = file.Filename,
                ContentType = file.Metadata != null && file.Metadata.Contains("ContentType") 
                    ? file.Metadata["ContentType"].AsString 
                    : "unknown"
            });
        }

        /// <summary>
        /// Deletes an image from MongoDB GridFS.
        /// </summary>
        /// <param name="id">The ObjectId of the image to delete.</param>
        /// <returns>True if the image was successfully deleted, false otherwise.</returns>
        public async Task<bool> DeleteImageAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
                return false;

            try
            {
                await _gridFS.DeleteAsync(objectId);
                return true;
            }
            catch (GridFSFileNotFoundException)
            {
                return false;
            }
        }
    }
}
