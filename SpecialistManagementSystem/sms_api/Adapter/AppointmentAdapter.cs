using SpecialistManagementSystem.API.Models;
using SpecialistManagementSystem.Logic;

namespace SpecialistManagementSystem.API.Adapter
{
    public static class AppointmentAdapter
    {
        public static Appointment ConvertAppointmentDtoToAppointment(AppointmentDTO appointmentDTO)
        {
            return new Appointment() {
                Id = appointmentDTO.Id,
                AppointmentCreatedBy = (Specialist)PersonAdapter.ConvertPersonDtoToPerson(appointmentDTO.AppointmentCreatedBy),
                PeopleInvited = PersonAdapter.CovertListPersonDtosToListOfPeople(appointmentDTO.PeopleInvited),
                PeopleAccepted = PersonAdapter.CovertListPersonDtosToListOfPeople(appointmentDTO.PeopleAccepted),
                AppointmentStart = appointmentDTO.AppointmentStart,
                AppointmentEnd = appointmentDTO.AppointmentEnd,
                Summary = appointmentDTO.Summary,
                Status = appointmentDTO.Status };
        }
    }
}
