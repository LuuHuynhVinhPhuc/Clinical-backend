using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class Medicine : BaseEntitty<string>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string? Status { get; set; }
        
        // public string? Dosage { get; set; }
        // public string? Instructions { get; set; }
        // public string? SideEffects { get; set; }
        // public string? Contraindications { get; set; }
    }
}
