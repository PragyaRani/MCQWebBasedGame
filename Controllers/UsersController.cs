using MCQPuzzleGame.Model;
using MCQPuzzleGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MCQPuzzleGame.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        //[Authorize(Roles = "Admin")]
        //[CustomAuthorization]
        [HttpGet("FetchUsers")]
        public IActionResult GetUserDeatils()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("autheticate")]
        public IActionResult Authenticate([FromBody] LoginUser user)
        {
            var response = _userService.Authenticate(user, ipAddress());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }
       
        [HttpPost("refersh-token")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = _userService.RefreshToken(refreshToken,ipAddress());
            if(response == null)
                return Unauthorized(new { message ="Invalid Token"});
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }
        
        [HttpPost("revoke-token")]
        public IActionResult RevokeToken()
        {
            var token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });
            var response = _userService.RevokeToken(token,ipAddress());
            if (!response)
                return NotFound(new { message = "Token not found" });
            return Ok(new { message = "Token Revoked" });

        }
        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                //HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
