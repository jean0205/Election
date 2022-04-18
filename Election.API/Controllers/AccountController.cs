using Election.API.Data.Entities;
using Election.API.Helpers;
using Election.API.Models;
using Election.API.Models.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMailHelper _mailHelper;

        public AccountController(IUserHelper userHelper, IConfiguration configuration, IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            this._mailHelper = mailHelper;
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

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            string userName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User user = await _userHelper.GetUserAsync(userName);
            user.Online = false;
            user.LogOutTime = DateTime.Now;
            await _userHelper.UpdateUserAsync(user);
            await _userHelper.LogoutAsync();
            return NoContent();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("RegisterUser")]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.AddUserAsync(model, "Welcome1");
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This Email or userName is been used by other user.");
                    return BadRequest(ModelState);
                }
                else
                {
                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendMail(model.Email, "Constituencies - Email Account Confirmation", $"<h1>Constituencies - Email Account Confirmation</h1>" +
                        $"To confirm your Email Address, " +
                        $"please click in the following link : </br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    if (response.IsSuccess)
                    {
                        return CreatedAtAction("Register", user);

                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("SendEmailConfirmation/{username}")]
        public async Task<IActionResult> SendEmailConfirmation(string username)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email Not Found.");
                    return BadRequest(ModelState);
                }
                else
                {
                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendMail(user.Email, "SBDF - Email Account Confirmation", $"<h1>SBDF - Email Account Confirmation</h1>" +
                        $"To confirm your Email Address, " +
                        $"please click in the following link : </br></br><a href = \"{tokenLink}\">Confirm Email</a>");

                    if (response.IsSuccess)
                    {
                        return Ok("Confirmation email successfully sent.");

                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            User user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }
            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound("Confirmation link not working.");
            }
            return Ok("Email confirmed");
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                if (user != null)
                {
                    IdentityResult result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return CreatedAtAction(string.Empty, user);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User Not Found.");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.userName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The user-name do not match with any user in the system.");
                    return BadRequest(ModelState);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                _mailHelper.SendMail(user.Email, "SBDF - Password Reset", $"<h1>SBDF - Password Reset</h1>" +
                    $"To set a new password click on the following link:</br></br>" +
                    $"<a href = \"{link}\">Change password</a>");
                return Ok("Instructions for change your password have been sent to your email.");

            }
            return BadRequest(ModelState);
        }

    }


}
