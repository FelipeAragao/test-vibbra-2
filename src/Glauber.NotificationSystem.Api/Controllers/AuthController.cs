using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Glauber.NotificationSystem.Api.Data;
using Glauber.NotificationSystem.Api.DTOs;
using Glauber.NotificationSystem.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Glauber.NotificationSystem.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController(
        SignInManager<AppUser> signInManager, 
        UserManager<AppUser> userManager,
        IOptions<AppSettings> appSettings) : MainController
    {
        private readonly SignInManager<AppUser> _signInManager = signInManager;
        private readonly UserManager<AppUser> _userManager = userManager;
        private AppSettings _appSettings = appSettings.Value;

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            if (!ModelState.IsValid) return Ok(false);

            var result = await _signInManager.PasswordSignInAsync(
                                    loginUser.Login,
                                    loginUser.Password,
                                    isPersistent: false, lockoutOnFailure: true);
           
            if (result.Succeeded)
            {
                return Ok(await GenerateToken(loginUser.Login));
            }

            if (result.IsLockedOut)
            {
                return BadRequest("User account locked for too many invalid attempts");
            }

            return BadRequest("Invalid user or password");
        }

        [HttpPost("users/register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUser)
        {
            var user = new AppUser()
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                Name = registerUser.Name,
                CompanyName = registerUser.CompanyName,
                CompanyAddress = registerUser.CompanyAddress,
                PhoneNumber = registerUser.PhoneNumber                
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByEmailAsync(user.Email);
                return Ok(new 
                { 
                    createdUser!.Id
                });
            }
            return FormatErrorResponse(result.Errors);
        }

        [Authorize]
        [HttpGet("users/{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(new
            {
                user.Email,
                user.Name,
                user.CompanyName,
                user.PhoneNumber,
                user.CompanyAddress
            });
        }

        private async Task<LoginResponseDTO> GenerateToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAudience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.HoursToExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseDTO
            {
                Token = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.HoursToExpire).TotalSeconds,
                User = new UserTokenDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
