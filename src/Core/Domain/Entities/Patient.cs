using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntitty<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
