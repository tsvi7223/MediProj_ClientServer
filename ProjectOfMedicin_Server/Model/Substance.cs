using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.Model
{
    [DataContract]
     public class Substance:BaseEntity
    {
        [DataMember]
        public string SubstanceName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
