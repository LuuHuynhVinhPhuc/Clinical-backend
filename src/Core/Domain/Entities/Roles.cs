using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class Role : BaseEntitty<int>
    {
        public string Name { get; set; }
    }
}