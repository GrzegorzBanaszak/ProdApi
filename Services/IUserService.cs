using System;
using ProdApi.Models;

namespace ProdApi.Services;

public interface IUserService
{
    Task<User> RegisterAsync(string email, string password, UserRole role);
    Task<User?> ValidateCredentialsAsync(string email, string password);
    Task<bool> UserExistsAsync(string email);
}
