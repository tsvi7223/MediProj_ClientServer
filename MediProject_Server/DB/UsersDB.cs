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
            command.CommandText = "SELECT * FROM  (people INNER JOIN  Users ON people.ID = Users.ID)";
            return new UserList(base.Select());
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = entity as User;
            //user.ID = int.Parse(reader["Users.ID"].ToString());
            base.CreateModel(user);
            user.UserName = reader["UserName"].ToString();
            user.Password = reader["Password"].ToString();
            int kupaId = int.Parse(reader["KupaId"].ToString());
            user.Kupa = KupatHolimDB.GetInstance().SelectAll().Find(kupa => kupa.ID == kupaId);

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
            // command.CommandText = $"UPDATE people SET FirstName = '{user.FirstName}', LastName = '{user.LastName}', WHERE ID = {user.Id}   ";
            command.CommandText = $"UPDATE users SET userName = '{user.UserName}', [Password] = '{user.Password}', KupaId = {user.Kupa.ID} WHERE(users.ID ={user.ID})";
            base.ExecuteNonQuery();
        }
        public void Insert(User user)
        {
            int userId  = base.Insert(user);
            command.CommandText = $"INSERT INTO users (id, userName, [Password], KupaId) VALUES  ({userId}, '{user.UserName}', '{user.Password}', {user.Kupa.ID})";
            base.ExecuteNonQuery();
        }



    }
}
