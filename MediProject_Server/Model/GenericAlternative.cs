using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.Model
{
    [DataContract]
    public class GenericAlternative : BaseEntity
    {
        [DataMember]
        private Medication medications { get; set; }

        [DataMember]
        private List<Medication> medicationList { get; set; }
    }
}