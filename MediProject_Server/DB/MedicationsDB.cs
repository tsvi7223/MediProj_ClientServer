using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת התרופות ויורשת ממחלקת הבסיס BaseDB
    public class MedicationsDB : BaseDB
    {

        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון (כחלק מתבנית Singleton)
        private static MedicationsDB instance;

        // בנאי פרטי כדי למנוע ממחלקות חיצוניות ליצר מופעים חדשים של ה-DB באמצעות האופרטור new
        private MedicationsDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה, ואם הוא לא קיים - מאתחלת אותו פעם אחת
        public static MedicationsDB GetInstance()
        {
            // בדיקה: אם עדיין לא נוצר מופע של המחלקה בזיכרון
            if (instance == null)
                // יצירת המופע היחיד והשמתו במשתנה הסטטי
                instance = new MedicationsDB();
            // החזרת המופע הקיים למי שקרא לפונקציה
            return instance;
        }
        // פונקציה ציבורית השולפת את כל התרופות מהדאטה-בייס ומחזירה אותן כאוסף מסוג MedicationsList
        public MedicationsList SelectAll()
        {
            // הגדרת שאילתת SQL לבחירת כל השורות והעמודות מתוך טבלת התרופות Medications
            command.CommandText = "SELECT * FROM Medications";
            // קריאה לפונקציית ה-Select של מחלקת האב, והמרת הרשימה שחזרה לאובייקט הרשימה הייעודי
            return new MedicationsList(base.Select());
        }

        // מימוש הפונקציה המופשטת של האב הלוקחת ישות כללית, ממירה אותה לתרופה וממלאת אותה בנתונים מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            // המרת אובייקט הבסיס הכללי (BaseEntity) לסוג הספציפי Medication
            Medication medication = entity as Medication;
            // שליפת ערך עמודת ה-ID מה-Reader, המרתו למספר שלם והשמתו במאפיין ה-ID של האובייקט
            medication.ID = int.Parse(reader["ID"].ToString());
            // שליפת ערך עמודת ה-OriginalName מה-Reader, המרתו לטקסט והשמתו במאפיין השם של האובייקט
            medication.OriginalName = reader["OriginalName"].ToString();
            // שליפת קוד החומר הפעיל (MainSubstanceId) מה-Reader והמרתו למספר שלם
            int SubstanceID = int.Parse(reader["MainSubstanceId"].ToString());
            // פנייה למחלקת החומרים הפעילים, שליפת כולם ומציאת האובייקט שה-ID שלו תואם לקוד שנשלף
            medication.ActiveSubstance = SubstanceDB.GetInstance().SelectAll().Find(sub => sub.ID == SubstanceID);
            // שליפת ערך עמודת ה-InHealthBox (האם בסל הבריאות) מה-Reader והמרתו לערך בוליאני (True/False)
            medication.InHealthBox = (bool)reader["InHealthBox"];
            // שליפת ערך עמודת ה-AvailableInIsrael (האם זמין בישראל) מה-Reader והמרתו לערך בוליאני
            medication.AvailableInIsrael = (bool)reader["AvailableInIsrael"];



            // החזרת אובייקט התרופה המלא כשהוא מומר בחזרה לטיפוס הבסיס שלו
            return medication;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של האובייקט הספציפי
        protected override BaseEntity NewEntity()
        {
            // יצירת אובייקט חדש של Medication והחזרתו כשהוא מומר לטיפוס הבסיס BaseEntity
            return new Medication() as BaseEntity;
        }
        // פונקציה ציבורית המוחקת תרופה מסוימת מהדאטה-בייס על פי ה-ID שלה
        public void Delete(Medication medication)
        {
            // הגדרת שאילתת SQL מסוג DELETE למחיקת השורה שבה ה-ID שווה ל-ID של האובייקט שהתקבל
            command.CommandText = $"DELETE FROM Medications WHERE Id = {medication.ID})";
            // קריאה לפונקציית העזר של מחלקת האב שמבצעת את הרצת הפקודה מול האקסס
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המעדכנת את נתוני התרופה הקיימת בדאטה-בייס
        public void Update(Medication medication)
        {
            // command.CommandText = $"UPDATE people SET FirstName = '{person.FirstName}', LastName = '{person.LastName}', WHERE ID = {person.Id}   ";
            // הגדרת שאילתת SQL מסוג UPDATE המעדכנת את השדות השונים של התרופה בשורה המתאימה לפי ה-ID שלה
            command.CommandText = $"UPDATE Medications SET OriginalName = '{medication.OriginalName}', InHealthBox = {medication.InHealthBox}, AvailableInIsrael = {medication.AvailableInIsrael}, MainSubstanceId = {medication.ActiveSubstance.ID} " +
                $"WHERE (Medications.ID = {medication.ID})";
            // קריאה לפונקציית העזר של מחלקת האב שמבצעת את הרצת הפקודה מול האקסס
            base.ExecuteNonQuery();
        }
        // פונקציה ציבורית המכניסה תרופה חדשה לתוך הטבלה בדאטה-בייס
        public void Insert(Medication medication)
        {
            // הגדרת שאילתת SQL מסוג INSERT INTO להוספת שורה חדשה עם שם התרופה, הסטטוסים וקוד החומר הפעיל שלה
            command.CommandText = $"INSERT INTO Medications " +
                $"(OriginalName, InHealthBox, AvailableInIsrael, MainSubstanceId)" +
                $" VALUES ('{medication.OriginalName}', {medication.InHealthBox},{medication.AvailableInIsrael}, {medication.ActiveSubstance.ID})";
            // קריאה לפונקציית העזר של מחלקת האב שמבצעת את הרצת הפקודה מול האקסס
            base.ExecuteNonQuery();
        }





    }
}