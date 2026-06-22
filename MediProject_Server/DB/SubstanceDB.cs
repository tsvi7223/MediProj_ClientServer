using MediProject_Server.Model;
using System;
using System.Collections.Generic;

namespace MediProject_Server.DB
{
    public class SubstanceDB : BaseDB
    {
        private static SubstanceDB instance;
        private SubstanceDB() { }

        public static SubstanceDB GetInstance()
        {
            if (instance == null)
                instance = new SubstanceDB();
            return instance;
        }

        public SubstanceList SelectAll()
        {
            command.CommandText = "SELECT * FROM Substance";
            return new SubstanceList(base.Select());
        }

        public Substance SelectById(int substanceId)
        {
            command.CommandText = $"SELECT * FROM Substance WHERE ID = {substanceId}";
            List<BaseEntity> list = base.Select();
            if (list != null && list.Count > 0)
            {
                return (Substance)list[0];
            }
            return null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Substance substance = entity as Substance;
            substance.ID = int.Parse(reader["ID"]?.ToString() ?? "0");
            substance.Description = reader["Description"]?.ToString() ?? "";
            substance.SubstanceName = reader["SubstanceName"]?.ToString() ?? "";

            return substance;
        }

        protected override BaseEntity NewEntity()
        {
            return new Substance() as BaseEntity;
        }
        public void Delete(Substance substance)
        {
            command.CommandText = $"DELETE FROM Substance WHERE ID = {substance.ID}";
            base.ExecuteNonQuery();
        }

        public void Update(Substance substance)
        {
            command.CommandText = $"UPDATE Substance SET SubstanceName = '{substance.SubstanceName}', Description = '{substance.Description}' WHERE ID = {substance.ID}";
            base.ExecuteNonQuery();
        }

        public void Insert(Substance substance)
        {
            command.CommandText = $"INSERT INTO Substance (SubstanceName, Description) VALUES ('{substance.SubstanceName}', '{substance.Description}')";
            base.ExecuteNonQuery();
        }
    }
}