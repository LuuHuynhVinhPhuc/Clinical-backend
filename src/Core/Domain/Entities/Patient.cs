using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntity<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string CheckStatus { get; set; }

        //IAuditable
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; } 


        //Navigation entity
        public virtual ICollection<FollowUp> FollowUps { get; set; } 
    }
}