using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Logic
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Person AppointmentCreatedBy { get; set; }
        public List<Person> PeopleInvited { get; } = new List<Person>();
        public List<Person> PeopleAccepted { get; } = new List<Person>();
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public AppointmentStatus Status { get; private set; }
        public string? Summary { get; set; }

        public Appointment(Person appointmentCreatedBy, List<Person> peopleInvited, DateTime appointmentStart, DateTime appointmentEnd)
        {
            if(appointmentEnd <= appointmentStart)
            {
                throw new ArgumentException("The appointment can't end before it started!");
            }
            if(appointmentStart <= DateTime.Now)
            {
                throw new ArgumentOutOfRangeException("The appointment can't take place in the past!");
            }
            AppointmentCreatedBy = appointmentCreatedBy;
            PeopleInvited = peopleInvited ?? throw new ArgumentNullException(nameof(peopleInvited));
            AppointmentStart = appointmentStart;
            AppointmentEnd = appointmentEnd;
            Status = AppointmentStatus.Pending;
            PeopleAccepted.Add(appointmentCreatedBy);
        }

        public void StartAppointment()
        {
            if(Status != AppointmentStatus.Accepted && Status != AppointmentStatus.Pending)
            {
                throw new InvalidOperationException("The appointment cannot be started, because it is cancelled or already started!");
            }
            Status = AppointmentStatus.Started;
        }

        public void EndAppointment(string summary)
        {
            if(Status != AppointmentStatus.Started)
            {
                throw new InvalidOperationException("The appointment cannot be ended before it started!");
            }
            if(string.IsNullOrEmpty(summary) || summary.Length < 75)
            {
                throw new ArgumentNullException(nameof(summary));
            }
            Summary = summary;
            Status = AppointmentStatus.Ended;
        }

        public void AcceptAppointment(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("The person was null!");
            }
            if (Status == AppointmentStatus.Cancelled)
            {
                throw new InvalidOperationException("The appointment was already cancelled!");
            }
            if (Status == AppointmentStatus.Started)
            {
                throw new InvalidOperationException("The appointment was already started!");
            }
            if (!PeopleInvited.Contains(person))
            {
                throw new ArgumentException("Person was not invited for appointment!");
            }
            if(PeopleAccepted.Contains(person))
            {
                throw new ArgumentException("Person already accepted the appointment");
            }
            PeopleAccepted.Add(person);

            if(!PeopleInvited.Except(PeopleAccepted).Any())
            {
                Status = AppointmentStatus.Accepted;
            }
        }

        public void CancelAppointment()
        {
            if (Status == AppointmentStatus.Ended)
            {
                throw new InvalidOperationException("The appointment already happenned!");
            }
            Status = AppointmentStatus.Cancelled;
        }

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   EqualityComparer<Person>.Default.Equals(AppointmentCreatedBy, appointment.AppointmentCreatedBy) &&
                   EqualityComparer<List<Person>>.Default.Equals(PeopleInvited, appointment.PeopleInvited) &&
                   AppointmentStart == appointment.AppointmentStart &&
                   AppointmentEnd == appointment.AppointmentEnd &&
                   Status == appointment.Status;
        }
    }
}
