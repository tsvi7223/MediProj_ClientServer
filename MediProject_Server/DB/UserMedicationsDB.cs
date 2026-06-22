using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    public class UserMedicationsDB : BaseDB
    {
        private static UserMedicationsDB instance;

        private UserMedicationsDB() { }
        public static UserMedicationsDB GetInstance()
        {
            if (instance == null)
                instance = new UserMedicationsDB();
            return instance;
        }
        public void Delete(User user, Medication medication)
        {
            command.CommandText = $"DELETE FROM UserMedications WHERE userId = {user.ID} AND MedicationId = {medication.ID}";
            base.ExecuteNonQuery();
        }
        public void Insert(User user, Medication medication)
        {
            command.CommandText = $"INSERT INTO UserMedications (UserId, MedicationId) " +
                $"VALUES ({user.ID}, {medication.ID})";
            base.ExecuteNonQuery();
        }

        protected override BaseEntity NewEntity()
        {
            throw new NotImplementedException();
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            throw new NotImplementedException();
        }


    }
}