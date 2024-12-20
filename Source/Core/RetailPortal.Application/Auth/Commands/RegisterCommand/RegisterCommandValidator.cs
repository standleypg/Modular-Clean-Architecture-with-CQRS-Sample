using FluentValidation;

namespace RetailPortal.Application.Auth.Commands.RegisterCommand;

public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        this.RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        this.RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        this.RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}