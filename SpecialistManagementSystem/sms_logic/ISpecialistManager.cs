using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialistManagementSystem.Logic
{
    public interface ISpecialistManager
    {
        Specialist CreateSpecialist(Specialist specialist);
        void EditSpecialist(Guid id, Specialist specialist);
        void DeleteSpecialist(Guid id);
        Specialist GetSpecialist(Guid id);
        List<Specialist> GetSpecialists();
        void AppointmentCreated(Appointment appointment, List<Specialist> specialists);
        void AppointmentCancelled(Appointment appointment);
        void AppointmentEdited(Appointment appointment);
        void PatientDeleted(Patient patient);
    }
}
