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
            user.UserName = reader["UserName"].ToString();
            user.Password = reader["Password"].ToString();
            user.Kupa = KupatHolimDB.GetInstance().SelectAll().Find(kupa => kupa.ID == user.Kupa.ID);

            //user.Gmail = reader["Gmail"].ToString();


            return user;
        }

        protected override BaseEntity NewEntity()
        {
            return new User() as BaseEntity;
        }
        public void Delete(User user)
        {
            base.Delete(user);
            command.CommandText = $"DELETE FROM users WHERE Id = {user.ID})";
            base.ExecuteNonQuery();
        }

        public void Update(User user)
        {
            base.Update(user);
            // command.CommandText = $"UPDATE people SET fName = '{user.fName}', lName = '{user.lName}', WHERE ID = {user.Id}   ";
            command.CommandText = $"UPDATE users SET userName = '{user.UserName}', [Password] = '{user.Password}', KupaId = {user.Kupa.ID} WHERE(users.ID ={user.ID})";
            base.ExecuteNonQuery();
        }
        public void Insert(User user)
        {
            base.Insert(user);
            command.CommandText = $"INSERT INTO users (id, userName, [Password], KupaId) VALUES  ({user.ID}, '{user.UserName}', '{user.Password}', {user.Kupa.ID})";
            base.ExecuteNonQuery();
        }



    }
}
