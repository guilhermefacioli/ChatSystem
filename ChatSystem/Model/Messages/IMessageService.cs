using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Messages
{
    public interface IMessageService
    {

        Task<ApplicationResult<Message>> CreateMessage(MessageCreateCommand command);

        Task<ApplicationResult> DeleteMessage(Guid id);

        Task<ApplicationResult<CollectionResult<Message>>> GetAllMessage(MessageFilter messageFilter, PagingOptions pagingOptions);
    }
}
