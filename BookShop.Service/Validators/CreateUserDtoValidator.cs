using BookShop.Service.DTOs.User;
using FluentValidation;

namespace BookShop.Service.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(u => u.Username)
            .NotNull()
            .Length(1, 50)
            .NotEqual("sardorbek").WithMessage("Sardorbek already exists!");

        RuleFor(e => e.Password)
            .Equal(e => e.ConfirmPassword)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(6).WithMessage("Your password length must be at least 6.")
            .MaximumLength(20).WithMessage("Your password length must not exceed 20.");
        //  .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
        //  .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
        // .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number."); 

        //RuleFor(e => e.Password)
        //    .Equal(e => e.ConfirmPassword)
        //    .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
    }
}