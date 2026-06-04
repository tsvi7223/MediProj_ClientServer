using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.DB
{
    public class KupatHolimDB:BaseDB
    {
        private static KupatHolimDB instance;

        private KupatHolimDB() { }

        public static KupatHolimDB GetInstance()
        {
            if (instance == null)
                instance = new KupatHolimDB();
            return instance;
        }
        public KupatHolimList SelectAll()
        {
            command.CommandText = "SELECT * FROM KupatHolim";
            return new KupatHolimList(base.Select());
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            KupatHolim kupatHolim = entity as KupatHolim;
            kupatHolim.ID = int.Parse(reader["ID"].ToString());
            kupatHolim.Name = reader["KupaName"].ToString();
            return kupatHolim;
        }

        protected override BaseEntity NewEntity()
        {
            return new KupatHolim() as BaseEntity;
        }
        public void Delete(KupatHolim kupatHolim)
        {
            command.CommandText = $"DELETE FROM kupatHolims WHERE Id = {kupatHolim.ID})";
            base.ExecuteNonQuery();
        }

        public void Update(KupatHolim kupatHolim)
        {
            command.CommandText = $"UPDATE kupatHolims SET Name = '{kupatHolim.Name}',  WHERE ID = {kupatHolim.ID}";
            base.ExecuteNonQuery();
        }
        public void Insert(KupatHolim kupatHolim)
        {
            command.CommandText = $"INSERT INTO kupatholim (Name) " +
                $"VALUES ('{kupatHolim.Name}')";
            base.ExecuteNonQuery();
        }


    }
}
