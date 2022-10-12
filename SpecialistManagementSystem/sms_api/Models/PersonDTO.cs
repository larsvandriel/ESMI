namespace SpecialistManagementSystem.API.Models
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public PersonType Type { get; set; }
        public Guid KnownIdentificationNumber { get; set; }
        public List<AppointmentDTO> AppointmentsInvitedFor { get; set; } = new List<AppointmentDTO>();
        public List<AppointmentDTO> AppointmentsAccepted { get; set; } = new List<AppointmentDTO>();
        public List<AppointmentDTO> AppointmentCreated { get; set; } = new List<AppointmentDTO>();
    }

    public enum PersonType
    {
        Patient,
        Specialist
    }
}
