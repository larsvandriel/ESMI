namespace AppointmentManagementSystem.Logic
{
    public class Person
    {
        public Guid Id { get; set; }
        public PersonType Type { get; set; }
        public Guid KnownIdentificationNumber { get; set; }
        public List<Appointment> AppointmentsInvitedFor { get; set; } = new List<Appointment>();
        public List<Appointment> AppointmentsAccepted { get; set; } = new List<Appointment>();
        public List<Appointment> AppointmentCreated { get; set; } = new List<Appointment>();

        public override bool Equals(object? obj)
        {
            return obj is Person person &&
                   Type == person.Type &&
                   KnownIdentificationNumber.Equals(person.KnownIdentificationNumber);
        }
    }
}