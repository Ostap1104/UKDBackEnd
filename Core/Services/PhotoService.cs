using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder = "general")
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folder
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Upload to Cloudinary failed");

            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl) || !Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                return true;
            }

            try
            {
                var uri = new Uri(imageUrl);
                var pathSegments = uri.AbsolutePath.Split('/');

                int uploadIndex = Array.IndexOf(pathSegments, "upload");

                if (uploadIndex == -1 || uploadIndex + 2 >= pathSegments.Length)
                    return false;

                var afterUpload = pathSegments.Skip(uploadIndex + 1).ToList();

                if (afterUpload[0].StartsWith("v"))
                {
                    afterUpload.RemoveAt(0);
                }

                var publicId = string.Join("/", afterUpload)
                    .Replace(".jpg", "")
                    .Replace(".png", "")
                    .Replace(".jpeg", "");

                var deletionParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deletionParams);

                return result.Result == "ok" || result.Result == "not_found";
            }
            catch
            {
                return false;
            }
        }



    }

}
