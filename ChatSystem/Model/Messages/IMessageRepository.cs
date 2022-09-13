using ChatSystem.Common;
using ChatSystem.Filter;
using System.Linq.Expressions;

namespace ChatSystem.Model.Messages
{
    public interface IMessageRepository
    {
        Task<ApplicationResult> Create(Message message);

        Task<ApplicationResult> Delete(Guid id);

        Task<ApplicationResult<CollectionResult<Message>>> GetAll(MessageFilter messageFilter, PagingOptions pagingOptions);
    }
}
