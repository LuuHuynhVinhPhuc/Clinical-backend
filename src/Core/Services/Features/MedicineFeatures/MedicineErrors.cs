using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.MedicineFeatures
{
    public static class MedicineErrors
    {
        public static readonly Error MedicineNameExist = new Error(
            "Medicines.Medicine", "A medicine with the same name already exist");

        public static  Error NameNotFound(string Name) => new Error(
        "Medicines.NameNotFound", $"The medicine with the name '{Name}' was not found");

        public static Error IdNotFound(Guid Id) => new Error(
        "Medicines.IdNotFound", $"The medicine with Id '{Id}' was not found");

        public static Error MedicineNotFound(string Id) => new Error(
        "Medicines.NameNotFound", $"The medicine with the name '{Id}' was not found");
    }
}