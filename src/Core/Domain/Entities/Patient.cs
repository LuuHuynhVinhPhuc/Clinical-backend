using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntitty<int>
    {
        public string? Name { get; set; }
    }
}
