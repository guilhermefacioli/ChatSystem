namespace ChatSystem.Command.Chats
{
    public class ChatsUsersAddCommand
    {
        public ChatsUsersAddCommand( Guid chatId, Guid userId)
        {
            ChatId = chatId;
            UserId = userId;
        }
        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }
    }
}
