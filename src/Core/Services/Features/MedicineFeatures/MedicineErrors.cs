using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.MedicineFeatures
{
    public static class MedicineErrors
    {
        public static readonly Error MedicineNameExist = new Error(
            "Medicines.Medicine", "This medicine is already exist");

        public static Error NotFound(string Name) => new Error(
        "Medicines.NotFound", $"The medicine with the name '{Name}' was not found");
    }
}