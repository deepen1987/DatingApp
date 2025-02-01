using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length <= 0) return uploadResult;
        
        await using var stream = file.OpenReadStream();
        
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
            Folder = "photos"
        };
            
        uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string photoId)
    {
        var deleteParams = new DeletionParams(photoId);
        
        return await _cloudinary.DestroyAsync(deleteParams);
    }
}