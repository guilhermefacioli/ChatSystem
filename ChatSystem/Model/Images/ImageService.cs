using ChatSystem.Command.Images;
using ChatSystem.Common;

namespace ChatSystem.Model.Images;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;

    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    public async Task<ApplicationResult<Image>> CreateImage(ImageCreateCommand command)
    {
        var path = Path.GetTempPath();

        var extension = command.File.FileName.Split('.').Last();

        path = Path.Combine(path, $"{command.Name}.{extension}");

        using var stream = File.Create(path);

        command.File.CopyTo(stream);

        var entity = new Image(
            Guid.NewGuid(),
            string.Empty,
            command.ChatId, 
            DateTime.UtcNow,
            command.UserId, 
            command.Name, 
            path);
          

        var creationResult = await _imageRepository.Create(entity);

        var result = new ApplicationResult<Image>
        {
            Result = creationResult.IsSuceccss ? entity : null,
            Errors = creationResult.Errors
        };

        return result;
    }
     

    public async Task<ApplicationResult> DeleteImage(Guid id)
    {
        var deleteResult = await _imageRepository.Delete(id);

        var result = new ApplicationResult
        {
            Errors = deleteResult.Errors
        };

        return result;        
    }

    public Task<ApplicationResult<Image>> GetImage(Guid id)
    {
        return _imageRepository.Get(id);
    }

       
}
