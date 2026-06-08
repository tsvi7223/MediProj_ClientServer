using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [DataContract]
    public class Medication : BaseEntity
    {
        [DataMember]
        public string OriginaLastName { get; set; }

        [DataMember]
        public List<string> Alternativies { get; set; } 

        [DataMember]
        public List<GenericAlternative> Generics { get; set; } 

        [DataMember]
        public Substance ActiveSubstance { get; set; }

        [DataMember]
        public bool InHealthBox { get; set; }

        [DataMember]
        public bool AvailableInIsrael { get; set; }
    }
}
