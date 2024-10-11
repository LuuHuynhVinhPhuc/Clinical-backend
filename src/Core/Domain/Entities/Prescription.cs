using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntitty<int>, IAuditable
    {
        public string Name { get; set; }
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Summary { get; set; }
        public string? MedicineName { get; set; }

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation property
        public virtual Patient Patient { get; set; }
        public virtual FollowUp FollowUp { get; set; }
        public virtual Medicine Medicine { get; set; }
        // Prescription
        public List<PrescriptionDrug> PrescriptionDrugs { get; set; } = new List<PrescriptionDrug>();
    }

    public class PrescriptionDrug : BaseEntitty<int>
    {
        public string? Time { get; set; }
        public string? Usage { get; set; }
        public string? Note { get; set; }
    }
}