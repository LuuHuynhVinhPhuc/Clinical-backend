using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Domain.Entities
{
    public class PatientsInfo : BaseEntitty<int>
    {
        public required string PatientName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
