using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Users;
using System.Linq.Expressions;

namespace ChatSystem
{
    public interface IUserRepository
    {

        Task<ApplicationResult> Create(User user);

        Task<ApplicationResult> Update(User user);

        Task<ApplicationResult> Delete(Guid id);

        Task<ApplicationResult<User>> Get(Guid id);

        Task<ApplicationResult<CollectionResult<User>>> GetAll(Filters filter, PagingOptions pagingOptions);
    }
}
