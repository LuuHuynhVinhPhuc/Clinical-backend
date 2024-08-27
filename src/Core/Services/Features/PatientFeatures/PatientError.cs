using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.PatientFeatures
{
    public static class PatientError
    {
        // If patient is exist
        public static readonly Error PatientNameExist = new Error("Patients.Patient", "Patient name is already exist");

        // If patient is exist
        public static readonly Error PatientListNotFound = new Error("Patients.Patient", "Patient list is not found in DB");

        // Patient name is not exist
        public static Error NotFound(string Name) => new Error(
        "Patient.NotFound", $"Patient with the name '{Name}' was not found");

        // Patient phone number is not exist
        public static Error NotFoundPhone(string phone) => new Error(
        "Patient.NotFound", $"Patient with the phone number '{phone}' was not found");

        // Patient ID is not exist
        public static Error NotFoundID(Guid ID) => new Error(
        "Patient.NotFound", $"Patient with the phone number '{ID}' was not found");
    }
}
