using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Logic
{
    public interface IMessageBusClient
    {
        void SendAppointmentCreatedEvent(Appointment appointment);
        void SendAppointmentCancelledEvent(Appointment appointment);
        void SendAppointmentEndedEvent(Appointment appointment);
        void SendAppointmentEditedEvent(Appointment appointment);
    }
}
