using ChatSystem.Model.Chats;
using ChatSystem.Model.Users;

namespace ChatSystem.ViewModel.Messages
{
    public class MessageViewModel
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }

    }
}
