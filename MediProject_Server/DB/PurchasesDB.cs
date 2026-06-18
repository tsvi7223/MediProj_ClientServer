using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת בתי המרקחת ויורשת ממחלקת הבסיס BaseDB
    public class PurchasesDB : BaseDB
    {
        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון
        private static PurchasesDB instance;

        // בנאי פרטי כדי למנוע ממחלקות חיצוניות ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        private PurchasesDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה ואם הוא לא קיים אז מאתחלת אותו פעם אחת
        public static PurchasesDB GetInstance()
        {
            if (instance == null)
                instance = new PurchasesDB();
            return instance;
        }
        // פונקציה ציבורית השולפת את כל בתי המרקחת מהדאטה-בייס ומחזירה אותם כאוסף מסוג PurchaseList
        public PurchaseList SelectAll()
        {
            command.CommandText = "SELECT * FROM Purchases";
            return new PurchaseList(base.Select());
        }

        // מימוש הפונקציה המופשטת של האב הלוקחת ישות כללית, ממירה אותה לבית מרקחת וממלאת אותה בנתונים מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Purchase purchase = entity as Purchase;
            // שליפת ערך עמודת ה-Id מה-Reader, המרתו למספר שלם והשמתו במאפיין ה-ID של האובייקט
            purchase.ID = int.Parse(reader["Id"].ToString());
            // שליפת ערך עמודת ה-Name מה-Reader והשמתו במאפיין שם בית המרקחת
            
            int userId = int.Parse(reader["UserID"].ToString());
            purchase.user = UsersDB.GetInstance().SelectAll().Find(u=>u.ID == userId);
            purchase.PillsPerDay = int.Parse(reader["PillsPerDay"].ToString());
            purchase.PillAmount = int.Parse(reader["PillAmount"].ToString());
            int mediId = int.Parse(reader["MedicationId"].ToString());
            purchase.medication = MedicationsDB.GetInstance().SelectAll().Find(medi => medi.ID == mediId);
            purchase.PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString());
            return purchase;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של אובייקט Purchase
        protected override BaseEntity NewEntity()
        {
            return new Purchase() as BaseEntity;
        }
        // פונקציה ציבורית המוחקת בית מרקחת מסוים מהדאטה-בייס על פי ה-ID שלו
        public void Delete(Purchase purchase)
        {
            command.CommandText = $"DELETE FROM Purchases WHERE Id = {purchase.ID})";
            base.ExecuteNonQuery();
        }

        // פונקציה ציבורית המעדכנת את נתוני בית המרקחת הקיים בדאטה-בייס
        public void Update(Purchase purchase)
        {
            command.CommandText = $"UPDATE Purchases SET PurchaseDate =#{purchase.PurchaseDate}#, UserID ={purchase.user.ID}," +
                $"PillsPerDay ={purchase.PillsPerDay}, PillAmount ={purchase.PillAmount}, MedicationId ={purchase.medication.ID} WHERE(Purchases.ID = 1)";

            base.ExecuteNonQuery();
        }
        // פונקציה ציבורית המכניסה בית מרקחת חדש לתוך הטבלה בדאטה-בייס
        public void Insert(Purchase purchase)
        {
            command.CommandText = $"INSERT INTO Purchases " +
                $"(PurchaseDate, UserID, PillsPerDay, PillAmount, MedicationId) " +
                $"VALUES(#{purchase.PurchaseDate}#, {purchase.user.ID}, {purchase.PillsPerDay}, {purchase.PillAmount}, {purchase.medication.ID})";
            base.ExecuteNonQuery();
        }
    }
}