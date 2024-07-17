using System;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Service.Helpers.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<ImageUploadResult> AddPhoto(IFormFile file);
        Task<DeletionResult> DeletePhoto(string publicId);
    }
}

