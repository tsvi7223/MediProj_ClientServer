using MediProject_Server.Model;
using System;
namespace MediProject_Server.DB
{
    
    public class PeopleDB : BaseDB
    {
        private static PeopleDB instance;
        protected PeopleDB() { }
        public static PeopleDB GetInstance()
        {
            if (instance == null)
                instance = new PeopleDB();
            return instance;
        }

        public PeopleList SelectAll()
        {
            command.CommandText = "SELECT * FROM People";
            return new PeopleList(base.Select());
        }
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Person person = entity as Person;
            if (person == null) person = new Person();
            person.ID = int.Parse(reader["people.ID"].ToString());          
            person.FirstName = reader["FirstName"].ToString();          
            person.LastName = reader["LastName"].ToString();
            person.DateOfBirth = (DateTime)reader["DateOfBirth"];
            person.Gmail = reader["Gmail"].ToString();
            person.FullAddress = reader["FullAddress"].ToString();
            person.PhoneNumber = reader["PhoneNumber"].ToString();
            return person;
        }
        protected override BaseEntity NewEntity()
        {
            return new Person();
        }

        public void Delete(Person person)
        {
            command.CommandText = $"DELETE FROM people WHERE ID = {person.ID}";
            base.ExecuteNonQuery();
        }

        public void Update(Person person)
        {
            command.CommandText = $"UPDATE people SET FirstName = '{person.FirstName}', LastName = '{person.LastName}', DateOfBirth = #{person.DateOfBirth}#, Gmail = '{person.Gmail}'," +
                $"FullAddress='{person.FullAddress}', PhoneNumber='{person.PhoneNumber}' WHERE ID = {person.ID}";
            base.ExecuteNonQuery();
        }
        public int Insert(Person person)
        {
            int newId = 0;
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO people (FirstName, LastName, DateOfBirth, Gmail, FullAddress, PhoneNumber) VALUES ('{person.FirstName}', '{person.LastName}', #{person.DateOfBirth}#, '{person.Gmail}', '{person.FullAddress}','{person.PhoneNumber}')";
                command.ExecuteNonQuery();
                command.CommandText = "SELECT @@IDENTITY";
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    newId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return newId;

        }
    }
}