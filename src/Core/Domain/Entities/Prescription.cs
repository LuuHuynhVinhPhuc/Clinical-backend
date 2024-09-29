using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntitty<int>, IAuditable
    {
        public Guid PatientID { get; set; }
        
        // IAuditable
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation property
        public virtual Medicine Medicine { get; set; }
        public virtual Patient Patient { get; set; }

        // Props
        // Patient
        public string? PatientName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        // Follow UP
        public string? Summary { get; set; }

        // Prescription
        public List<PrescriptionDrug> PrescriptionDrugs { get; set; } = new List<PrescriptionDrug>();
    }

    public class PrescriptionDrug
    {
        public Guid MedicineID { get; set; }
        public virtual Medicine Medicine { get; set; }
        public string Name { get; set; }
        public int Morning { get; set; }
        public int Noon { get; set; }
        public int Night { get; set; }
        public string? Usage { get; set; }
        public string? Note { get; set; }
    }
}
