using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediProject_Server.DB
{
    public class UsersDB : PeopleDB
    {
        private static UsersDB instance;

        private UsersDB() { }

        public new static  UsersDB GetInstance()
        {
            if (instance == null)
                instance = new UsersDB();
            return instance;
        }
        public UserList SelectAll()
        { 
            command.CommandText = "SELECT * FROM  (people INNER JOIN  Users ON people.ID = Users.ID)";
            UserList list = new UserList(base.Select());
            foreach (User user in list)
            {
                user.Medications = MedicationsDB.GetInstance().GetByUser(user);
            }
            return list;
        }


        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User user = entity as User;
            base.CreateModel(user);
            user.UserName = reader["UserName"].ToString();
            user.Password = reader["Password"].ToString();
            user.IsAdmin = reader["IsAdmin"] != DBNull.Value && Convert.ToBoolean(reader["IsAdmin"]);
            int kupaId = int.Parse(reader["KupaId"].ToString());
            user.Kupa = KupatHolimDB.GetInstance().SelectAll().Find(kupa => kupa.ID == kupaId);

            return user;
        }

        protected override BaseEntity NewEntity()
        {
            return new User() as BaseEntity;
        }
        public void Delete(User user)
        { 
            command.CommandText = $"DELETE FROM users WHERE Id = {user.ID}";
            base.ExecuteNonQuery();
        }
        public void Update(User user)
        {
            base.Update(user);
            command.CommandText = $"UPDATE users SET userName = '{user.UserName}', [Password] = '{user.Password}', KupaId = {user.Kupa.ID} WHERE(users.ID ={user.ID})";
            base.ExecuteNonQuery();
        }
        public void Insert(User user)
        {
            int userId = base.Insert(user);
            command.CommandText = $"INSERT INTO users (id, userName, [Password], KupaId) VALUES  ({userId}, '{user.UserName}', '{user.Password}', {user.Kupa.ID})";
            base.ExecuteNonQuery();

            if (user.Medications != null && user.Medications.Count > 0)
                foreach (Medication med in user.Medications)
                {
                    command.CommandText = $"INSERT INTO UserMedications (UserID, MedicationID)  VALUES ({user.ID}, {med.ID})";
                    base.ExecuteNonQuery();
                }

        }

     



    }
}