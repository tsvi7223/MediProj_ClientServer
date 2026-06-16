using System;
using System.Runtime.Serialization;
namespace MediProject_Server.Model
{
    // הגדרת תכונה המציינת שהמחלקה הזו ניתנת לסריאליזציה ויכולה לעבור ברשת באמצעות WCF
    [DataContract]
    // תכונה המצהירה ל-WCF על קיומה של מחלקה יורשת מסוג User כדי שהשרת ידע לזהות אותה בזמן העברת הנתונים
    [KnownType(typeof(User))]
    public class Person : BaseEntity
    {
        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את השם הפרטי של האדם עם אפשרות קריאה וכתיבה
        public string FirstName { get; set; }

        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את שם המשפחה של האדם עם אפשרות קריאה וכתיבה
        public string LastName { get; set; }

        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את מספר הטלפון של האדם עם אפשרות קריאה וכתיבה
        public string PhoneNumber { get; set; }

        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את תאריך הלידה של האדם, כשהברירת מחדל היא הזמן הנוכחי
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את כתובת הג'ימייל של האדם עם אפשרות קריאה וכתיבה
        public string Gmail { get; set; }

        // הגדרת תכונה המציינת שהמשתנה הבא הוא חלק מחוזה הנתונים ויעבור ברשת
        [DataMember]
        // מאפיין ציבורי המכיל את הכתובת המלאה של האדם עם אפשרות קריאה וכתיבה
        public string FullAddress { get; set; }

        // בנאי המקבל פרמטרים ומאתחל איתם את כל המאפיינים של האדם כולל ה-ID שורש ממחלקת הבסיס
        public Person(int id, string FirstName, string LastName, string phoneNumber, DateTime dateOfBirth, string gmail, string fullAddress)
        {
            this.ID = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = phoneNumber;
            this.DateOfBirth = dateOfBirth;
            this.Gmail = gmail;
            this.FullAddress = fullAddress;
        }

        // בנאי ברירת מחדל ריק המאפשר יצירת אובייקט אדם ללא העברת נתונים ראשונית
        public Person() { }

        // אין צורך ב-[DataMember] כי זה שדה מחושב לקריאה בלבד
        // מאפיין לקריאה בלבד (get) המחזיר מחרוזת המשלבת את השם הפרטי ושם המשפחה יחד
        public string FulLastName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}