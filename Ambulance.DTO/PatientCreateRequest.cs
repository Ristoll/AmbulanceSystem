namespace Ambulance.DTO
{
    public class PatientCreateRequest
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Gender { get; set; }
    }
}
