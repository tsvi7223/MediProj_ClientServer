using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את טבלת הקישור בין משתמשים לתרופות שלהם ויורשת ממחלקת הבסיס BaseDB
    public class UserMedicationsDB : BaseDB
    {
        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון
        private static UserMedicationsDB instance;

        // בנאי פרטי כדי למנוע ממחלקות חיצוניות ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        private UserMedicationsDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה ואם הוא לא קיים אז מאתחלת אותו פעם אחת
        public static UserMedicationsDB GetInstance()
        {
            if (instance == null)
                instance = new UserMedicationsDB();
            return instance;
        }






        // פונקציה ציבורית המוחקת קישור ספציפי בין משתמש לתרופה מתוך טבלת הקישור על פי ה-ID של שניהם
        public void Delete(User user, Medication medication)
        {
            // הגדרת שאילתת SQL מסוג DELETE למחיקת השורה שבה גם קוד המשתמש וגם קוד התרופה תואמים לאובייקטים שהתקבלו
            command.CommandText = $"DELETE FROM UserMedications WHERE userId = {user.ID} AND MedicationId = {medication.ID}";
            base.ExecuteNonQuery();
        }


        // פונקציה ציבורית המכניסה שורת קישור חדשה המצמדת תרופה מסוימת למשתמש מסוים בטבלה
        public void Insert(User user, Medication medication)
        {
            // הגדרת שאילתת SQL מסוג INSERT INTO להוספת שורה חדשה המכילה את קוד המשתמש וקוד התרופה שלו
            command.CommandText = $"INSERT INTO UserMedications (UserId, MedicationId) " +
                $"VALUES ({user.ID}, {medication.ID})";
            base.ExecuteNonQuery();
        }

        // מימוש פונקציית האב לייצור ישות ריקה - כאן היא זורקת שגיאה מכיוון שטבלת קישור לא מייצרת ישות עצמאית משלה
        protected override BaseEntity NewEntity()
        {
            throw new NotImplementedException();
        }

        // מימוש פונקציית האב למילוי ישות מנתוני ה-Reader - כאן היא זורקת שגיאה כי הקישורים לרוב נשלפים דרך מחלקת המשתמש או התרופה
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            throw new NotImplementedException();
        }


    }
}