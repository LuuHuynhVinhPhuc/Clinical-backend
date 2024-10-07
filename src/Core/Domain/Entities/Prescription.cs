using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntitty<int>, IAuditable
    {

        // IAuditable
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation property
        public Guid PatientID { get; set; }
        public virtual required Patient Patient { get; set; }

        // Follow UP
        public Guid FollowUpID { get; set; }
        public virtual FollowUp FollowUp { get; set; }
        // Prescription
        public List<PrescriptionDrug> PrescriptionDrugs { get; set; } = new List<PrescriptionDrug>();
    }

    public class PrescriptionDrug : BaseEntitty<int>
    {
        public Guid MedicineID { get; set; }
        public int PrescriptionID { get; set; }
        public virtual Medicine Medicine { get; set; }
        public string Name { get; set; }
        public string? Time { get; set; }
        public string? Usage { get; set; }
        public string? Note { get; set; }
    }
}