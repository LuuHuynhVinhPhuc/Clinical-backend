using Domain.Entities;

namespace ClinicalBackend.Domain.Entities
{
    public class User : BaseEntitty<Guid>
    {
        public string UserName { get; set; }
        public string HashPassword { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
