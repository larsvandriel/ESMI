using AppointmentManagementSystem.Logic;

namespace AppointmentManagementSystem.DataAccessLayer
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public RepositoryContext RepositoryContext { get; set; }

        public AppointmentRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public Appointment CreateAppointment(Appointment appointment)
        {
            appointment = RepositoryContext.Set<Appointment>().Add(appointment).Entity;
            RepositoryContext.SaveChanges();
            return appointment;
        }

        public Appointment GetAppointment(Guid id)
        {
            return RepositoryContext.Set<Appointment>().First(a => a.Id.Equals(id));
        }

        public void UpdateAppointment(Appointment appointment)
        {
            RepositoryContext.Set<Appointment>().Update(appointment);
            RepositoryContext.SaveChanges();
        }

        public Person GetPersonByPatientId(Guid patientId)
        {
            return RepositoryContext.Set<Person>().First(p => p.Type == PersonType.Patient && p.KnownIdentificationNumber.Equals(patientId));
        }

        public List<Appointment> GetAllUpcommingAppointments(Person person)
        {
            return RepositoryContext.Set<Appointment>().Where(a => a.AppointmentStart > DateTime.Now && a.PeopleInvited.Contains(person)).ToList();
        }

        public Person GetPersonBySpecialistId(Guid SpecialistId)
        {
            return RepositoryContext.Set<Person>().First(p => p.Type == PersonType.Specialist && p.KnownIdentificationNumber.Equals(SpecialistId));
        }
    }
}