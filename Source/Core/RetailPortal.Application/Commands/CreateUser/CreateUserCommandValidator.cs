using FluentValidation;

namespace RetailPortal.Application.Commands.AddUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
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