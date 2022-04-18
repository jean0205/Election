using Election.API.Data.Entities;
using Election.API.Enum;
using Election.API.Helpers;

namespace Election.API.Data
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
            User user = await _userHelper.GetUserAsync(username);
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
                await _userHelper.AddUserToRoleAsync(user, UserAccess.Voters.ToString());
                await _userHelper.AddUserToRoleAsync(user, UserAccess.App_Configuration.ToString());
            }

            return user;
        }
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserAccess.Voters.ToString());
           // await _userHelper.CheckRoleAsync(UserAccess.Review_Applications.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.Enter_Applications.ToString());
            await _userHelper.CheckRoleAsync(UserAccess.App_Configuration.ToString());
            await _userHelper.CheckRoleAsync(UserAccess.App_Maintenance.ToString());
            await _userHelper.CheckRoleAsync(UserAccess.Manage_Users.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.See_Reports.ToString());
            //await _userHelper.CheckRoleAsync(UserAccess.Approve_Applications.ToString());

        }
    }
}
