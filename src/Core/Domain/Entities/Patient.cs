using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class Patient : BaseEntitty<Guid>
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        // day of birth
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        // Get DateTime for signin
        public DateOnly CreatedAt { get; set; }
    }
}