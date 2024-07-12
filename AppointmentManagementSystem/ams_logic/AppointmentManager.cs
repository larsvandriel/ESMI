using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Logic
{
    public class AppointmentManager : IAppointmentManager
    {
        readonly IAppointmentRepository _repository;
        readonly IMessageBusClient _messageBusClient;

        public AppointmentManager(IAppointmentRepository repository, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _messageBusClient = messageBusClient;
        }

        public void AcceptAppointment(Guid appointmentId, Person person)
        {
            Appointment appointment = GetAppointment(appointmentId);
            appointment.AcceptAppointment(person);
            _repository.UpdateAppointment(appointment);
        }

        public void CancelAppointment(Guid appointmentId, Person person)
        {
            Appointment appointment = GetAppointment(appointmentId);
            if(!appointment.PeopleInvited.Contains(person))
            {
                throw new InvalidOperationException("You were not invited for this meeting");
            }
            appointment.CancelAppointment();
            _repository.UpdateAppointment(appointment);
            _messageBusClient.SendAppointmentCancelledEvent(appointment);
        }

        public Appointment CreateAppointment(Appointment appointment)
        {
            appointment =  _repository.CreateAppointment(appointment);
            _messageBusClient.SendAppointmentCreatedEvent(appointment);
            return appointment;
        }

        public void DeleteAllMetingsForPatient(Guid patientId)
        {
            Person patient = _repository.GetPersonByPatientId(patientId);
            _repository.GetAllUpcommingAppointments(patient).ForEach(a => CancelAppointment(a.Id, patient));
        }

        public void DeleteAllMetingsForSpecialist(Guid specialistId)
        {
            Person specialist = _repository.GetPersonBySpecialistId(specialistId);
            _repository.GetAllUpcommingAppointments(specialist).ForEach(a => CancelAppointment(a.Id, specialist));
        }

        public void EditAppointment(Appointment appointment, Person person)
        {
            Appointment oldAppointment = GetAppointment(appointment.Id);
            if(oldAppointment == null)
            {
                throw new ArgumentException("The appointment was not found in the system");
            }
            if (!oldAppointment.AppointmentCreatedBy.Equals(person))
            {
                throw new UnauthorizedAccessException("You must be a medical worker that created the appointment for it to end!");
            }
            if (appointment.AppointmentEnd <= appointment.AppointmentStart)
            {
                throw new ArgumentException("The appointment can't end before it started");
            }
            _repository.UpdateAppointment(appointment);
            _messageBusClient.SendAppointmentEditedEvent(appointment);
        }

        public void EndAppointment(Guid appointmentId, Person person, string summary)
        {
            Appointment appointment = GetAppointment(appointmentId);
            if(appointment.AppointmentCreatedBy != person)
            {
                throw new UnauthorizedAccessException("You must be a medical worker that created the appointment for it to end!");
            }
            appointment.EndAppointment(summary);
            _repository.UpdateAppointment(appointment);
            _messageBusClient.SendAppointmentEndedEvent(appointment);
        }

        public Appointment GetAppointmentById(Guid id)
        {
            return _repository.GetAppointment(id);
        }

        public void StartAppointment(Guid appointmentId, Person person)
        {
            Appointment appointment = GetAppointment(appointmentId);
            if (!appointment.AppointmentCreatedBy.Equals(person))
            {
                throw new UnauthorizedAccessException("You must be a medical worker that created the appointment for it to end!");
            }
            appointment.StartAppointment();
            _repository.UpdateAppointment(appointment);
        }

        private Appointment GetAppointment(Guid appointmentId)
        {
            Appointment appointment = _repository.GetAppointment(appointmentId);
            if (appointment == null)
            {
                throw new ArgumentException("Appointment was not found");
            }
            return appointment;
        }
    }
}
