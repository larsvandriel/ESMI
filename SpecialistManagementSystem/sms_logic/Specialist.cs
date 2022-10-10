using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialistManagementSystem.Logic
{
    public class Specialist: Person
    {
        public List<Patient> PatientsTreating { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<string> ListOfProfessions { get; set; }
        public string WorkEmail { get; set; }
        public bool Deleted { get; set; }
    }
}
