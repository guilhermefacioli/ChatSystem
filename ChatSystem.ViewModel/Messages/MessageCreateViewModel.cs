namespace ChatSystem.ViewModel.Messages
{
    public class MessageCreateViewModel
    {
        public string Text { get; set; }

        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }
        
    }
}
