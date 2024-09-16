using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Medicine : BaseEntitty<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        // public string? Dosage { get; set; }
        // public string? Instructions { get; set; }
        // public string? SideEffects { get; set; }
        // public string? Contraindications { get; set; }
    }
}