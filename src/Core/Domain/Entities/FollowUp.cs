using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class FollowUp : BaseEntitty<string>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public PatientsInfo Patient {get; set;}
        //Tổng quát
        public string? CheckUp { get; set; }
        //Tiền căn
        public string? History { get; set; }
        //Chuẩn đoán
        public string? Diagnosis { get; set; }

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
