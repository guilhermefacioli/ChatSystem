using ChatSystem.Common;
using ChatSystem.Filter;
using System.Linq.Expressions;

namespace ChatSystem.Model.Chats
{
    public interface IChatRepository
    {
        Task<ApplicationResult> Create(Chat chat);

        Task<ApplicationResult> Update(Chat chat);

        Task<ApplicationResult> Delete(Guid id);

        Task<ApplicationResult<Chat>> Get(Guid id);

        Task<ApplicationResult<CollectionResult<Chat>>> GetAll(Filters filter, PagingOptions pagingOptions);
    }
}
