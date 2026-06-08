using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.DB
{
    public class UsersDB: PeopleDB
    {

        private static UsersDB instance;

        private UsersDB() { }

        public static UsersDB GetInstance()
        {
            if (instance == null)
                instance = new UsersDB();
            return instance;
        }
        public UserList SelectAll()
        {
            command.CommandText = "SELECT * FROM Users";
            return new UserList(base.Select());
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = entity as User;
            user.ID = int.Parse(reader["ID"].ToString());
            base.CreateModel(user);
            //user.FName = reader["fName"].ToString();
            //user.LName = reader["lName"].ToString();
            //user.DateOfBirth = (DateTime)reader["DateOfBirth"];
            //user.Gmail = reader["Gmail"].ToString();


            return user;
        }

        protected override BaseEntity NewEntity()
        {
            return new User() as BaseEntity;
        }
        public void Delete(User user)
        {
            command.CommandText = $"DELETE FROM people WHERE Id = {user.ID})";
            base.ExecuteNonQuery();
        }

        public void Update(User user)
        {
            // command.CommandText = $"UPDATE people SET fName = '{user.fName}', lName = '{user.lName}', WHERE ID = {user.Id}   ";
            command.CommandText = $"UPDATE people SET fName = '{user.FName}', lName = '{user.LName}', DateOfBirth = #{user.DateOfBirth}#, Gmail = '{user.Gmail}', ID = WHERE(people.ID ={user.ID})";
            base.ExecuteNonQuery();
        }
        public void Insert(User user)
        {
            command.CommandText = $"INSERT INTO people (fName, lName, DateOfBirth, Gmail) VALUES  ('{user.FName}', '{user.LName}', #{user.DateOfBirth}#, '{user.Gmail}')";
            base.ExecuteNonQuery();
        }



    }
}
