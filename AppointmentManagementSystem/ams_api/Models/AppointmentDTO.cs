using AppointmentManagementSystem.Logic;

namespace AppointmentManagementSystem.API.Models
{
    public class AppointmentDTO
    {
        public Appointment Appointment { get; set; }
        public Person Person { get; set; }
    }
}
