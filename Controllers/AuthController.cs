using IdentityCMS.Models;
using IdentityCMS.Repo;
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
        private readonly IAuth _userService;
        private readonly IHttpContextAccessor _cxtAccessor;
        private readonly CustomDbContext _customDbContext;
        private readonly IRepo _repo;
        private readonly IPasswordHasher _passwordHasher;


        public AuthController(IConfiguration configuration, IHttpContextAccessor cxtAccessor, IAuth auth,
            CustomDbContext _customDbContext , IRepo r, IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _userService = auth;
            _cxtAccessor = cxtAccessor;
            _repo = r;
            _passwordHasher = passwordHasher;
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

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult LoginCheck([FromBody] loginuser login)
        {


            _customDbContext.BuildConnectionstring(_configuration.GetConnectionString("registerConn"));

            var user = _customDbContext.registerCMS.FirstOrDefault(u => u.username == login.username);
            if (user == null || !_passwordHasher.VerifyIdentityV3Hash(login.password, user.password)) return BadRequest(new { message = "Invalid - Username or password" });

            var jwtToken = _userService.GenerateToken(user.username, user.password);



            if (jwtToken == null)
                return BadRequest(new { message = "Invalid Credentials" });

            return Ok(jwtToken);
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






/*_customDbContext.BuildConnectionstring(_configuration.GetConnectionString("registerConn"));

var user = _customDbContext.registerCMS.SingleOrDefault(u => u.username == login.username && u.password == login.password);
if (user == null)
{
    return BadRequest(new { message = "Invalid - Username or password" });
}
else
{
    return Ok(new { message = "Account Logged!..." });
}*/