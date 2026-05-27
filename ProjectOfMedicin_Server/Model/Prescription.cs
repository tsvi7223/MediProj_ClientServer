using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.Model
{
    [DataContract] 
    public class Prescription : BaseEntity
    {
        [DataMember] 
        public DateTime IssueDate { get; set; }

        [DataMember] 
        public DateTime ValidFrom { get; set; }

        [DataMember] 
        public DateTime ValidUntil { get; set; }
    }
}