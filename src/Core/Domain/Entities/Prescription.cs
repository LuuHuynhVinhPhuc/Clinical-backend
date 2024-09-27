using Domain.Entities;
using Domain.Interfaces;

namespace ClinicalBackend.Domain.Entities
{
    public class Prescription : BaseEntitty<int>, IAuditable
    {
        public Guid PatientID { get; set; }
        

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation property
        public virtual Medicine Medicine { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
