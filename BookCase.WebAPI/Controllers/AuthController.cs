using BookCase.Business.Abstract;
using BookCase.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookCase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private readonly ILogger _logger;

        public AuthController(IAuthService authService, ILogger logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var userToLogin = _authService.Login(userForLoginDto);
            sw.Stop();
            _logger.LogInformation($"Login. ms:{sw.ElapsedMilliseconds}");
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            sw.Stop();
            _logger.LogInformation($"Register. ms:{sw.ElapsedMilliseconds}");
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
