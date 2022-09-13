using AutoMapper;
using ChatSystem.Command.Chats;
using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Chats;
using ChatSystem.ViewModel.Chats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;

        public ChatController(
            IMapper mapper,
            ILogger<ChatController> logger,
            IChatService chatService
            )
        {
            _mapper = mapper;
            _logger = logger;
            _chatService = chatService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ChatFilterViewModel model, [FromQuery] PagingOptions pagingOptions)
        {
            var filter = _mapper.Map<Filters>(model);
            var response = await _chatService.GetAllChat(filter, pagingOptions);

            if (response.IsError)
            {
                return Problem(string.Join(',', response.Errors), statusCode: 500);
            }
            var items = response.Result!.Items!
                .Select(x => _mapper.Map<ChatViewModel>(x));

            return Ok(new CollectionResult<ChatViewModel>
            {
                Items = items.ToList(),
                Total = response.Result.Total
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(Guid id)
        {
            var response = await _chatService.GetChat(id);

            if (response.IsError)
            {
                return Problem(string.Join(',', response.Errors), statusCode: 500);
            }

            var item = _mapper.Map<ChatViewModel>(response.Result);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] ChatCreateViewModel model)
        {
            var command = new ChatCreateCommand(model.Name);

            var result = await _chatService.CreateChat(command);

            if (result.IsError)
            {
                return Problem(string.Join(',', result.Errors), statusCode: 500);
            }

            return Created(result.Result!.Id.ToString(), _mapper.Map<ChatViewModel>(result.Result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChat(Guid id, [FromBody] ChatCreateViewModel model)
        {
            var command = new ChatUpdateCommand(id, model.Name);

            await _chatService.UpdateChat(id, command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(Guid id)
        {
            await _chatService.DeleteChat(id);

            return NoContent();
        }
    }
}
