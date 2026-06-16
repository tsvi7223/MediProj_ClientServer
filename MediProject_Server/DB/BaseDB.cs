using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace MediProject_Server.DB
{
    //  [DataContract]

    // הגדרת מחלקה מופשטת (abstract) המשמשת כאב קדמון לכל מחלקות ה-DB במערכת
    public abstract class BaseDB
    {
        //  [DataMember]

        // משתנה פרטי וקבוע המכיל את נתיב הגישה וההגדרות לקובץ הדאטה-בייס
        private readonly string connectionString;
        //  [DataMember]

        // משתנה מוגן המנהל את הצינור והחיבור הפיזי מול מסד הנתונים של אקסס
        protected OleDbConnection connection;
        //   [DataMember]

        // משתנה מוגן האחראי על החזקת השאילתה (SQL) והרצתה מול מסד הנתונים
        protected OleDbCommand command;
        //  [DataMember]

        // משתנה מוגן המשמש כקורא נתונים המזרים את התוצאות שחזרו מהדאטה-בייס שורה אחר שורה
        protected OleDbDataReader reader;

        // פונקציה מופשטת שכל מחלקה בת חייבת לממש, כדי לייצר מופע חדש וריק של הישות שלה
        protected abstract BaseEntity NewEntity();
        // פונקציה מופשטת שכל מחלקה בת חייבת לממש, כדי לקחת שורה מהדאטה-בייס ולמלא איתה את מאפייני הישות
        protected abstract BaseEntity CreateModel(BaseEntity entity);

        // הבנאי (Constructor) של מחלקת הבסיס, שמאתחל את החיבור לדאטה-בייס
        public BaseDB()
        {

            // AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);


            // הגדרת מחרוזת החיבור עם נתיב יחסי המוביל לקובץ ה-Access של הפרויקט
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\..\MediProject_Server\DB\DbMedi.accdb;Persist Security Info=True";

            // יצירת אובייקט החיבור החדש עם מחרוזת החיבור שהוגדרה
            this.connection = new OleDbConnection(connectionString);
            // יצירת אובייקט הפקודה שישמש להרצת השאילתות
            command = new OleDbCommand();
            // קישור אובייקט הפקודה אל אובייקט החיבור הפיזי שיצרנו
            command.Connection = this.connection;

        }

        // פונקציה מוגנת השולפת רשימה של ישויות מהדאטה-בייס על בסיס השאילתה שהוגדרה ב-command
        protected List<BaseEntity> Select()
        {
            // יצירת רשימה חדשה וריקה שבה נרכז את כל הישויות שנשלוף
            List<BaseEntity> list = new List<BaseEntity>();
            // תחילת בלוק ניסיון לטיפול בשגיאות בזמן ריצה
            try
            {
                // בדיקה: אם החיבור לדאטה-בייס סגור, נפתח אותו כעת
                if (connection.State != ConnectionState.Open)
                    // פתיחת החיבור הפיזי לקובץ האקסס
                    connection.Open();

                // וידוא שאובייקט הפקודה משתמש בחיבור הפתוח הנוכחי
                command.Connection = connection;
                // הרצת השאילתה ויצירת קורא הנתונים שיחזיק את שורות התוצאה
                reader = command.ExecuteReader();
                // לולאה הרצה כל עוד ישנן שורות מידע לקרוא מתוך תוצאות השאילתה
                while (reader.Read())
                {
                    // קריאה לפונקציה המופשטת כדי לקבל אובייקט ישות חדש וריק
                    BaseEntity entity = NewEntity();
                    // מילוי האובייקט בנתוני השורה הנוכחית והוספתו לרשימה הכללית
                    list.Add(CreateModel(entity));
                }
                //Console.WriteLine(reader["KupaName"].ToString());
            }
            // תפיסת שגיאה במקרה שמשהו נכשל במהלך השליפה או החיבור
            catch (Exception ex)
            {
                // הדפסת הודעת השגיאה ונוסח שאילתת ה-SQL לחלון ה-Output של ה-Debug לצורך ניפוי באגים
                System.Diagnostics.Debug.WriteLine(
                    ex.Message + "\nSQL: " + command.CommandText);
            }
            // בלוק שרץ תמיד ובכל מקרה בסיום הפונקציה (גם אם הייתה שגיאה וגם אם לא) לשחרור משאבים
            finally
            {
                // בדיקה: אם קורא הנתונים קיים ועדיין פתוח, נסגור אותו
                if (reader != null && !reader.IsClosed) { reader.Close(); }
                // בדיקה: אם החיבור לדאטה-בייס נשאר פתוח, נסגור אותו כדי לא לבזבז משאבים
                if (connection.State == ConnectionState.Open)
                    // סגירת החיבור הפיזי לקובץ האקסס
                    connection.Close();
            }
            // החזרת רשימת הישויות המלאה שנשלפה
            return list;
        }


        // פונקציה מוגנת המריצה שאילתות שלא מחזירות שורות נתונים (כמו Insert, Update, Delete)
        protected void ExecuteNonQuery()
        {
            // תחילת בלוק ניסיון לטיפול בשגיאות בזמן ריצה
            try
            {
                // בדיקה: אם החיבור לדאטה-בייס סגור, נפתח אותו כעת
                if (connection.State != ConnectionState.Open)
                    // פתיחת החיבור הפיזי לקובץ האקסס
                    connection.Open();

                // וידוא שאובייקט הפקודה משתמש בחיבור הפתוח הנוכחי
                command.Connection = connection;
                // הרצת פקודת ה-SQL ושמירת מספר השורות שהושפעו או השתנו בטבלה
                int recordsAffected = command.ExecuteNonQuery();
            }
            // תפיסת שגיאה במקרה שמשהו נכשל במהלך הרצת הפקודה מול הדאטה-בייס
            catch (Exception ex)
            {
                // הדפסת הודעת השגיאה ונוסח פקודת ה-SQL לחלון ה-Output של ה-Debug לצורך ניפוי באגים
                System.Diagnostics.Debug.WriteLine(
                    ex.Message + "\nSQL: " + command.CommandText);
            }
            // בלוק שרץ תמיד ובכל מקרה בסיום הפונקציה כדי להבטיח את סגירת החיבור
            finally
            {
                // בדיקה: אם החיבור לדאטה-בייס נשאר פתוח, נסגור אותו מיד
                if (connection.State == ConnectionState.Open)
                    // סגירת החיבור הפיזי לקובץ האקסס
                    connection.Close();
            }
        }

        // פונקציה ציבורית המאפשרת למחוק לחלוטין את כל התוכן והשורות מטבלה מסוימת על פי שמה
        public void ClearTable(string tableName)
        {
            // השמת שאילתת SQL מסוג DELETE המשולבת עם שם הטבלה שהתקבל כפרמטר
            command.CommandText = $"DELETE FROM {tableName}";
            // קריאה לפונקציית העזר כדי להוציא אל הפועל את מחיקת השורות מהטבלה
            ExecuteNonQuery();
        }
    }
}