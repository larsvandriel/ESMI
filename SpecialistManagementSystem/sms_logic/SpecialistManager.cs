using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialistManagementSystem.Logic
{
    public class SpecialistManager : ISpecialistManager
    {
        readonly ISpecialistRepository _repository;
        readonly IMessageBusClient _messageBusClient;

        public SpecialistManager(ISpecialistRepository repository, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _messageBusClient = messageBusClient;
        }
        
        public void AppointmentCancelled(Appointment appointment)
        {
            _repository.DeleteAppointment(appointment.Id);
        }

        public void AppointmentCreated(Appointment appointment, List<Specialist> specialists)
        {
            foreach (Specialist specialist in specialists)
            {
                specialist.Appointments.Add(appointment);
                _repository.EditSpecialist(specialist.UserId, specialist);
            }
        }

        public void AppointmentEdited(Appointment appointment)
        {
            _repository.UpdateAppointment(appointment);
        }

        public Specialist CreateSpecialist(Specialist specialist)
        {
            return _repository.CreateSpecialist(specialist);
        }

        public void DeleteSpecialist(Guid id)
        {
            Specialist specialist = _repository.GetSpecialist(id);
            _repository.DeleteSpecialist(specialist.UserId);
            _messageBusClient.SendSpecialistDeletedEvent(specialist);
        }

        public void EditSpecialist(Guid id, Specialist specialist)
        {
            _repository.EditSpecialist(id, specialist);
        }

        public Specialist GetSpecialist(Guid id)
        {
            return _repository.GetSpecialist(id);
        }

        public List<Specialist> GetSpecialists()
        {
            return _repository.GetSpecialists();
        }

        public void PatientDeleted(Patient patient)
        {
            _repository.DeletePatient(patient.UserId);
        }
    }
}
