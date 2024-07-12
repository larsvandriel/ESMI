using AppointmentManagementSystem.API.Models;
using AppointmentManagementSystem.Logic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public IAppointmentManager AppointmentManager { get; set; }

        public AppointmentController(IAppointmentManager appointmentManager)
        {
            AppointmentManager = appointmentManager;
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public Appointment Get(Guid id)
        {
            return AppointmentManager.GetAppointmentById(id);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public Appointment Post([FromBody] Appointment appointment)
        {
            return AppointmentManager.CreateAppointment(appointment);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] AppointmentDTO appointmentDTO)
        {
            AppointmentManager.EditAppointment(appointmentDTO.Appointment, appointmentDTO.Person);
        }

        // PATCH api/<AppointmentController>/5/accept
        [HttpPatch("{id}/accept")]
        public void AcceptAppointment([FromBody] AppointmentDTO appointmentDTO)
        {
            AppointmentManager.AcceptAppointment(appointmentDTO.Appointment.Id, appointmentDTO.Person);
        }

        // PATCH api/<AppointmentController>/5/cancel
        [HttpPatch("{id}/cancel")]
        public void CancelAppointment([FromBody] AppointmentDTO appointmentDTO)
        {
            AppointmentManager.CancelAppointment(appointmentDTO.Appointment.Id, appointmentDTO.Person);
        }

        // PATCH api/<AppointmentController>/5/Start
        [HttpPatch("{id}/start")]
        public void StartAppointment([FromBody] AppointmentDTO appointmentDTO)
        {
            AppointmentManager.StartAppointment(appointmentDTO.Appointment.Id, appointmentDTO.Person);
        }

        // PATCH api/<AppointmentController>/5/End
        [HttpPatch("{id}/end")]
        public void EndAppointment([FromBody] EndAppointmentDTO endAppointmentDTO)
        {
            AppointmentManager.EndAppointment(endAppointmentDTO.Appointment.Id, endAppointmentDTO.Person, endAppointmentDTO.Summary);
        }
    }
}
