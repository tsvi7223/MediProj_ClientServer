using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
     public class Substance:BaseEntity
    {
       
        public string SubstanceName { get; set; }
        public string Description { get; set; }
    }
}
