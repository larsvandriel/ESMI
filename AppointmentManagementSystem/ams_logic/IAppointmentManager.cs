using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Logic
{
    public interface IAppointmentManager
    {
        Appointment CreateAppointment(Appointment appointment);
        void AcceptAppointment(Guid appointmentId, Person person);
        void CancelAppointment(Guid appointmentId, Person person);
        void StartAppointment(Guid appointmentId, Person person);
        void EndAppointment(Guid appointmentId, Person person, string summary);
        void EditAppointment(Appointment appointment, Person person);
        void DeleteAllMetingsForSpecialist(Guid specialistId);
        void DeleteAllMetingsForPatient(Guid patientId);
        Appointment GetAppointmentById(Guid id);
    }
}
