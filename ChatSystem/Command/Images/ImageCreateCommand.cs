using Microsoft.AspNetCore.Http;

namespace ChatSystem.Command.Images
{
    public class ImageCreateCommand
    {
        public ImageCreateCommand(
            Guid chatId,
            Guid usertId, 
            string name, 
            IFormFile file)
        {
            ChatId = chatId;
            UserId = usertId;
            Name = name;
            File = file;
        }

        public Guid ChatId { get; set;}

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public IFormFile File { get; set; }
    }
}
