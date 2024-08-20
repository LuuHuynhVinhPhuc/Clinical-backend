using ClinicalBackend.Services.Common;
using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.PatientFeatures
{
    public static class PatientError
    {
        // If patient is exist
        public static readonly Error PatientNameExist = new Error("Patients.Patient", "Patient name is already exist");

        // Patient name is not exist
        public static Error NotFound(string Name) => new Error(
        "Patient.NotFound", $"Patient with the name '{Name}' was not found");
    }
}
