using gas_station.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace gas_station.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DBContext _context;
        // менеджер для аутентификации
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;
        public AccountController(DBContext context, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        [HttpPost("/registration")]
        public async Task<IActionResult> Register(UserInput model)
        {
            // по умолчанию все пользователи создаются с ролью Клиент
            User user = new User() { Email = model.Email, UserName = model.UserName, Password = model.Password, IdentityRoleId = 3 };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
 
            return new JsonResult(result);
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token(UserInput user)
        {
            var identity = await GetIdentity(user.UserName, user.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var userRole = await _roleManager.FindByIdAsync(identity.Claims.ToArray()[1].Value);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                role = userRole.Name
            };
            return new JsonResult(response);
        }

        [HttpPost]
        // идентификация пользователя
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            User user = _context.Users.FirstOrDefault(i => i.UserName == username && i.Password == password);

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (user != null && result)
            {
                var claims = new List<Claim>
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IdentityRoleId.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            // если пользователя не найдено
            return null;
        }

        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            return Ok("Ваша роль: администратор");
        }
    }
}
