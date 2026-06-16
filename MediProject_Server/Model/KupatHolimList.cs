using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
namespace MediProject_Server.Model
{
    // הגדרת תכונה המציינת שאוסף זה מוגדר כחוזה נתונים עבור רשימות ויכול לעבור ברשת באמצעות WCF
    [CollectionDataContract]
    // הגדרת מחלקה ציבורית המייצגת רשימה של קופות חולים ויורשת מרשימה גנרית מסוג KupatHolim
    public class KupatHolimList : List<KupatHolim>
    {
        // בנאי ברירת מחדל ריק המאתחל רשימה חדשה של קופות חולים
        public KupatHolimList() { }
        // בנאי המקבל אוסף של קופות חולים ומאתחל את רשימת הבסיס איתו
        public KupatHolimList(IEnumerable<KupatHolim> list) : base(list) { }

        // בנאי המקבל אוסף של ישויות בסיס, ממיר את כולן לסוג KupatHolim ומאתחל איתן את הרשימה
        public KupatHolimList(IEnumerable<BaseEntity> list) : base(list.Cast<KupatHolim>().ToList()) { }


    }
}