using ChatSystem.Command.Users;
using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Users;

namespace ChatSystem
{
    public interface IUserService
    {
        Task<ApplicationResult<User>> CreateUser(UserCreateCommand command);

        Task<ApplicationResult> UpdateUser(Guid id, UserUpdateCommand command);

        Task<ApplicationResult> DeleteUser(Guid id);
            
        Task<ApplicationResult<User>> GetUser(Guid id);
        
        Task<ApplicationResult<CollectionResult<User>>> GetAllUser(Filters filter, PagingOptions pagingOptions);

    }
}
