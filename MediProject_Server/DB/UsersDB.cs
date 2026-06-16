using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת המשתמשים ויורשת ממחלקת האנשים PeopleDB
    public class UsersDB : PeopleDB
    {

        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון
        private static UsersDB instance;

        // בנאי פרטי כדי למנוע ממחלקות חיצוניות ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        private UsersDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה ואם הוא לא קיים אז מאתחלת אותו פעם אחת
        public static UsersDB GetInstance()
        {
            if (instance == null)
                instance = new UsersDB();
            return instance;
        }
        // פונקציה ציבורית השולפת את כל המשתמשים ומחזירה אותם כאוסף מסוג UserList כולל רשימת התרופות שלהם
        public UserList SelectAll()
        {
            // הגדרת שאילתת SQL המבצעת INNER JOIN בין טבלת אנשים לטבלת משתמשים על בסיס ה-ID המשותף
            command.CommandText = "SELECT * FROM  (people INNER JOIN  Users ON people.ID = Users.ID)";
            UserList list = new UserList(base.Select());

            // לולאה העוברת על כל משתמש שנשלף וממלאת עבורו את רשימת התרופות האישיות שלו מהדאטה-בייס
            foreach (User user in list)
            {
                user.Medications = GetUserMedications(user);
            }
            return list;
        }

        // מימוש הפונקציה המופשטת הלוקחת ישות, ממירה אותה למשתמש וממלאת אותה בנתוני האדם והמשתמש מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = entity as User;
            //user.ID = int.Parse(reader["Users.ID"].ToString());
            // קריאה לפונקציית CreateModel של מחלקת האב PeopleDB כדי למלא קודם כל את נתוני האדם הבסיסיים
            base.CreateModel(user);
            // שליפת ערך עמודת ה-UserName מה-Reader והשמתו במאפיין שם המשתמש של האובייקט
            user.UserName = reader["UserName"].ToString();
            // שליפת ערך עמודת ה-Password מה-Reader והשמתו במאפיין הסיסמה של האובייקט
            user.Password = reader["Password"].ToString();
            // שליפת קוד קופת החולים (KupaId) מה-Reader והמרתו למספר שלם
            int kupaId = int.Parse(reader["KupaId"].ToString());
            // פנייה למחלקת קופות החולים, שליפת כולן ומציאת האובייקט שה-ID שלו תואם לקוד שנשלף
            user.Kupa = KupatHolimDB.GetInstance().SelectAll().Find(kupa => kupa.ID == kupaId);
            //user.Medications = GetUserMedications(user);

            //user.Gmail = reader["Gmail"].ToString();


            return user;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של אובייקט User
        protected override BaseEntity NewEntity()
        {
            return new User() as BaseEntity;
        }
        // פונקציה ציבורית המוחקת משתמש מהמערכת - גם מטבלת האנשים וגם מטבלת המשתמשים
        public void Delete(User user)
        {
            // קריאה לפונקציית המחיקה של מחלקת האב כדי למחוק את הרשומה מטבלת people
            base.Delete(user);
            // הגדרת שאילתת SQL מסוג DELETE למחיקת השורה המתאימה מטבלת המשתמשים users לפי ה-ID
            command.CommandText = $"DELETE FROM users WHERE Id = {user.ID}";
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המעדכנת את נתוני המשתמש בשתי הטבלאות (people ו-users) בדאטה-בייס
        public void Update(User user)
        {
            // קריאה לפונקציית העדכון של מחלקת האב כדי לעדכן את השדות השייכים לטבלת people
            base.Update(user);
            // command.CommandText = $"UPDATE people SET FirstName = '{user.FirstName}', LastName = '{user.LastName}', WHERE ID = {user.Id}   ";
            // הגדרת שאילתת SQL מסוג UPDATE לעדכון שדות שם המשתמש, הסיסמה וקוד הקופה בטבלת users לפי ה-ID
            command.CommandText = $"UPDATE users SET userName = '{user.UserName}', [Password] = '{user.Password}', KupaId = {user.Kupa.ID} WHERE(users.ID ={user.ID})";
            base.ExecuteNonQuery();
        }
        // פונקציה ציבורית המכניסה משתמש חדש - קודם כל לטבלת people, אז לטבלת users ואז את תרופותיו לטבלת הקישור
        public void Insert(User user)
        {
            // הכנסת נתוני האדם לטבלת people וקבלת ה-ID האוטומטי החדש שנוצר באקסס
            int userId = base.Insert(user);
            // הגדרת שאילתת SQL מסוג INSERT INTO להוספת המשתמש לטבלת users תוך שימוש ב-ID שנוצר הרגע
            command.CommandText = $"INSERT INTO users (id, userName, [Password], KupaId) VALUES  ({userId}, '{user.UserName}', '{user.Password}', {user.Kupa.ID})";
            base.ExecuteNonQuery();

            // בדיקה: אם קיימות תרופות ברשימת התרופות של המשתמש שהתקבל
            if (user.Medications != null && user.Medications.Count > 0)
                // לולאה הרצה על כל תרופה ברשימה של המשתמש
                foreach (Medication med in user.Medications)
                {
                    // שאילתת הכנסה לטבלה המקשרת
                    // הגדרת שאילתת SQL להוספת שורת קישור בין קוד המשתמש לקוד התרופה הנוכחית בלולאה
                    command.CommandText = $"INSERT INTO UserMedications (UserID, MedicationID)  VALUES ({user.ID}, {med.ID})";
                    base.ExecuteNonQuery();
                }

        }

        // פונקציה ציבורית השולפת ומחזירה את כל התרופות המשויכות למשתמש מסוים באמצעות קשר רבות-לרבות
        public MedicationsList GetUserMedications(User user)
        {
            // הגדרת שאילתת SQL המבצעת INNER JOIN כפול בין טבלת משתמשים, טבלת הקישור וטבלת התרופות לפי ה-ID של המשתמש
            command.CommandText = "SELECT Medications.* " +
                          "FROM (Users " +
                          "INNER JOIN UserMedications ON Users.ID = UserMedications.UserID) " +
                          "INNER JOIN Medications ON UserMedications.MedicationID = Medications.ID " +
                          $"WHERE Users.ID = {user.ID};";
            //command.CommandText = $"SELECT Medications.* " +
            //    $"FROM " +
            //        $"((Users INNER JOIN UserMedications ON Users.ID = UserMedications.UserID) " +
            //                 $"INNER JOIN  Medications " +
            //    $"ON UserMedications.MedicationID = Medications.ID) " +
            //    $"WHERE (Users.ID = {user.ID})";
            return new MedicationsList(base.Select());
        }



    }
}