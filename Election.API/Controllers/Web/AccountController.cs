
using Election.API.Data;
using Election.API.Data.Entities;
using Election.API.Helpers;
using Election.API.Models.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Election.API.Controllers.Web

{  
 
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly DataContext _context;

        public AccountController(DataContext context,IUserHelper userHelper, IConfiguration configuration, IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _configuration = configuration;
            this._mailHelper = mailHelper;
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.userName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The user-name do not match with any user in the system.");
                    return View(model);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                _mailHelper.SendMail(user.Email, "Constituencies Project - Password Reset", $"<h1>Constituencies Project - Password Reset</h1>" +
                    $"To set a new password click on the following link:</br></br>" +
                    $"<a href = \"{link}\">Change password</a>");
                ViewBag.Message = "Instructions for change your password have been sent to your email.";
                return View();

            }

            return View(model);
        }

        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            User user = await _userHelper.GetUserAsync(model.UserName);
            if (user != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Password changed.";
                    return View();
                }

                ViewBag.Message = "Error updating password.";
                return View(model);
            }

            ViewBag.Message = "User not found.";
            return View(model);
        }

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        
        //public async Task<ActionResult<IEnumerable<User>>> OnlineUsers()
        //{
        //   var online= await _context.Users.Where(u => u.Online == true).ToListAsync();
              
        //    return View(online);
        //}
    }
}
