using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Logic
{
    public interface IAppointmentRepository
    {
        Appointment CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        Appointment GetAppointment(Guid id);
        Person GetPersonByPatientId(Guid patientId);
        List<Appointment> GetAllUpcommingAppointments(Person person);
        Person GetPersonBySpecialistId(Guid specialistId);
    }
}
