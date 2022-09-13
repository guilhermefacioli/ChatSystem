using ChatSystem.Command.Users;
using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Users
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationResult<User>> CreateUser(UserCreateCommand command)
        {
            var entity = new User(
                Guid.NewGuid(),
                command.Name, 
                command.Email,
                command.Password);
            

            var creationResult = await _userRepository.Create(entity);

            var result = new ApplicationResult<User>
            {
                Result = creationResult.IsSuceccss ? entity : null,
                Errors = creationResult.Errors
            };

            return result;
        }

        public async Task<ApplicationResult> UpdateUser(Guid id ,UserUpdateCommand command)
        {
            var searchResult = await _userRepository.Get(id);

            var entity = searchResult.Result;

            //check if null

            entity.Name = command.Name;
            entity.Email = command.Email;
            entity.Password = command.Password;

            var updateResult = await _userRepository.Update(entity);

            var result = new ApplicationResult<User>
            {
                Result = updateResult.IsSuceccss ? entity : null,
                Errors = updateResult.Errors
            };

            return result;
        }

        public async Task<ApplicationResult> DeleteUser(Guid id)
        {
            var deleteResult = await _userRepository.Get(id);

            var result = new ApplicationResult
            {
                Errors = deleteResult.Errors
            };

            return result;
        }

        public Task<ApplicationResult<CollectionResult<User>>> GetAllUser(Filters filter, PagingOptions pagingOptions)
        {
            return _userRepository.GetAll(filter, pagingOptions);   
        }

        public Task<ApplicationResult<User>> GetUser(Guid id)
        {
            return _userRepository.Get(id);
        }
    }
}
