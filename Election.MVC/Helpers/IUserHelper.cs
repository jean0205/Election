﻿using Microsoft.AspNetCore.Identity;
using Election.MVC.Data.Entities;
using Election.MVC.Models;

namespace Election.MVC.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);
        Task<User> GetUserAsyncUserName(string email);

        Task<User> GetUserAsync(Guid userId);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<User> AddUserAsync(AddUserViewModel model);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<IdentityResult> UpdateUserAsync(User user);
    }
}
