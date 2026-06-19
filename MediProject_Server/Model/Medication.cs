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
    // הגדרת מחלקה ציבורית המייצגת תרופה ויורשת ממחלקת הבסיס BaseEntity
    public class Medication : BaseEntity
    {
        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את השם המקורי של התרופה עם אפשרות קריאה וכתיבה
        public string OriginalName { get; set; }

       
        [DataMember]
        // מאפיין ציבורי המכיל רשימת שמות של תרופות חלופיות
        public List<string> Alternativies { get; set; }

       
        [DataMember]
        // מאפיין ציבורי המכיל רשימה של חלופות גנריות מסוג GenericAlternative
        public List<GenericAlternative> Generics { get; set; }

        [DataMember]
        public int MainSubstanceId { get; set; } // זה השדה החסר!


        [DataMember]
        // מאפיין ציבורי בוליאני המציין האם התרופה כלולה בסל הבריאות (True/False)
        public bool InHealthBox { get; set; }

       
        [DataMember]
        // מאפיין ציבורי בוליאני המציין האם התרופה זמינה ומאושרת לשיווק בישראל
        public bool AvailableInIsrael { get; set; }
    }
}