using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.Model
{
     public class Substance:BaseEntity
    {
       
        public string SubstanceName { get; set; }
        public string Description { get; set; }
    }
}
