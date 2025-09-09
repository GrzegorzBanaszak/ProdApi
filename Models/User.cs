using System;

namespace ProdApi.Models;

public sealed class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Email { get; private set; }
    public string HashPassword { get; private set; }
    public UserRole Role { get; private set; }


    private User() { }
    private User(string email, UserRole? role)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        Email = email;
        Role = role ?? UserRole.User;
    }

    public static User Create(string email, UserRole role)
    {
        return new User(email, role);
    }

    public void SetPassword(string password)
    {
        HashPassword = password;
    }



}
