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
    // הגדרת מחלקה ציבורית המייצגת חלופה גנרית לתרופה ויורשת ממחלקת הבסיס BaseEntity
    public class GenericAlternative : BaseEntity
    {
        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין פרטי המכיל אובייקט בודד מסוג תרופה (Medication) עם אפשרות קריאה וכתיבה
        private Medication medications { get; set; }

        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין פרטי המכיל רשימה של אובייקטים מסוג תרופה המייצגים את רשימת החלופות הגנריות
        private List<Medication> medicationList { get; set; }
    }
}