using System;
using FluentValidation;
using ProdApi.Dtos;

namespace ProdApi.Validators;

public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
{
    public CreateTaskItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Tytuł nie może być pusty")
            .MaximumLength(100).WithMessage("Maksymalna długość tytułu to 100");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Maksymalna długość opisu to 500");

        RuleFor(x => x.Status)
           .IsInEnum().WithMessage("Status musi być jedną z wartości: ToDo, InProgress, Done");

    }

}
