using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class FollowUp : BaseEntitty<Guid>, IAuditable
    {
        public Guid PatientId { get; set; }
        //Tổng quát
        public string? CheckUp { get; set; }
        //Tiền căn
        public string? History { get; set; }
        //Chuẩn đoán
        public string? Diagnosis { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

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
