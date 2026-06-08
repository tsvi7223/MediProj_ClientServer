using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [DataContract]
    public class Price:BaseEntity
    {
        [DataMember]
        public int fullPrice { get; set; }
        [DataMember]
        public Medication medication { get; set; }
        [DataMember]
        public KupatHolim kupa { get; set; }
        [DataMember]
        public int discount { get; set; }
            
    }
}
