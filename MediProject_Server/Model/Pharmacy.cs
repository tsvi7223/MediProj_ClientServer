using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [DataContract]
    public class Pharmacy:BaseEntity
    {

        [DataMember]
        public string Name { get; set; }
    }
}
