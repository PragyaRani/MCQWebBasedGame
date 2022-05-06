using MCQPuzzleGame.Context;
using MCQPuzzleGame.Model;
using MCQPuzzleGame.Repositiories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MCQPuzzleGame.Exception;
using Microsoft.AspNetCore.Authorization;

namespace MCQPuzzleGame.Controllers
{
   
    [Route("Users")]
    [ApiController]
    [Log]
    public class RegisterLoginController : ControllerBase
    {
        private readonly DbStoreContext dbStoreContext;
        private readonly IUserRepository userRepository;
        private IConfiguration config;
        public RegisterLoginController(DbStoreContext _dbStoreContext, IUserRepository _userRepository, IConfiguration _config)
        {
            dbStoreContext = _dbStoreContext;
            userRepository = _userRepository;
            config = _config;

        }
        [Authorize]
        //[Authorize(Roles = "Admin")]
        
        //[CustomAuthorization]
        [HttpGet("FetchUsers")]
        public async Task<IActionResult> GetUserDeatils()
        {
            var users = await dbStoreContext.Users.ToListAsync();
            //var db = config.GetValue<string>("ConnectionStrings:Server");
            return Ok(users);
        }
        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> PostUserDeatils([FromBody]RegisterUser user)
        {
            var id= await userRepository.AddUser(user);
            return Ok(id);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            var userdetails = await userRepository.VerifyUser(user);
            if(userdetails !=null)
            {
                return Ok(userRepository.sendToken(userdetails));
            }

            return Ok("Username or password is wrong");
        }
    }
}
