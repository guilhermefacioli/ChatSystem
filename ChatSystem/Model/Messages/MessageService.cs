using ChatSystem.Common;
using ChatSystem.Filter;

namespace ChatSystem.Model.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<ApplicationResult<Message>> CreateMessage(MessageCreateCommand command)
        {
            var entity = new Message(Guid.NewGuid(), command.Text, command.ChatId, command.UserId);
            
            var creationResult = await _messageRepository.Create(entity);

            var result = new ApplicationResult<Message>
            {
                Result = creationResult.IsSuceccss ? entity : null,
                Errors = creationResult.Errors
            };

            return result;
        }

        public async Task<ApplicationResult> DeleteMessage(Guid id)
        {
            var deleteResult = await _messageRepository.Delete(id);

            var result = new ApplicationResult
            {
                Errors = deleteResult.Errors
            };
            return result;
        }

        public Task<ApplicationResult<CollectionResult<Message>>> GetAllMessage(MessageFilter messageFilter, PagingOptions pagingOptions)
        {
            return _messageRepository.GetAll(messageFilter, pagingOptions); 
        }
    }
}
