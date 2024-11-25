using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApiBearerAuth.Models;
using WebApiBearerAuth.Services;

namespace WebApiBearerAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody]AuthRequest authRequest)
        {
            if (String.IsNullOrWhiteSpace(authRequest.Username) || String.IsNullOrWhiteSpace(authRequest.Password))
                return new BadRequestResult();

            // fail auth if/when password is "bad" case insenitive
            if (authRequest.Username.Equals("bad", StringComparison.OrdinalIgnoreCase)) 
                return new UnauthorizedResult();

            User user = new()
            {
                UserName = authRequest.Username,
                Roles = ["Employee", "Manager"]
            };

            string token = this._tokenService.GetToken(user);

            return Ok(token);             
        }
    }
}
