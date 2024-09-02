using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.ReExaminations
{
    public static class FollowUpErrors
    {
        public static readonly Error FollowUpExists = new Error(
            "FollowUps.FollowUp", "This follow-up already exist");

        public static Error NotFound(string Name) => new Error(
        "FollowUp.NotFound", $"The follow-up with the name '{Name}' was not found");
    }
}