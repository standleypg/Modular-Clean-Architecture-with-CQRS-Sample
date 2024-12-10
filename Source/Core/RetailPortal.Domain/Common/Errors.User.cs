using ErrorOr;

namespace RetailPortal.Domain.Common;

public partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail() =>
            Error.Conflict(
                code: "User.DuplicateEmail",
                description: $"The email is already in use."
            );
    }
}