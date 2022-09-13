using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Images
{
    public interface IImageRepository
    {
        Task<ApplicationResult> Create(Image image);
        
        Task<ApplicationResult> Update(Image image);

        Task<ApplicationResult> Delete(Guid id);

        Task<ApplicationResult<Image>> Get(Guid id);

    }
}
