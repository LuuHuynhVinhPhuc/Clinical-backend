using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class FollowUp : BaseEntitty<Guid>, IAuditable
    {
        public Guid? PatientId { get; set; }
        public string? Reason { get; set; }
        public string? History { get; set; }
        public string? Diagnosis { get; set; }
        public string? Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation entity
        public virtual Patient Patient { get; set; }


        // public float Price { get; set; }
        // public string? Status { get; set; }
        // public string? Type { get; set; }
        // public DateTime dateTime {get; set;}
        // public string? Dosage { get; set; }
        // public string? Instructions { get; set; }
        // public string? SideEffects { get; set; }
        // public string? Contraindications { get; set; }
    }
}