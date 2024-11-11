using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Medicine : BaseEntitty<Guid>, IAuditable
    {
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? Specialty { get; set; }
        public IList<string> Nutritional { get; set; } = new List<string>(); // Changed to IList<string>
        public string? Dosage { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        // public string? Dosage { get; set; } = dạng bào chế
        // public string? Instructions { get; set; }
        // public string? SideEffects { get; set; }
        // public string? Contraindications { get; set; }
    }

}
