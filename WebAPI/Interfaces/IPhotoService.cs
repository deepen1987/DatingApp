using CloudinaryDotNet.Actions;

namespace WebAPI.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string photoId);
}