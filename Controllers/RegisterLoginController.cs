using MCQPuzzleGame.Context;
using MCQPuzzleGame.Model;
using MCQPuzzleGame.Repositiories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MCQPuzzleGame.Exception;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace MCQPuzzleGame.Controllers
{
   
    [Route("Users")]
    [ApiController]
    //[Log]
    public class RegisterLoginController : ControllerBase
    {
      
        private readonly IUserRepository userRepository;
        public RegisterLoginController(IUserRepository _userRepository)
        {
           
            userRepository = _userRepository;
        }
              
        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> PostUserDeatils([FromBody]RegisterUser user)
        {
            var id= await userRepository.AddUser(user);
            return Ok(id);
        }

        [NonAction]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser user)
        {
            var userdetails = await userRepository.VerifyUser(user);
            if(userdetails !=null)
            {
                //return Ok(userRepository.sendToken(userdetails));
            }

            return Ok("Username or password is wrong");
        }
    }
}
