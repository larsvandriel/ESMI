using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialistManagementSystem.Logic
{
    public interface IMessageBusClient
    {
        void SendSpecialistDeletedEvent(Specialist specialist);
    }
}
