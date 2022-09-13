namespace ChatSystem.Command.Chats
{
    public class ChatCreateCommand
    {
        public ChatCreateCommand( string name)
        {
            Name = name;
        }

        public string Name { get; set; }

       
    }
}
