using AppointmentManagementSystem.Logic;

namespace AppointmentManagementSystem.API.Models
{
    public class EndAppointmentDTO
    {
        public Appointment Appointment { get; set; }
        public Person Person { get; set; }
        public string Summary { get; set; }
    }
}
