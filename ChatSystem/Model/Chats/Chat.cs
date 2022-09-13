namespace ChatSystem.Model.Chats
{
    public class Chat
    {
        public Chat(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime? DateDeleted { get; set; }

        public IEnumerable<ChatsUsers>? ChatsUsers { get; set; }

    }
}
