using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Helpers.Interfaces;

namespace Service.Helpers
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> AddPhoto(IFormFile file)
        {
            var uploadResoult = new ImageUploadResult();
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Gravity("face")
            };
            uploadResoult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResoult;
        }

        public async Task<DeletionResult> DeletePhoto(string publicId)
        {
            var deleteparams = new DeletionParams(publicId);
            var resoult = await _cloudinary.DestroyAsync(deleteparams);
            return resoult;
        }
    }
}

