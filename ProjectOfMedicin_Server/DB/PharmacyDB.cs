using ProjectOfMedicin_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOfMedicin_Server.DB
{
    public class PharmacyDB:BaseDB
    {
        private static PharmacyDB instance;

        private PharmacyDB() { }

        public static PharmacyDB GetInstance()
        {
            if (instance == null)
                instance = new PharmacyDB();
            return instance;
        }
        public PharmacyList SelectAll()
        {
            command.CommandText = "SELECT * FROM Pharmacy";
            return new PharmacyList(base.Select());
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Pharmacy pharmacy = entity as Pharmacy;
            pharmacy.ID = int.Parse(reader["Id"].ToString());
            pharmacy.Name = reader["Name"].ToString();
            return pharmacy;
        }

        protected override BaseEntity NewEntity()
        {
            return new Pharmacy() as BaseEntity;
        }
        public void Delete(Pharmacy pharmacy)
        {
            command.CommandText = $"DELETE FROM pharmacys WHERE Id = {pharmacy.ID})";
            base.ExecuteNonQuery();
        }

        public void Update(Pharmacy pharmacy)
        {
            command.CommandText = $"UPDATE pharmacys SET Name = '{pharmacy.Name}',  WHERE ID = {pharmacy.ID}";
            base.ExecuteNonQuery();
        }
        public void Insert(Pharmacy pharmacy)
        {
            command.CommandText = $"INSERT INTO pharmacies (Name) " +
                $"VALUES ('{pharmacy.Name}')";
            base.ExecuteNonQuery();
        }
    }
}
