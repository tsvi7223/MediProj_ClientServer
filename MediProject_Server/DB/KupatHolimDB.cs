using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת קופות החולים ויורשת ממחלקת הבסיס BaseDB
    public class KupatHolimDB : BaseDB
    {
        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון (כחלק מתבנית Singleton)
        private static KupatHolimDB instance;

        // בנאי פרטי כדי למנוע מכל מחלקה חיצונית ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        private KupatHolimDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה, ואם הוא לא קיים - מאתחלת אותו פעם אחת
        public static KupatHolimDB GetInstance()
        {
            // בדיקה: אם עדיין לא נוצר מופע של המחלקה בזיכרון
            if (instance == null)
                // יצירת המופע היחיד והשמתו במשתנה הסטטי
                instance = new KupatHolimDB();
            // החזרת המופע הקיים למי שקרא לפונקציה
            return instance;
        }
        // פונקציה ציבורית השולפת את כל קופות החולים מהדאטה-בייס ומחזירה אותן כאוסף מסוג KupatHolimList
        public KupatHolimList SelectAll()
        {
            // הגדרת שאילתת SQL לבחירת כל השורות והעמודות מתוך טבלת קופות החולים
            command.CommandText = "SELECT * FROM KupatHolim";
            // קריאה לפונקציית ה-Select של מחלקת האב, והמרת הרשימה שחזרה לאובייקט הרשימה הייעודי
            return new KupatHolimList(base.Select());
        }

        // מימוש הפונקציה המופשטת של האב הלוקחת ישות כללית, ממירה אותה לקופת חולים וממלאת אותה בנתונים מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            // המרת אובייקט הבסיס הכללי (BaseEntity) לסוג הספציפי KupatHolim
            KupatHolim kupatHolim = entity as KupatHolim;
            // שליפת ערך עמודת ה-ID מה-Reader, המרתו למספר שלם והשמתו במאפיין ה-ID של האובייקט
            kupatHolim.ID = int.Parse(reader["ID"].ToString());
            // שליפת ערך עמודת ה-KupaName מה-Reader, המרתו לטקסט והשמתו במאפיין ה-Name של האובייקט
            kupatHolim.Name = reader["KupaName"].ToString();
            // החזרת האובייקט המלא כשהוא מומר בחזרה לטיפוס הבסיס שלו
            return kupatHolim;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של האובייקט הספציפי
        protected override BaseEntity NewEntity()
        {
            // יצירת אובייקט חדש של KupatHolim והחזרתו כשהוא מומר לטיפוס הבסיס BaseEntity
            return new KupatHolim() as BaseEntity;
        }
        // פונקציה ציבורית המוחקת קופת חולים מסוימת מהדאטה-בייס על פי ה-ID שלה
        public void Delete(KupatHolim kupatHolim)
        {
            // הגדרת שאילתת SQL מסוג DELETE למחיקת השורה שבה ה-ID שווה ל-ID של האובייקט שהתקבל
            command.CommandText = $"DELETE FROM kupatHolim WHERE Id = {kupatHolim.ID})";
            // קריאה לפונקציית העזר של מחלקת האב שמבצעת את הרצת הפקודה מול האקסס
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המעדכנת את נתוני קופת החולים הקיימת בדאטה-בייס
        public void Update(KupatHolim kupatHolim)
        {
            // הגדרת שאילתת SQL מסוג UPDATE המעדכנת את שם הקופה בשורה המתאימה לפי ה-ID שלה
            command.CommandText = $"UPDATE kupatHolim SET KupaName = '{kupatHolim.Name}',  WHERE ID = {kupatHolim.ID}";
            // קריאה לפונקציית העזר של מחלקת האב שמבצעת את הרצת הפקודה מול האקסס
            base.ExecuteNonQuery();
        }
        // פונקציה ציבורית המכניסה קופת חולים חדשה לתוך הטבלה בדאטה-בייס
        public void Insert(KupatHolim kupatHolim)
        {
            // הגדרת שאילתת SQL מסוג INSERT INTO להוספת שורה חדשה עם שם קופת החולים שהתקבל
            command.CommandText = $"INSERT INTO kupatholim (KupaName) " +
                $"VALUES ('{kupatHolim.Name}')";
            // קריאה לפונקציית העזר של מחלקת האב שמבצעת את הרצת הפקודה מול האקסס
            base.ExecuteNonQuery();
        }


    }
}