using FluentValidation;

namespace RetailPortal.Application.Auth.Commands.TokenExchange;

public class TokenExchangeValidator: AbstractValidator<TokenExchangeCommand>
{
    public TokenExchangeValidator()
    {
        this.RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        this.RuleFor(x => x.Name)
            .NotEmpty();

        this.RuleFor(x => x.TokenProvider)
            .NotEmpty();
    }
}