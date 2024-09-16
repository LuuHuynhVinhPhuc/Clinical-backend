using ClinicalBackend.Domain.Entities;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Response
{
    public class QueryMedicinesResponse
    {
        public List<Medicine> Medicines { get; set; }
    }
}