using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntitty<int>
    {
        public string name { get; set; }
        


        public Guid PatientID { get; set; }
    }
}
