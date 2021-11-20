using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nologo.Domain.Auth;
using Nologo.Extensions;

namespace Nologo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AccountController(SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager,
                                IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("new-account")]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUserDto.Email,
                Email = registerUserDto.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                string role;
                if (registerUserDto.Role == 1)
                    role = "ADMIN";
                else
                    role = "USER";

                _ = await _userManager.AddToRoleAsync(user, role);
                await _signInManager.SignInAsync(user, false);
                return Ok(await GenerateJwt(user));
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email,
                loginDto.Password, false, true);

            var user = await _signInManager.UserManager.FindByNameAsync(loginDto.Email);

            if (result.Succeeded)
            {
                var results = await GenerateJwt(user);
                return Ok(results);
            }               

            if (result.IsLockedOut)
                return BadRequest("The user is temporarily blocked due to invalid attempts");

            return BadRequest("Username or password is invalid");
        }

        private async Task<LoginResponseDto> GenerateJwt(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var isInRole = await _userManager.IsInRoleAsync(user, "ADMIN");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("userid", user.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim("role", isInRole ? "1" : "2"),
                new Claim(ClaimTypes.Role,user.UserName.ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _appSettings.Issuer,
                audience: _appSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDto
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds
            };
            return response;
        }
    }
}