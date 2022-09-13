using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Chats
{
    public interface IChatsUsersRepository
    {
        Task<ApplicationResult> Create(ChatsUsers chatsUsers);

        Task<ApplicationResult> Delete(Guid id);

        Task<ApplicationResult<CollectionResult<ChatsUsers>>> GetAll(ChatsUsersFilter filter, PagingOptions pagingOptions);
    }
}
