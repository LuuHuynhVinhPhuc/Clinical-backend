namespace ClinicalBackend.Contracts.DTOs
{
    public class QueryFollowUpsResponseDto
    {
        public List<FollowUpDto> FollowUps { get; set; }
        public PaginationInfoDto Pagination { get; set; }
    }
}    
