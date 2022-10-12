namespace SpecialistManagementSystem.Logic
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Specialist AppointmentCreatedBy { get; set; }
        public List<Person> PeopleInvited { get; set; }
        public List<Person> PeopleAccepted { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Summary { get; set; }
    }
}