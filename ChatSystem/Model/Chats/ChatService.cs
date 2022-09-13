using ChatSystem.Command.Chats;
using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Chats
{
    public class ChatService : IChatService
    {
        private IChatRepository _chatRepository;

        private IChatsUsersRepository _chatsUsersRepository;
        public ChatService(IChatRepository chatRepository, IChatsUsersRepository chatsUsersRepository)
        {
            _chatRepository = chatRepository;
            _chatsUsersRepository = chatsUsersRepository;
        } 

        public async Task<ApplicationResult<Chat>> CreateChat(ChatCreateCommand command)
        {
            var entity = new Chat(Guid.NewGuid(), command.Name);

            var creationResult = await _chatRepository.Create(entity);

            var result = new ApplicationResult<Chat>
            {
                Result = creationResult.IsSuceccss ? entity : null,
                Errors = creationResult.Errors
            };

            return result;
        }

        public async Task<ApplicationResult> UpdateChat(Guid id, ChatUpdateCommand command)
        {
            var searchResult = await _chatRepository.Get(id);

            var entity = searchResult.Result;

            if (entity == null)
            {
                return new ApplicationResult
                {
                    Errors = new List<string> { "Chat not found!" }
                };
            }

            entity.Id = id;
            entity.Name = command.Name;

            var updateResult = await _chatRepository.Update(entity);

            var result = new ApplicationResult<Chat>
            {
                Result = updateResult.IsSuceccss ? entity : null,
                Errors = updateResult.Errors
            };

            return result;
        }

        public async Task<ApplicationResult> DeleteChat(Guid id)
        {
            var deleteResult = await _chatRepository.Delete(id);

            var result = new ApplicationResult
            {
                Errors = deleteResult.Errors
            };

            return result;
        }

        public Task<ApplicationResult<CollectionResult<Chat>>> GetAllChat(Filters filter, PagingOptions pagingOptions)
        {
            return _chatRepository.GetAll(filter, pagingOptions);
        }

        public Task<ApplicationResult<Chat>> GetChat(Guid id)
        {
            return _chatRepository.Get(id);
        }

        public async Task<ApplicationResult<CollectionResult<ChatsUsers>>> AddMember(ChatsUsersAddCommand command)
        {
            var entity = new ChatsUsers(Guid.NewGuid(), command.ChatId, command.UserId);

            var creationResult = await _chatsUsersRepository.Create(entity);

            var chatsUsers = await _chatsUsersRepository.GetAll(new ChatsUsersFilter(command.ChatId, command.UserId), new PagingOptions());

            var result = new ApplicationResult<CollectionResult<ChatsUsers>>
            {
                Result = creationResult.IsSuceccss ? chatsUsers.Result : null,
                Errors = creationResult.Errors
            };

            return result;
        }

        public Task<ApplicationResult<CollectionResult<ChatsUsers>>> GetAllMembers(ChatsUsersFilter filter, PagingOptions pagingOptions)
        {
            return _chatsUsersRepository.GetAll(filter, pagingOptions);
        }

        public async Task<ApplicationResult> DeleteMember(Guid id)
        {
            var deleteResult = await _chatsUsersRepository.Delete(id);

            var result = new ApplicationResult
            {
                Errors = deleteResult.Errors
            };

            return result;
        }
    }
}
