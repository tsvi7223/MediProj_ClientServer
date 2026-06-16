using MediProject_Server.Model;
using System;
namespace MediProject_Server.DB
{
    // הוסר ה-DataContract - המחלקה הזו לא עוברת ברשת!
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת האנשים ויורשת ממחלקת הבסיס BaseDB
    public class PeopleDB : BaseDB
    {
        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון
        private static PeopleDB instance;

        // בנאי מוגן כדי למנוע ממחלקות חיצוניות ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        protected PeopleDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה ואם הוא לא קיים אז מאתחלת אותו פעם אחת
        public static PeopleDB GetInstance()
        {
            if (instance == null)
                instance = new PeopleDB();
            return instance;
        }

        // פונקציה ציבורית השולפת את כל האנשים מהדאטה-בייס ומחזירה אותם כאוסף מסוג PeopleList
        public PeopleList SelectAll()
        {
            command.CommandText = "SELECT * FROM People";
            return new PeopleList(base.Select());
        }

        // מימוש הפונקציה המופשטת של האב הלוקחת ישות כללית וממלאת אותה בנתונים מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Person person = entity as Person;
            if (person == null) person = new Person();

            // שליפת ערך עמודת ה-ID מה-Reader, המרתו למספר שלם והשמתו במאפיין ה-ID של האובייקט
            person.ID = int.Parse(reader["people.ID"].ToString());
            // שליפת ערך עמודת ה-FirstName מה-Reader והשמתו במאפיין השם הפרטי
            person.FirstName = reader["FirstName"].ToString();
            // שליפת ערך עמודת ה-LastName מה-Reader והשמתו במאפיין שם המשפחה
            person.LastName = reader["LastName"].ToString();
            // שליפת ערך עמודת ה-DateOfBirth מה-Reader והמרתו לטיפוס תאריך
            person.DateOfBirth = (DateTime)reader["DateOfBirth"];
            // שליפת ערך עמודת ה-Gmail מה-Reader והשמתו במאפיין המייל
            person.Gmail = reader["Gmail"].ToString();
            // שליפת ערך עמודת ה-FullAddress מה-Reader והשמתו במאפיין הכתובת
            person.FullAddress = reader["FullAddress"].ToString();
            // שליפת ערך עמודת ה-PhoneNumber מה-Reader והשמתו במאפיין מספר הטלפון
            person.PhoneNumber = reader["PhoneNumber"].ToString();
            return person;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של אובייקט Person
        protected override BaseEntity NewEntity()
        {
            return new Person();
        }

        // פונקציה ציבורית המוחקת אדם מסוים מהדאטה-בייס על פי ה-ID שלו
        public void Delete(Person person)
        {
            // תוקן: הוסר הסוגר המיותר בסוף
            command.CommandText = $"DELETE FROM people WHERE ID = {person.ID}";
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המעדכנת את נתוני האדם הקיים בדאטה-בייס
        public void Update(Person person)
        {
            // TODO: fix
            command.CommandText = $"UPDATE people SET FirstName = '{person.FirstName}', LastName = '{person.LastName}', DateOfBirth = #{person.DateOfBirth}#, Gmail = '{person.Gmail}'," +
                $"FullAddress='{person.FullAddress}', PhoneNumber='{person.PhoneNumber}' WHERE ID = {person.ID}";
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המכניסה אדם חדש לטבלה ומחזירה את ה-ID האוטומטי החדש שנוצר לו באקסס
        public int Insert(Person person)
        {
            int newId = 0;
            try
            {
                // בדיקה: אם החיבור לדאטה-בייס סגור אז נפתח אותו כעת
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                command.Connection = connection;
                // הגדרת שאילתת SQL מסוג INSERT INTO להוספת שורה חדשה עם כל נתוני האדם שהתקבלו
                command.CommandText = $"INSERT INTO people (FirstName, LastName, DateOfBirth, Gmail, FullAddress, PhoneNumber) VALUES ('{person.FirstName}', '{person.LastName}', #{person.DateOfBirth}#, '{person.Gmail}', '{person.FullAddress}','{person.PhoneNumber}')";
                // הרצת פקודת ההכנסה ישירות מול החיבור כדי להשאיר את הצינור פתוח לשלב הבא
                command.ExecuteNonQuery(); // שימוש ישיר ולא דרך base כדי לא לסגור את החיבור
                // השמת שאילתה מיוחדת של אקסס השולפת את ה-ID האחרון שנוצר באופן אוטומטי
                command.CommandText = "SELECT @@IDENTITY";
                // הרצת השאילתה לשליפת ערך בודד ושמירתו בתוך משתנה כללי מסוג object
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    // המרת ה-ID שחזר מ-object למספר שלם והשמתו במשתנה newId
                    newId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // כאן כדאי להוסיף טיפול בשגיאות או זריקה של השגיאה למעלה
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                // 4. סגירת החיבור רק בסוף התהליך כולו
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return newId;

        }
    }
}