using ChatSystem.Command.Chats;
using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Chats
{
    public interface IChatService
    {
        Task<ApplicationResult<Chat>> CreateChat(ChatCreateCommand command);

        Task<ApplicationResult> UpdateChat(Guid id, ChatUpdateCommand command);

        Task<ApplicationResult> DeleteChat(Guid id);
        
        Task<ApplicationResult<Chat>> GetChat(Guid id);

        Task<ApplicationResult<CollectionResult<Chat>>> GetAllChat(Filters filter, PagingOptions pagingOptions);

        Task<ApplicationResult<CollectionResult<ChatsUsers>>> AddMember(ChatsUsersAddCommand command);

        Task<ApplicationResult<CollectionResult<ChatsUsers>>> GetAllMembers(ChatsUsersFilter filter, PagingOptions pagingOptions);

        Task<ApplicationResult> DeleteMember(Guid id);





    }
}
