using ChatSystem.Model.Chats;
using ChatSystem.Model.Users;

namespace ChatSystem.Model.Messages
{
    public class Message 
    {
        public Message(Guid id, string text, Guid chatId, Guid userId)
        {
            Id = id;
            Text = text;
            ChatId = chatId;
            UserId = userId;
        }

        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid ChatId { get; set; }

        public Chat? Chat { get; set; }

        public Guid UserId { get; set; }

        public User? User { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateDeleted { get; set; }
    }
}