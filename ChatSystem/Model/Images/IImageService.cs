using ChatSystem.Command.Images;
using ChatSystem.Common;


namespace ChatSystem.Model.Images
{
    public interface IImageService
    {
        Task<ApplicationResult<Image>> CreateImage(ImageCreateCommand command);

        Task<ApplicationResult> DeleteImage(Guid id);

        Task<ApplicationResult<Image>> GetImage(Guid id);
    }
}
