using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.DB
{
  //  [DataContract]

    public abstract class BaseDB
    {
      //  [DataMember]

        private readonly string connectionString;
      //  [DataMember]

        protected OleDbConnection connection;
     //   [DataMember]

        protected OleDbCommand command;
      //  [DataMember]

        protected OleDbDataReader reader;

        protected abstract BaseEntity NewEntity();
        protected abstract BaseEntity CreateModel(BaseEntity entity);

        public BaseDB()
        {
            
           // AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

        
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\..\..\MediProject_Server\DB\DbMedi.accdb;Persist Security Info=True";

            this.connection = new OleDbConnection(connectionString);
            command = new OleDbCommand();
            command.Connection = this.connection;

        }

        protected List<BaseEntity> Select()
        {
            List<BaseEntity> list = new List<BaseEntity>();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                command.Connection = connection;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
                //Console.WriteLine(reader["KupaName"].ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    ex.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                if (reader != null && !reader.IsClosed) { reader.Close(); }
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return list;
        }


        protected void ExecuteNonQuery()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                command.Connection = connection;
                int recordsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    ex.Message + "\nSQL: " + command.CommandText);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void ClearTable(string tableName)
        {
            command.CommandText = $"DELETE FROM {tableName}";
            ExecuteNonQuery();
        }
    }
}
