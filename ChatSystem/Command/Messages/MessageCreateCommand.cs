namespace ChatSystem
{
    public class MessageCreateCommand
    {
        public MessageCreateCommand(
            string text,
            Guid chatId,
            Guid userId)
        {
            Text = text;
            ChatId = chatId;
            UserId = userId;
        }

        public string Text { get; set; }

        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }

    }
}