using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntity<Guid>, IAuditable
    {
        public Guid PatientId { get; set; }
        public Guid FollowUpId { get; set; }
        public ICollection<Product> Medicines { get; set; }
        public DateTime BillDate { get; set; }
        public string Notes { get; set; }
        // IAuditable
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation property
        public virtual Patient Patient { get; set; }
        public virtual FollowUp FollowUp { get; set; }
        public virtual Medicine Medicine { get; set; }
    }

    public class Product {
        public Guid MedicineId {get; set;}
        public int Quantity {get; set;}
    }
}