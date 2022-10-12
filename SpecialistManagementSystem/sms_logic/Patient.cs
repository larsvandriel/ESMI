using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialistManagementSystem.Logic
{
    public class Patient: Person
    {
        public List<Specialist> TreadedBy { get; set; }
    }
}
