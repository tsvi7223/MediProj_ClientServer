using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [DataContract] // מגדיר את המחלקה כאובייקט שניתן להעביר בין השרת ללקוח בתקשורת WCF
    public class Prescription : BaseEntity
    {
        [DataMember] // מציין שהשדה נכלל במידע שעובר בצינור התקשורת (Serialization)
        public DateTime IssueDate { get; set; } // תאריך הנפקת המרשם

        [DataMember]
        public DateTime ValidFrom { get; set; } // תאריך תחילת תוקף המרשם

        [DataMember]
        public DateTime ValidUntil { get; set; } // תאריך סיום תוקף המרשם
    }
}