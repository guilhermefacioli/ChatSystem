using ChatSystem.Model.Users;

namespace ChatSystem.Model.Chats
{
    public class ChatsUsers
    {
        public ChatsUsers(Guid id, Guid chatId, Guid userId)
        {
            Id = id;
            ChatId = chatId;
            UserId = userId;
        }

        public Guid Id { get; set; }

        public Guid ChatId { get; set; }

        public Chat? Chat { get; set; }

        public Guid UserId { get; set; }

        public User? User{ get; set; }

        public DateTime? DateDeleted { get; set; }


    }
}
