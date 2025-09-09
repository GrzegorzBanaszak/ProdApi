using System;
using ProdApi.Models;

namespace ProdApi.Dtos;

public record RegisterDto(string Email, string Password, UserRole Role);
