using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.FollowUps
{
    public static class FollowUpErrors
    {
        public static readonly Error FollowUpExists = new Error(
            "FollowUps.FollowUp", "This follow-up already exist");

        public static Error NotFound(string Id) => new Error(
        "FollowUp.NotFound", $"The follow-up with ID '{Id}' was not found");

        public static Error FollowUpNotExists(Guid id) => new Error(
           "FollowUp.NotFound", $"No medical record found for patient ID: {id}");
    }
}