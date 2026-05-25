using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.Model
{
    public class Medication:BaseEntity
    {
       
        public string OriginalName { get; set; }
       public List<String> Alternativies { get; set; }
       public List<GenericAlternative> generics { get; set; }
       public Substance ActiveSubstance { get; set; }
       public bool InHealthBox { get; set; }
       public bool AvailableInIsrael { get; set; }


    }
}
