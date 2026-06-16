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
    // הגדרת מחלקת בסיס ציבורית שממנה ירשו כל הישויות והמודלים במערכת
    public class BaseEntity
    {
        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המייצג את המזהה הייחודי (ID) של הישות עם אפשרות קריאה וכתיבה
        public int ID { get; set; }
    }
}