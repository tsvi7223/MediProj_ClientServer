using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.Model
{
    public class Prescription:BaseEntity
    {
        
        public DateTime issueDate { get; set; }
        public DateTime validFrom { get; set; }
        public DateTime validUntil { get; set; }
    }
}
