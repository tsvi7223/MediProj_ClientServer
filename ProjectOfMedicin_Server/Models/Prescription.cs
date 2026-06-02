using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
    public class Prescription:BaseEntity
    {
        
        public DateTime issueDate { get; set; }
        public DateTime validFrom { get; set; }
        public DateTime validUntil { get; set; }
    }
}
