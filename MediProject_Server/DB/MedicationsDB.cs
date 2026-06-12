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
                int substenceID = int.Parse(reader["MainSubctenceId"].ToString());
                medication.ActiveSubstance = SubstanceDB.GetInstance().SelectAll().Find(sub => sub.ID == substenceID);
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
                command.CommandText = $"DELETE FROM Medications WHERE Id = {medication.ID})";
                base.ExecuteNonQuery();
            }

            public void Update(Medication medication)
            {
            // command.CommandText = $"UPDATE people SET FirstName = '{person.FirstName}', LastName = '{person.LastName}', WHERE ID = {person.Id}   ";
            command.CommandText = $"UPDATE Medications SET OriginalName = '{medication.OriginalName}', InHealthBox = {medication.InHealthBox}, AvailableInIsrael = {medication.AvailableInIsrael}, MainSubctenceId = {medication.ActiveSubstance.ID} " +
                $"WHERE (Medications.ID = {medication.ID})";
                base.ExecuteNonQuery();
            }
            public void Insert(Medication medication)
            {
            command.CommandText = $"INSERT INTO Medications " +
                $"(OriginalName, InHealthBox, AvailableInIsrael, MainSubctenceId)" +
                $" VALUES ('{medication.OriginalName}', {medication.InHealthBox},{medication.AvailableInIsrael}, {medication.ActiveSubstance.ID})";
                base.ExecuteNonQuery();
            }




        
    }
}
