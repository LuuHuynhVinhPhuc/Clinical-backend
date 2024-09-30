using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntitty<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        // day of birth
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        // Get DateTime for signin 
        public DateTime CreatedAt { get; set; } // Ngày tạo 
        public DateTime ModifiedAt { get; set; }  // Thời gian sửa

        // advance property
        public string CheckStatus { get; set; } // đã khám hay 

        public ICollection<FollowUp> FollowUps { get; set; }  // điều hướng 
        //public string Discharge { get; set; }  // xuất viện 

    }
}