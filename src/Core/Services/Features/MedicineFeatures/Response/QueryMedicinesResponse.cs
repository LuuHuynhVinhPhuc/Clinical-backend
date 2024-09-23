using ClinicalBackend.Domain.Entities;

namespace ClinicalBackend.Services.Features.MedicineFeatures.Response
{
    public class QueryMedicinesResponse
    {
        public List<Medicine> Medicines { get; set; }
        public PaginationInfo Pagination { get; set; }
    }

    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}