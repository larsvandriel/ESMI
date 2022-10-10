namespace SpecialistManagementSystem.Logic
{
    public abstract class Person
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PhysicalGender PhysicalGender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalEmail { get; set; }
        public List<Appointment> AppointmentsInvitedFor { get; set; } = new List<Appointment>();
        public List<Appointment> AppointmentsAccepted { get; set; } = new List<Appointment>();
    }
}