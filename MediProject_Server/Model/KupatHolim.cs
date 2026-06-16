using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.Model
{
    // הגדרת תכונה המציינת שהמחלקה הזו ניתנת לסריאליזציה ויכולה לעבור ברשת באמצעות WCF
    [DataContract]
    //הגדרת מחלקת קופת חולים שיורשת הבייס
    public class KupatHolim : BaseEntity
    {
        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את שם קופת החולים עם אפשרות קריאה וכתיבה
        public string Name { get; set; }
    }
}