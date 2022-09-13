using Microsoft.AspNetCore.Http;

namespace ChatSystem.ViewModel.Images
{
    public class ImageCreateViewModel
    {

        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public IFormFile File { get; set; }
    }
}
