using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures
{
    public static class PrescriptionError
    {
        public static readonly Error PrescriptionExist = new Error("Prescriptions.PrescriptionExist", "Prescription already exist");
        
        public static Error NotFound(string Name) => new Error(
        "Prescription.NotFound", $"Prescription with the name '{Name}' was not found");

        public static Error InvalidProductName(string Name) => new Error(
        "Prescription.InvalidProductName", $"Product with the name '{Name}' has its ID not matching with the provided ID");

        // Prescription ID is not exist
        public static Error IDNotFound(Guid ID) => new Error(
        "Prescription.NotFound", $"Prescription with the ID '{ID}' was not found");

        public static Error FollowUpNotFound(Guid ID) => new Error(
        "Prescription.NotFound", $"The Follow-up with ID {ID} was not found");
        // No prescription found for the given date
        public static Error NoPrescriptionFoundForDate(string date) => new Error(
        "Prescription.NotFound", $"No prescription found for the date: {date}");

        public static Error PatientPhoneNotFound(string phoneNumber) => new Error(
        "Patient.PhoneNotFound", $"Patient with the phone number '{phoneNumber}' was not found");

        public static readonly Error InputDateInvalidFormat = new Error("Prescription.InputDateInvalid", "Input date is not in the right format");
    }
}