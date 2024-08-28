using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MongoDb_Test.Data;
using MongoDb_Test.Services;

namespace MongoDb_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private UserService _userService;
        private RandomUserService _randomUserService;

        public UserController(UserService userService, RandomUserService randomUserService)
        {
            _userService = userService;
            _randomUserService = randomUserService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult> Index()
        {
            var result = await _userService.GetAsync();
            
            return Ok(result.Slice(0, 50));
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateMany([FromBody] int count)
        {
            await _randomUserService.CreateManyUsers(count);
            return Ok($"Its created by {count}");
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GitById([FromRoute] string id)
        {
            var result = await _userService.GetAsync(id);
            return Ok(result);
        }
    }
}
