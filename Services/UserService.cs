using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProdApi.Context;
using ProdApi.Models;

namespace ProdApi.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User> _hasher;
    private readonly TaskDbContext _db;

    public UserService(IPasswordHasher<User> hasher, TaskDbContext db)
    {
        _hasher = hasher;
        _db = db;
    }
    public async Task<User> RegisterAsync(string email, string password, UserRole role)
    {
        var user = User.Create(email, role);
        user.SetPassword(_hasher.HashPassword(user, password));
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;

    }

    public async Task<bool> UserExistsAsync(string email)
    {
        return await _db.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> ValidateCredentialsAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.HashPassword, password);
        return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded ? user : null;
    }
}
