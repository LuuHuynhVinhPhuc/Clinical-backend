using ClinicalBackend.Domain.Common;

namespace ClinicalBackend.Services.Features.UserFeatures
{
    public static class UserErrors
    {
        public static readonly Error UserNameExist = new Error(
            "Users.UsernameExist", "This username is already exist");
        public static readonly Error IncorrectLoginInfo = new Error(
            "Users.UncorrectLoginInfo", "Login information uncorrect");

        public static Error NotFound(string userName) => new Error(
        "Users.NotFound", $"The user with user name '{userName}' was not found");
    }
}
