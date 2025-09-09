using System;
using FluentValidation;
using ProdApi.Dtos;

namespace ProdApi.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email nie może być pusty")
            .EmailAddress().WithMessage("Nieprawidłowy format email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło nie może być puste")
            .MinimumLength(6).WithMessage("Minimalna długość hasła to 6 znaków");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Rola musi być jedną z wartości: Admin, User");
    }
}
