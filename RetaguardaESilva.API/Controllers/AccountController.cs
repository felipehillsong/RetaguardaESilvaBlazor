using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RetaguardaESilva.Application.Login;
using RetaguardaESilva.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RetaguardaESilva.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return $"AccountController :: {DateTime.Now.ToShortDateString()}";
        }

        [HttpPost("Cadastrar")]
        public async Task<ActionResult<LoginToken>> Cadastrar([FromBody] Login login)
        {
            var usuario = new IdentityUser
            {
                UserName = login.Email,
                Email = login.Email,
            };

            var result = await _userManager.CreateAsync(usuario, login.Senha);

            if (result.Succeeded)
            {
                return GerarToken(login);
            }
            else
            {
                return BadRequest(new {message = "Email ou senha inválidos..."});
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginToken>> Login([FromBody] Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Senha, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return GerarToken(login);
            }
            else
            {
                return BadRequest(new { message = "Email ou senha inválidos..." });
            }
        }

        private LoginToken GerarToken(Login login)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
                new Claim(ClaimTypes.Name, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var message = "Token JWT criado com sucesso";
            JwtSecurityToken token = new JwtSecurityToken(
                issuer:null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new LoginToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = message
            };
        }
    }
}
