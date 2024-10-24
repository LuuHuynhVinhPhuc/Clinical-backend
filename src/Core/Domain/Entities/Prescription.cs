using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntitty<Guid>, IAuditable
    {
        public Guid PatientId { get; set; }
        public Guid FollowUpId { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime BillDate { get; set; }
        public string? Notes { get; set; }
        public float TotalPrice { get; set; }
        // IAuditable
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation property
        public virtual Patient Patient { get; set; }
        public virtual FollowUp FollowUp { get; set; }
    }

    public class Product
    {
        public Guid MedicineId { get; set; }
        public int Quantity { get; set; }
        public Instructions Instructions { get; set; }
        //Navigation property
        public virtual Medicine Medicine { get; set; }

    }
    
    public class Instructions
    {
        public string? Day { get; set; }
        public string? Lunch { get; set; }
        public string? Afternoon { get; set; }
        public string? Manual { get; set; }
    }
}