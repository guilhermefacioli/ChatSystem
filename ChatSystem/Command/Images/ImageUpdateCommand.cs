namespace ChatSystem.Command.Images
{
    public class ImageUpdateCommand
    {
        public ImageUpdateCommand(string name, Guid messageId, string path)
        {
            Name = name;
            MessageId = messageId;
            Path = path;
        }

        public string Name { get; set; }

        public Guid MessageId { get; set; }

        public string Path { get; set; }
    }
}
