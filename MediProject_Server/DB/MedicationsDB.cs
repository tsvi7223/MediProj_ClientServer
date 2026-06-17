using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.DB
{
    public class MedicationsDB:BaseDB
    {
      
            private static MedicationsDB instance;

            private MedicationsDB() { }

            public static MedicationsDB GetInstance()
            {
                if (instance == null)
                    instance = new MedicationsDB();
                return instance;
            }
            public MedicationsList SelectAll()
            {
                command.CommandText = "SELECT * FROM Medications";
                return new MedicationsList(base.Select());
            }

            protected override BaseEntity CreateModel(BaseEntity entity)
            {
                Medication medication = entity as Medication;
                medication.ID = int.Parse(reader["ID"].ToString());
                medication.OriginalName = reader["OriginalName"].ToString();
                int SubstanceID = int.Parse(reader["MainSubstanceId"].ToString());
                medication.ActiveSubstance = SubstanceDB.GetInstance().SelectAll().Find(sub => sub.ID == SubstanceID);
            medication.InHealthBox = (bool)reader["InHealthBox"];
                medication.AvailableInIsrael = (bool)reader["AvailableInIsrael"];
           


                return medication;
            }

            protected override BaseEntity NewEntity()
            {
                return new Medication() as BaseEntity;
            }
            public void Delete(Medication medication)
            {
                command.CommandText = $"DELETE FROM Medications WHERE Id = {medication.ID}";
                base.ExecuteNonQuery();
            }

            public void Update(Medication medication)
            {
            // command.CommandText = $"UPDATE people SET FirstName = '{person.FirstName}', LastName = '{person.LastName}', WHERE ID = {person.Id}   ";
            command.CommandText = $"UPDATE Medications SET OriginalName = '{medication.OriginalName}', InHealthBox = {medication.InHealthBox}, AvailableInIsrael = {medication.AvailableInIsrael}, MainSubstanceId = {medication.ActiveSubstance.ID} " +
                $"WHERE (Medications.ID = {medication.ID})";
                base.ExecuteNonQuery();
            }
            public void Insert(Medication medication)
            {
            command.CommandText = $"INSERT INTO Medications " +
                $"(OriginalName, InHealthBox, AvailableInIsrael, MainSubstanceId)" +
                $" VALUES ('{medication.OriginalName}', {medication.InHealthBox},{medication.AvailableInIsrael}, {medication.ActiveSubstance.ID})";
                base.ExecuteNonQuery();
        }

        // פונקציה ציבורית השולפת ומחזירה את כל התרופות המשויכות למשתמש מסוים באמצעות קשר רבות-לרבות
        public MedicationsList GetByUser(User user)
        {
            // הגדרת שאילתת SQL המבצעת INNER JOIN כפול בין טבלת משתמשים, טבלת הקישור וטבלת התרופות לפי ה-ID של המשתמש
            command.CommandText = "SELECT Medications.* " +
                          "FROM (Users " +
                          "INNER JOIN UserMedications ON Users.ID = UserMedications.UserID) " +
                          "INNER JOIN Medications ON UserMedications.MedicationID = Medications.ID " +
                          $"WHERE Users.ID = {user.ID};";

            return new MedicationsList(base.Select());
        }
        public void DeleteFromUserMedications(int userId, int medicationId)
        {
            // הפקודה הזאת מוחקת רק את השורה שמקשרת בין המשתמש הספציפי לתרופה הספציפית
            command.CommandText = "DELETE FROM UserMedications WHERE UserID = " + userId + " AND MedicationID = " + medicationId;
            base.ExecuteNonQuery();
        }



    }
}
