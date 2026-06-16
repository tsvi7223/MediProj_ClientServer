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
    // הגדרת מחלקה ציבורית המייצגת רשימה של אנשים ויורשת מרשימה גנרית מסוג Person
    public class PeopleList : List<Person>
    {
        // בנאי ברירת מחדל ריק המאתחל רשימה חדשה של אנשים
        public PeopleList() { }

        // בנאי המקבל אוסף של אנשים ומאתחל את רשימת הבסיס איתו
        public PeopleList(IEnumerable<Person> list) : base(list) { }


        // בנאי המקבל אוסף של ישויות בסיס, ממיר את כולן לסוג Person ומאתחל איתן את הרשימה
        public PeopleList(IEnumerable<BaseEntity> list) : base(list.Cast<Person>().ToList()) { }

    }
}