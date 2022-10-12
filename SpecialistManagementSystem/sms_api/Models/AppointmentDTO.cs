using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.API.Models
{
    public class AppointmentDTO
    {
        public Guid Id { get; set; }
        public PersonDTO AppointmentCreatedBy { get; set; }
        public List<PersonDTO> PeopleInvited { get; } = new List<PersonDTO>();
        public List<PersonDTO> PeopleAccepted { get; } = new List<PersonDTO>();
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public AppointmentStatus Status { get; private set; }
        public string? Summary { get; set; }
    }
}
