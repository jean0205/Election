
using Election.API.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Election.API.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string userName);
        Task<User> GetUserAsync(Guid id);

        Task<User> AddUserAsync(User user, string password);
        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);
        Task<IList<string>> ListUserRoleAsync(User user);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<IdentityResult> RemoveFromRoleAsync(User user, string role);
        Task LogoutAsync();

        Task<SignInResult> ValidatePasswordAsync(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        public Task<IdentityResult> DeleteUserAsync(User user);

    }
}
