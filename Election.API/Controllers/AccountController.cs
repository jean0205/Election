using Election.API.Data.Entities;
using Election.API.Helpers;
using Election.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;

        public AccountController(IUserHelper userHelper, IConfiguration configuration)
        {
            _userHelper = userHelper;
            _configuration = configuration;

        }

        [HttpPost]
        [Route("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.UserName);
                if (user != null)
                {
                    if (user.Active)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            Claim[] claims = new[]
                            {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            JwtSecurityToken token = new JwtSecurityToken(
                                _configuration["Tokens:Issuer"],
                                _configuration["Tokens:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddDays(30),
                                signingCredentials: credentials);
                            var results = new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo,
                                user
                            };
                            user.Online = true;
                            user.LogInTime = DateTime.Now;
                            await _userHelper.UpdateUserAsync(user);
                            return Created(string.Empty, results);
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Username and password do not match.");
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("Error", "The user was De-Activated, Please contact your System Administrator.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "The user was not found.");
                }
            }
            return BadRequest(ModelState);
        }


    }


}
