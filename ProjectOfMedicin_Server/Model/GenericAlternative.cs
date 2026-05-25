using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.Model
{
    public class GenericAlternative:BaseEntity
    {
        private Medication medications { get; set; }
        private List<Medication> mediations { get; set; }
    }
}
