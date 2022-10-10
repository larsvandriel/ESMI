using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialistManagementSystem.Logic
{
    public interface ISpecialistRepository
    {
        Specialist CreateSpecialist(Specialist specialist);
        void EditSpecialist(Guid id, Specialist specialist);
        void DeleteSpecialist(Guid id);
        Specialist GetSpecialist(Guid id);
        List<Specialist> GetSpecialists();
        void CreateAppointemnt(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Guid id);
        void DeletePatient(Guid id);
    }
}
