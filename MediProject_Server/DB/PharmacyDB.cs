using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת בתי המרקחת ויורשת ממחלקת הבסיס BaseDB
    public class PharmacyDB : BaseDB
    {
        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון
        private static PharmacyDB instance;

        // בנאי פרטי כדי למנוע ממחלקות חיצוניות ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        private PharmacyDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה ואם הוא לא קיים אז מאתחלת אותו פעם אחת
        public static PharmacyDB GetInstance()
        {
            if (instance == null)
                instance = new PharmacyDB();
            return instance;
        }
        // פונקציה ציבורית השולפת את כל בתי המרקחת מהדאטה-בייס ומחזירה אותם כאוסף מסוג PharmacyList
        public PharmacyList SelectAll()
        {
            command.CommandText = "SELECT * FROM pharmacies";
            return new PharmacyList(base.Select());
        }

        // מימוש הפונקציה המופשטת של האב הלוקחת ישות כללית, ממירה אותה לבית מרקחת וממלאת אותה בנתונים מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Pharmacy pharmacy = entity as Pharmacy;
            // שליפת ערך עמודת ה-Id מה-Reader, המרתו למספר שלם והשמתו במאפיין ה-ID של האובייקט
            pharmacy.ID = int.Parse(reader["Id"].ToString());
            // שליפת ערך עמודת ה-Name מה-Reader והשמתו במאפיין שם בית המרקחת
            pharmacy.Name = reader["Name"].ToString();
            return pharmacy;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של אובייקט Pharmacy
        protected override BaseEntity NewEntity()
        {
            return new Pharmacy() as BaseEntity;
        }
        // פונקציה ציבורית המוחקת בית מרקחת מסוים מהדאטה-בייס על פי ה-ID שלו
        public void Delete(Pharmacy pharmacy)
        {
            command.CommandText = $"DELETE FROM pharmacies WHERE Id = {pharmacy.ID})";
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המעדכנת את נתוני בית המרקחת הקיים בדאטה-בייס
        public void Update(Pharmacy pharmacy)
        {
            command.CommandText = $"UPDATE pharmacies SET Name = '{pharmacy.Name}',  WHERE ID = {pharmacy.ID}";
            base.ExecuteNonQuery();
        }
        // פונקציה ציבורית המכניסה בית מרקחת חדש לתוך הטבלה בדאטה-בייס
        public void Insert(Pharmacy pharmacy)
        {
            command.CommandText = $"INSERT INTO pharmacies (Name) " +
                $"VALUES ('{pharmacy.Name}')";
            base.ExecuteNonQuery();
        }
    }
}