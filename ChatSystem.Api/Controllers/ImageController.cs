using AutoMapper;
using ChatSystem.Command.Images;
using ChatSystem.Model.Images;
using ChatSystem.ViewModel.Images;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ImageController> _logger;
        private readonly IImageService _imageService;

        public ImageController(
            IMapper mapper,
            ILogger<ImageController> logger,
            IImageService imageService)
        {
            _mapper = mapper;
            _logger = logger;
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(Guid id)
        {
            var response = await _imageService.GetImage(id);

            if (response.IsError)
            {
                return Problem(string.Join(',', response.Errors), statusCode: 500);
            }

            var item = _mapper.Map<ImageViewModel>(response.Result);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage([FromForm] ImageCreateViewModel model)
        {
            var command = new ImageCreateCommand(model.ChatId, model.UserId, model.Name, model.File);

            var result = await _imageService.CreateImage(command);

            if (result.IsError)
            {
                return Problem(string.Join(',', result.Errors), statusCode: 500);
            }

            return Created(result.Result!.Id.ToString(), _mapper.Map<ImageViewModel>(result.Result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            await _imageService.DeleteImage(id);
            return NoContent();
        }
    }
}
