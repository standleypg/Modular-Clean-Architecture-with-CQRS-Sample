using FluentValidation;

namespace RetailPortal.Application.Auth.Queries;

public class LoginQueryValidator: AbstractValidator<LoginQuery>
{
      public LoginQueryValidator()
      {
          this.RuleFor(x => x.Email).NotEmpty();
          this.RuleFor(x => x.Password).NotEmpty();
      }
}