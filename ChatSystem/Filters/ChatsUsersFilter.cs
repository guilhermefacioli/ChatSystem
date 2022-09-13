using ChatSystem.Model.Chats;
namespace ChatSystem.Filter
{
    public class ChatsUsersFilter
    {
        public ChatsUsersFilter(Guid chatId, Guid? userId = null)
        {
            ChatId = chatId;
            UserId = userId;
        }
        public Guid ChatId { get; set; }

        public Guid? UserId { get; set; }
    }
}
