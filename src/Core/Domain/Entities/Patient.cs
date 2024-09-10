using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
