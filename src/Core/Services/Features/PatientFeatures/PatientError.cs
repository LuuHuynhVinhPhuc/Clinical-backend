using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.PatientFeatures
{
    public static class PatientError
    {
        public static readonly Error PatientNameExist = new Error("Patients.Patient", "Patient name already exist");
        
        public static Error NotFound(string Name) => new Error(
        "Patient.NotFound", $"Patient with the name '{Name}' was not found");

        // Patient phone number is not exist
        public static Error PhoneNotFound(string phone) => new Error(
        "Patient.NotFound", $"Patient with the phone number '{phone}' was not found");

        // Patient phone number is already used
        public static Error PhoneAlreadyExisted(string phone) => new Error(
            "Patient.ExistPhoneNumber", $"Phone number: {phone} has already been used by another patient");

        // Patient ID is not exist
        public static Error IDNotFound(Guid ID) => new Error(
        "Patient.NotFound", $"Patient with the ID '{ID}' was not found");

        // Invalid date of birth format
        public static readonly Error InvalidDOBFormat = new Error("Patient.InvalidDOB", "Date of birth should not exceeds current date!");

        // Input date is not in the right format
        public static readonly Error InputDateInvalidFormat = new Error("Patient.InputDateInvalid", "Input date is not in the right format");

        // No patient found for the given date
        public static Error NoPatientFoundForDate(string date) => new Error(
        "Patient.NotFound", $"No patient found for the date: {date}");
    }
}