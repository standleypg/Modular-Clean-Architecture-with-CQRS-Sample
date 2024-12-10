using ErrorOr;

namespace RetailPortal.Domain.Common;

public partial class Errors
{
    public static class Auth
    {
        public static Error InvalidCredentials() =>
            Error.Validation(
                code: "Auth.InvalidCredentials",
                description: "Invalid credentials."
            );
    }
}