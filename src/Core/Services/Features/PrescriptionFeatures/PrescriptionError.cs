﻿using ClinicalBackend.Services.Common;

namespace ClinicalBackend.Services.Features.PrescriptionFeatures
{
    public static class PrescriptionError
    {
        public static readonly Error PrescriptionExist = new Error("Prescriptions.PrescriptionExist", "Prescription already exist");
        
        public static Error NotFound(string Name) => new Error(
        "Prescription.NotFound", $"Prescription with the name '{Name}' was not found");

        // Prescription ID is not exist
        public static Error IDNotFound(Guid ID) => new Error(
        "Prescription.NotFound", $"Prescription with the ID '{ID}' was not found");

        // No prescription found for the given date
        public static Error NoPrescriptionFoundForDate(string date) => new Error(
        "Prescription.NotFound", $"No prescription found for the date: {date}");
    }
}