using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
    public class Price:BaseEntity
    {    
       
        public int fullPrice { get; set; }
        public Medication medication { get; set; }
        public KupatHolim kupa { get; set; }
        public int discount { get; set; }
            
    }
}
