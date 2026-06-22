using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    public class PurchasesDB : BaseDB
    {
        private static PurchasesDB instance;

        private PurchasesDB() { }
        public static PurchasesDB GetInstance()
        {
            if (instance == null)
                instance = new PurchasesDB();
            return instance;
        }
        public PurchaseList SelectAll()
        {
            command.CommandText = "SELECT * FROM Purchases";
            return new PurchaseList(base.Select());
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Purchase purchase = entity as Purchase;
            purchase.ID = int.Parse(reader["Id"].ToString());
            
            int userId = int.Parse(reader["UserID"].ToString());
            purchase.user = UsersDB.GetInstance().SelectAll().Find(u=>u.ID == userId);
            purchase.PillsPerDay = int.Parse(reader["PillsPerDay"].ToString());
            purchase.PillAmount = int.Parse(reader["PillAmount"].ToString());
            int mediId = int.Parse(reader["MedicationId"].ToString());
            purchase.medication = MedicationsDB.GetInstance().SelectAll().Find(medi => medi.ID == mediId);
            purchase.PurchaseDate = DateTime.Parse(reader["PurchaseDate"].ToString());
            return purchase;
        }

        protected override BaseEntity NewEntity()
        {
            return new Purchase() as BaseEntity;
        }
        public void Delete(Purchase purchase)
        {
            command.CommandText = $"DELETE FROM Purchases WHERE Id = {purchase.ID})";
            base.ExecuteNonQuery();
        }
        public void Update(Purchase purchase)
        {
            command.CommandText = $"UPDATE Purchases SET PurchaseDate =#{purchase.PurchaseDate}#, UserID ={purchase.user.ID}," +
                $"PillsPerDay ={purchase.PillsPerDay}, PillAmount ={purchase.PillAmount}, MedicationId ={purchase.medication.ID} WHERE(Purchases.ID = 1)";

            base.ExecuteNonQuery();
        }
        public void Insert(Purchase purchase)
        {
            command.CommandText = $"INSERT INTO Purchases " +
                $"(PurchaseDate, UserID, PillsPerDay, PillAmount, MedicationId) " +
                $"VALUES(#{purchase.PurchaseDate}#, {purchase.user.ID}, {purchase.PillsPerDay}, {purchase.PillAmount}, {purchase.medication.ID})";
            base.ExecuteNonQuery();
        }
    }
}