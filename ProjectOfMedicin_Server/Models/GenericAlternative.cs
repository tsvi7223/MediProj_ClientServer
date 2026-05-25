using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
    public class GenericAlternative:BaseEntity
    {
        private Medication medications { get; set; }
        private List<Medication> mediations { get; set; }
    }
}
