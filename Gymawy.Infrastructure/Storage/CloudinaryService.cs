using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ErrorOr;
using Gymawy.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace Gymawy.Infrastructure.Storage
{
    public class CloudinaryService : ICloudinaryService
    {

        private static readonly List<string> AllowedContentTypes = new()
        {
        "image/jpeg",
        "image/png",
        "image/jpg"
        };

        private readonly string _usersFolder = "users";
        private readonly CloudinaryOptions _cloudinaryOptions;
        private readonly Cloudinary _cloudinary;
       
        public CloudinaryService(IOptions< CloudinaryOptions >cloudinaryOptions)
        {
            _cloudinaryOptions = cloudinaryOptions.Value;

            var account = new Account(_cloudinaryOptions.Name, _cloudinaryOptions.ApiKey, _cloudinaryOptions.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<ErrorOr<string>> UploadUserPhotoAsync(IFormFile file, Guid userId)
        {
            if (!IsEmptyFile(file))
                return CloudinaryErrors.EmptyFile;

            if (!IsValidImageType(file.ContentType))
                return CloudinaryErrors.UnsupportedPhotoType;

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = _usersFolder,
                PublicId = userId.ToString(), 
                Overwrite = true
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode == HttpStatusCode.OK)
                return result.SecureUrl.ToString();

           return CloudinaryErrors.FailedToUpload;



        }

        public async Task<ErrorOr<string>> UploadGymPhotoAsync(IFormFile file, Guid gymId)
        {
            if (!IsEmptyFile(file))
                return CloudinaryErrors.EmptyFile;

            if (!IsValidImageType(file.ContentType))
                return CloudinaryErrors.UnsupportedPhotoType;

            var uniqueFileName = Guid.NewGuid().ToString();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = $"gyms/{gymId}/photos",
                PublicId = uniqueFileName,
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode == HttpStatusCode.OK)
                return result.SecureUrl.ToString();

            return CloudinaryErrors.FailedToUpload;
        }

        public async Task<ErrorOr<Success>> DeleteUserPhotoAsync(Guid userId)
        {
            var publicId = $"{_usersFolder}/{userId.ToString()}";

            var deletionParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image
            };

            var result = await _cloudinary.DestroyAsync(deletionParams);


            if (result.Result != "ok")
                return CloudinaryErrors.FailedToDelete;

            return Result.Success;
        }

        private bool IsValidImageType (string imageType)
        {
            if (AllowedContentTypes.Contains(imageType))
                return true; 
            return false;
        }

        private bool IsEmptyFile (IFormFile file)
        {
            if (file is null || file.Length == 0 )
                return false;   

            return true;
        }

        
    }
  
}

