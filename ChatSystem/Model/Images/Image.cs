using ChatSystem.Model.Messages;

namespace ChatSystem.Model.Images
{
    public class Image : Message
    {
        public Image(
            Guid id,
            string text,
            Guid chatId,
            DateTime dateCreated,
            Guid userId,
            string name,
            string path
            ): base(id, text, chatId, userId)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; set; }

        public string Path { get; set; }

    }
}
