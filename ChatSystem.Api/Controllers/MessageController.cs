using AutoMapper;
using ChatSystem.Command.Images;
using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.Model.Messages;
using ChatSystem.ViewModel.Images;
using ChatSystem.ViewModel.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatSystem.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessageController (
            ILogger<MessageController> logger,
            IMapper mapper,
            IMessageService messageService)
        {
            _logger = logger;
            _mapper = mapper;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MessageFilterViewModel model, [FromQuery] PagingOptions pagingOptions )
        {
            var filter = _mapper.Map<MessageFilter>(model);
            var response = await _messageService.GetAllMessage(filter, pagingOptions);

            if (response.IsError)
            {
                return Problem(string.Join(',', response.Errors), statusCode: 500);
            }

            var items = response.Result!.Items!
                .Select(x => _mapper.Map<MessageViewModel>(x));

            return Ok(new CollectionResult<MessageViewModel>
            {
                Items = items.ToList(),
                Total = response.Result.Total
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageCreateViewModel model)
        {
            var command = new MessageCreateCommand(model.Text, model.ChatId, model.UserId);

            var result = await _messageService.CreateMessage(command);

            if (result.IsError)
            {
                return Problem(string.Join(',', result.Errors), statusCode: 500);
            }

            return Created(result.Result!.Id.ToString(), _mapper.Map<MessageViewModel>(result.Result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            await _messageService.DeleteMessage(id);

            return NoContent();
        }
    }
}
