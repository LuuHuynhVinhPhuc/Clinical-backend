using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Medicine : BaseEntitty<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }

        public Instructions Instructions { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        // public string? Dosage { get; set; }
        // public string? Instructions { get; set; }
        // public string? SideEffects { get; set; }
        // public string? Contraindications { get; set; }
    }

    public class Instructions
    {
        public string? Day { get; set; }
        public string? Lunch { get; set; }
        public string? Afternoon { get; set; }
        public string? Manual { get; set; }
    }
}
