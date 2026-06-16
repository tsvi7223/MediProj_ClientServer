using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    // הגדרת מחלקה ציבורית שמנהלת את הגישה לטבלת החומרים הפעילים ויורשת ממחלקת הבסיס BaseDB
    public class SubstanceDB : BaseDB
    {
        // משתנה סטטי ופרטי שיחזיק את המופע היחיד של המחלקה הזו בזיכרון
        private static SubstanceDB instance;

        // בנאי פרטי כדי למנוע ממחלקות חיצוניות ליצור מופעים חדשים של ה-DB באמצעות האופרטור new
        private SubstanceDB() { }

        // פונקציה סטטית ציבורית המאפשרת לקבל את המופע היחיד של המחלקה ואם הוא לא קיים אז מאתחלת אותו פעם אחת
        public static SubstanceDB GetInstance()
        {
            if (instance == null)
                instance = new SubstanceDB();
            return instance;
        }
        // פונקציה ציבורית השולפת את כל החומרים הפעילים מהדאטה-בייס ומחזירה אותם כאוסף מסוג SubstanceList
        public SubstanceList SelectAll()
        {
            // הגדרת שאילתת SQL לבחירת כל השורות והעמודות מתוך טבלת החומרים הפעילים Substance
            command.CommandText = "SELECT * FROM Substance";
            return new SubstanceList(base.Select());
        }

        // מימוש הפונקציה המופשטת של האב הלוקחת ישות כללית, ממירה אותה לחומר פעיל וממלאת אותה בנתונים מה-Reader
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Substance substance = entity as Substance;
            // שליפת ערך עמודת ה-ID מה-Reader, המרתו למספר שלם והשמתו במאפיין ה-ID של האובייקט
            substance.ID = int.Parse(reader["ID"].ToString());
            // שליפת ערך עמודת ה-Description מה-Reader והשמתו במאפיין התיאור של החומר
            substance.Description = reader["Description"].ToString();
            // שליפת ערך עמודת ה-SubstanceName מה-Reader והשמתו במאפיין שם החומר הפעיל
            substance.SubstanceName = reader["SubstanceName"].ToString();


            return substance;
        }

        // מימוש הפונקציה המופשטת של האב האחראית לייצר ולהחזיר מופע חדש וריק של אובייקט Substance
        protected override BaseEntity NewEntity()
        {
            return new Substance() as BaseEntity;
        }
        // פונקציה ציבורית המוחקת חומר פעיל מסוים מהדאטה-בייס על פי ה-ID שלו
        public void Delete(Substance substance)
        {
            command.CommandText = $"DELETE FROM substance WHERE Id = {substance.ID})";
            base.ExecuteNonQuery();
        }



        // פונקציה ציבורית המעדכנת את נתוני החומר הפעיל הקיים בדאטה-בייס
        public void Update(Substance substance)
        {

            command.CommandText = $"UPDATE  Substance SET SubstanceName = '{substance.SubstanceName}', Description = '{substance.Description}'";
            base.ExecuteNonQuery();
        }
        // פונקציה ציבורית המכניסה חומר פעיל חדש לתוך הטבלה בדאטה-בייס
        public void Insert(Substance substance)
        {
            command.CommandText = $"INSERT INTO Substance (SubstanceName, Description) VALUES ({substance.SubstanceName}, {substance.Description})";
            base.ExecuteNonQuery();
        }

    }
}