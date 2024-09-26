using ClinicalBackend.Contracts.DTOs;

namespace ClinicalBackend.Contracts.DTOs
{
    public class FollowUpDto 
    {
        public string Reason { get; set; }
        public string History { get; set; }
        public string Diagnosis { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //Navigation Property
        public PatientDto Patient { get; set; }
    }
}

