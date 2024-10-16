using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}