using Election.MVC.Data.Entities;
using Election.MVC.Enums;
using Election.MVC.Helpers;

namespace Election.MVC.Data
{
    public class SeedDB

    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            //await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Jean", "Soto", "jean0205", "jeanc.soto0205@gmail.com", "4734170097");


        }
        private async Task<User> CheckUserAsync(string firstName, string lastName, string username, string email, string phone)
        {
            User user = await _userHelper.GetUserAsyncUserName(username);
            if (user == null)
            {
                user = new User
                {
                    Active = true,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = username,
                    PhoneNumber = phone,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, UserType.Admin.ToString());
               // await _userHelper.AddUserToRoleAsync(user, UserType.User.ToString());
            }

            return user;
        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
            // await _userHelper.CheckRoleAsync(UserAccess.Review_Applications.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.Enter_Applications.ToString());
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.App_Maintenance.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.Manage_Users.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.See_Reports.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.Approve_Applications.ToString());

        }
    }
}
