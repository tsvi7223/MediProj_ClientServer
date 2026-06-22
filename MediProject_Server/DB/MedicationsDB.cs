using MediProject_Server.Model;
using System;
using System.Collections.Generic;

namespace MediProject_Server.DB
{
    public class MedicationsDB : BaseDB
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

            
            int substanceId = int.Parse(reader["MainSubstanceId"].ToString());
            medication.MainSubstanceId = substanceId;

            medication.InHealthBox = bool.Parse(reader["InHealthBox"].ToString());
            medication.AvailableInIsrael = bool.Parse(reader["AvailableInIsrael"].ToString());

            return medication;
        }

        protected override BaseEntity NewEntity()
        {
            return new Medication() as BaseEntity;
        }

        public void Delete(Medication medication)
        {
            command.CommandText = $"DELETE FROM Medications WHERE ID = {medication.ID}";
            base.ExecuteNonQuery();
        }

        public void Update(Medication medication)
        {
            command.CommandText = $"UPDATE Medications SET " +
                $"OriginalName = '{medication.OriginalName}', " +
                $"InHealthBox = {medication.InHealthBox}, " +
                $"AvailableInIsrael = {medication.AvailableInIsrael}, " +
                $"MainSubstanceId = {medication.MainSubstanceId} " +
                $"WHERE ID = {medication.ID}";
            base.ExecuteNonQuery();
        }

        public void Insert(Medication medication)
        {
        
            command.CommandText = $"INSERT INTO Medications (OriginalName, InHealthBox, AvailableInIsrael, MainSubstanceId) " +
                $"VALUES ('{medication.OriginalName}', {medication.InHealthBox}, {medication.AvailableInIsrael}, {medication.MainSubstanceId})";
            base.ExecuteNonQuery();
        }

        public MedicationsList GetByUser(User user)
        {
            command.CommandText = "SELECT Medications.* " +
                          "FROM (Users " +
                          "INNER JOIN UserMedications ON Users.ID = UserMedications.UserID) " +
                          "INNER JOIN Medications ON UserMedications.MedicationID = Medications.ID " +
                          $"WHERE Users.ID = {user.ID};";

            return new MedicationsList(base.Select());
        }

        public void DeleteFromUserMedications(int userId, int medicationId)
        {
            command.CommandText = $"DELETE FROM UserMedications WHERE UserID = {userId} AND MedicationID = {medicationId}";
            base.ExecuteNonQuery();
        }
    }
}