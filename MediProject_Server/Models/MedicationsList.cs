using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
  
           public class MedicationsList:List<Medication>
           {
      
            public MedicationsList() { }

            public MedicationsList(IEnumerable<Medication> list) : base(list) { }


            public MedicationsList(IEnumerable<BaseEntity> list) : base(list.Cast<Medication>().ToList()) { }
       
           }
    
}
