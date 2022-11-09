using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.DataAccessLayer
{
    public class SpecialistRepository : ISpecialistRepository
    {
        public RepositoryContext RepositoryContext { get; set; }

        public SpecialistRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public void CreateAppointemnt(Appointment appointment)
        {
            RepositoryContext.Set<Appointment>().Add(appointment);
            RepositoryContext.SaveChanges();
        }

        public Specialist CreateSpecialist(Specialist specialist)
        {
            specialist = RepositoryContext.Set<Specialist>().Add(specialist).Entity;
            RepositoryContext.SaveChanges();
            return specialist;
        }

        public void DeleteAppointment(Guid id)
        {
            Appointment appointment = RepositoryContext.Set<Appointment>().First(a => a.Id == id);
            appointment.Status = AppointmentStatus.Cancelled;
            RepositoryContext.SaveChanges();
        }

        public void DeletePatient(Guid id)
        {
            RepositoryContext.Set<Patient>().Remove(p => p.UserId.Equals(id));
            
        }

        public void DeleteSpecialist(Guid id)
        {
            throw new NotImplementedException();
        }

        public void EditSpecialist(Guid id, Specialist specialist)
        {
            throw new NotImplementedException();
        }

        public Specialist GetSpecialist(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Specialist> GetSpecialists()
        {
            throw new NotImplementedException();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}