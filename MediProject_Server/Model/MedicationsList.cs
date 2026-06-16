using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.Model
{
    // הגדרת תכונה המציינת שאוסף זה מוגדר כחוזה נתונים עבור רשימות ויכול לעבור ברשת באמצעות WCF
    [CollectionDataContract]
    // הגדרת מחלקה ציבורית המייצגת רשימה של תרופות ויורשת מרשימה גנרית מסוג Medication
    public class MedicationsList : List<Medication>
    {

        // בנאי ברירת מחדל ריק המאתחל רשימה חדשה של תרופות
        public MedicationsList() { }

        // בנאי המקבל אוסף של תרופות ומאתחל את רשימת הבסיס איתו
        public MedicationsList(IEnumerable<Medication> list) : base(list) { }


        // בנאי המקבל אוסף של ישויות בסיס, ממיר את כולן לסוג Medication ומאתחל איתן את הרשימה
        public MedicationsList(IEnumerable<BaseEntity> list) : base(list.Cast<Medication>().ToList()) { }

    }

}