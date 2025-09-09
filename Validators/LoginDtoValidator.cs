using System;
using FluentValidation;
using ProdApi.Dtos;

namespace ProdApi.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email nie może być pusty")
            .EmailAddress().WithMessage("Nieprawidłowy format email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło nie może być puste")
            .MinimumLength(6).WithMessage("Minimalna długość hasła to 6 znaków");
    }
}
