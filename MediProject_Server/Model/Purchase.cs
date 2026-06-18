using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [DataContract] // מגדיר את המחלקה כאובייקט שניתן להעביר בין השרת ללקוח בתקשורת WCF
    [KnownType(typeof(BaseEntity))] // הוסף את השורה הזו
    public class Purchase : BaseEntity
    {
        [DataMember]
        public DateTime PurchaseDate { get; set; }

        [DataMember]
        public User user { get; set; }

        [DataMember]
        public int PillsPerDay { get; set; }

        [DataMember]
        public int PillAmount { get; set; }

        [DataMember]
        public Medication medication { get; set; }
    }
}