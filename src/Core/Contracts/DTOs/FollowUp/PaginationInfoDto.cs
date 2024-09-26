
namespace ClinicalBackend.Contracts.DTOs
{
    public class PaginationInfoDto
    {
        public int TotalItems { get; set; }
        public int TotalItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

