using AutoMapper;
using ChatSystem.Command.Users;
using ChatSystem.Common;
using ChatSystem.Filter;
using ChatSystem.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ChatSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(
            IMapper mapper,
            ILogger<UserController> logger,
            IUserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserFilterViewModel model, [FromQuery] PagingOptions pagingOptions)
        {
            var filter = _mapper.Map<Filters>(model);
            var response = await _userService.GetAllUser(filter, pagingOptions);

            if (response.IsError)
            {
                return Problem(string.Join(',', response.Errors), statusCode: 500);
            }

            var items = response.Result!.Items!
                .Select(x => _mapper.Map<UserViewModel>(x));

            return Ok(new CollectionResult<UserViewModel>
             {
                Items = items.ToList(),
                Total = response.Result.Total
             });;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var response = await _userService.GetUser(id);

            if (response.IsError)
            {
                return Problem(string.Join(',', response.Errors), statusCode: 500);
            }

            var item = _mapper.Map<UserViewModel>(response.Result);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateViewModel model)
        {
            var command = new UserCreateCommand(model.Name, model.Email, model.Password);

            var result = await _userService.CreateUser(command);

            if (result.IsError)
            {
                return Problem(string.Join(',', result.Errors), statusCode: 500);
            }

            return Created(result.Result!.Id.ToString(), _mapper.Map<UserViewModel>(result.Result));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserCreateViewModel model)
        {
            var command = new UserUpdateCommand(model.Name, model.Email, model.Password);

            await _userService.UpdateUser(id, command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);

            return NoContent();
        }
    }
}
