using Election.API.Data;
using Election.API.Data.Entities;
using Election.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Election.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public UsersController(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            return await _context.Users.ToListAsync();
        }

        [HttpGet("Roles/{userName}")]
        public async Task<IList<string>> GetUserAccess(string userName)
        {
            var roles = new List<string>();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return await _userHelper.ListUserRoleAsync(user);
        }

        [HttpPut("AddAccess/{access}")]
        public async Task<ActionResult<User>> AddUserAccess(User user, string access)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            try
            {
                await _userHelper.CheckRoleAsync(access);
                await _userHelper.AddUserToRoleAsync(userDb, access);
                userDb.Access = await _userHelper.ListUserRoleAsync(user);
                return CreatedAtAction("AddUserAccess", userDb);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPut("RemoveAccess/{access}")]
        public async Task<ActionResult<User>> RemoveUserAccess(User user, string access)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            try
            {
                await _userHelper.RemoveFromRoleAsync(userDb, access);
                userDb.Access = await _userHelper.ListUserRoleAsync(user);
                return CreatedAtAction("RemoveUserAccess", userDb);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("EditUser")]
        public async Task<ActionResult<User>> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (_context.Users.Where(u => u.Email == user.Email && u.UserName != user.UserName).Any())
                {
                    ModelState.AddModelError(string.Empty, "This Email is been used by other user.");
                    return BadRequest(ModelState);
                }
                userDb.FirstName = user.FirstName;
                userDb.LastName = user.LastName;
                userDb.Email = user.Email;
                userDb.PhoneNumber = user.PhoneNumber;
                userDb.ImageId = imageId;
                await _userHelper.UpdateUserAsync(userDb);
                return CreatedAtAction("EditUser", userDb);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("UploadPicture")]
        public async Task<ActionResult<User>> EditUserPicture(User user)
        {
            if (ModelState.IsValid)
            {
                var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (userDb.ImageId != Guid.Empty)
                {
                    await _blobHelper.DeleteBlobAsync(userDb.ImageId, "usersimages");
                }
                Guid imageId = Guid.Empty;

                if (user.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(user.ImageFile, "usersimages");

                }
                userDb.ImageId = imageId;
                await _userHelper.UpdateUserAsync(userDb);
                return CreatedAtAction("EditUserPicture", userDb);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("ResetPicture")]
        public async Task<ActionResult<User>> ResetUserPicture(User user)
        {
            if (ModelState.IsValid)
            {
                var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
                if (userDb.ImageId != Guid.Empty)
                {
                    await _blobHelper.DeleteBlobAsync(userDb.ImageId, "usersimages");
                }
                Guid imageId = Guid.Empty;
                userDb.ImageId = imageId;
                await _userHelper.UpdateUserAsync(userDb);
                return CreatedAtAction("ResetUserPicture", userDb);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("ActivateDeactivate")]
        public async Task<ActionResult<User>> ActivateDeactivate(User user)
        {
            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            try
            {
                userDb.Active = !userDb.Active;
                await _userHelper.UpdateUserAsync(userDb);
                return CreatedAtAction("ActivateDeactivate", userDb);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        //TODO implementareldelete users cuando decida q entidades sesalvaran con el usuario qlas creo


        //// DELETE: api/Applicants/5
        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            User user = await _userHelper.GetUserAsync(userName);
            //var statuses = await _context.Status.FirstOrDefaultAsync(s => s.User.UserName == userName);
            //if (statuses != null)
            //{
            //    ModelState.AddModelError(string.Empty, "The user can not be deleted beacuse have  saved Applications/Applicants.");
            //    return BadRequest(ModelState);
            //}
            //var comments = await _context.Comments.FirstOrDefaultAsync(s => s.User.UserName == userName);
            //if (comments != null)
            //{
            //    ModelState.AddModelError(string.Empty, "The user can not be deleted beacuse have saved Comments");
            //    return BadRequest(ModelState);
            //}
            //var doc = await _context.Documents.FirstOrDefaultAsync(s => s.User.UserName == userName);
            //if (doc != null)
            //{
            //    ModelState.AddModelError(string.Empty, "The user can not be deleted beacuse saved Documents");
            //    return BadRequest(ModelState);
            //}
            await _userHelper.DeleteUserAsync(user);
            return NoContent();
        }
    }
}
