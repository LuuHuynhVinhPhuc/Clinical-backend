using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntitty<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } // Ngày tạo 
        public DateTime ModifiedAt { get; set; }  // Thời gian sửa

        public string CheckStatus { get; set; } // đã khám hay 

        //Navigation entity
        public virtual ICollection<FollowUp> FollowUps { get; set; }  // điều hướng 
        //public string Discharge { get; set; }  // xuất viện 

    }
}