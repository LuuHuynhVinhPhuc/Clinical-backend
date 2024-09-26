namespace ClinicalBackend.Contracts.DTOs
{
    public class PatientDto 
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        // day of birth
        public DateOnly DOB { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        // Get DateTime for signin 
    }
}

