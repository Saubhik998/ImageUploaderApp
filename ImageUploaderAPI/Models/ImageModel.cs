using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ImageUploaderAPI.Models
{
    /// <summary>
    /// Represents an image model stored in the MongoDB database.
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// Unique identifier for the image (MongoDB ObjectId).
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Name of the uploaded file.
        /// </summary>
        [BsonElement("FileName")]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// MIME type of the image (e.g., image/jpeg, image/png).
        /// </summary>
        [BsonElement("ContentType")]
        public string ContentType { get; set; } = string.Empty;
    }
}
