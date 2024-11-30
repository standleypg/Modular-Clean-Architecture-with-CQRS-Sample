using FluentValidation;

namespace RetailPortal.Application.Commands.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
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