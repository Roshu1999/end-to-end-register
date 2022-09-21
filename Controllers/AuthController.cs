using IdentityCMS.Models;
using IdentityCMS.Repo;
using IdentityCMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace IdentityCMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static registeruser register = new registeruser();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _cxtAccessor;
        private readonly CustomDbContext _customDbContext;
        private readonly IRepo _repo;


        public AuthController(IConfiguration configuration, IUserService userService , IHttpContextAccessor cxtAccessor,
            CustomDbContext _customDbContext , IRepo r)
        {
            _configuration = configuration;
            _userService = userService;
            _cxtAccessor = cxtAccessor;
            _repo = r;
            this._customDbContext = _customDbContext;
        }

/*        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }
*/

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Signup([FromBody] registeruser reg)
        {
            _customDbContext.BuildConnectionstring(_configuration.GetConnectionString("registerConn"));
            var status = _repo.createcustomer(reg);

            if (status == "OK")
            {
                return Ok(new { message = "customer created successfully!" });
            }
            else
            {
                return StatusCode(429, status);
            }
        }
    }
}


/*_customDbContext.BuildConnectionstring(_configuration.GetConnectionString("registerConn"));

var user = _customDbContext.registerCMS.SingleOrDefault(u => u.username == value.username);
if (user != null) return StatusCode(409);

_customDbContext.registerCMS.Add(new registeruser
{
    username = value.username,
    email = value.email,
    phone = value.phone,
    password = value.password,
});


await _customDbContext.SaveChangesAsync();
user = _customDbContext.registerCMS.SingleOrDefault(u => u.username == value.username);
return Ok(new { message = "Account Created! Please Login..." });*/