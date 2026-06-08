using MediProject_Server.Model;
using System;

namespace MediProject_Server.DB
{
    // הוסר ה-DataContract - המחלקה הזו לא עוברת ברשת!
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

            person.ID = int.Parse(reader["ID"].ToString());
            person.FName = reader["fName"].ToString();
            person.LName = reader["lName"].ToString();
            person.DateOfBirth = (DateTime)reader["DateOfBirth"];
            person.Gmail = reader["Gmail"].ToString();

            return person;
        }

        protected override BaseEntity NewEntity()
        {
            return new Person();
        }

        public void Delete(Person person)
        {
            // תוקן: הוסר הסוגר המיותר בסוף
            command.CommandText = $"DELETE FROM people WHERE ID = {person.ID}";
            base.ExecuteNonQuery();
        }

        public void Update(Person person)
        {
            // תוקן: סינטקס ה-WHERE תוקן, ותוקן ה-LName שקיבל בטעות FName
            command.CommandText = $"UPDATE people SET fName = '{person.FName}', lName = '{person.LName}', DateOfBirth = #{person.DateOfBirth:#yyyy-MM-dd#}, Gmail = '{person.Gmail}' WHERE ID = {person.ID}";
            base.ExecuteNonQuery();
        }

        public void Insert(Person person)
        {
            command.CommandText = $"INSERT INTO people (fName, lName, DateOfBirth, Gmail) VALUES ('{person.FName}', '{person.LName}', #{person.DateOfBirth:#yyyy-MM-dd#}, '{person.Gmail}')";
            base.ExecuteNonQuery();
        }
    }
}
//using MediProject_Server.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;

//namespace MediProject_Server.DB
//{
//   // [DataContract]

//    public class PeopleDB : BaseDB
//    {
//       // [DataMember]
//        private static PeopleDB instance;

//        protected PeopleDB() { }

//        public static PeopleDB GetInstance()
//        {
//            if (instance == null)
//                instance = new PeopleDB();
//            return instance;
//        }
//        public PeopleList SelectAll()
//        {
//            command.CommandText = "SELECT * FROM People";
//            return new PeopleList(base.Select());
//        }

//        protected override BaseEntity CreateModel(BaseEntity entity)
//        {
//            Person person = entity as Person;
//            person.ID = int.Parse(reader["ID"].ToString());
//            person.FName = reader["fName"].ToString();
//            person.LName = reader["lName"].ToString();
//            person.DateOfBirth = (DateTime)reader["DateOfBirth"];
//            person.Gmail = reader["Gmail"].ToString();


//            return person;
//        }

//        protected override BaseEntity NewEntity()
//        {
//            return new Person() as BaseEntity;
//        }
//        public void Delete(Person person)
//        {
//            command.CommandText = $"DELETE FROM people WHERE Id = {person.ID})";
//            base.ExecuteNonQuery();
//        }

//        public void Update(Person person)
//        {
//            // command.CommandText = $"UPDATE people SET fName = '{person.fName}', lName = '{person.lName}', WHERE ID = {person.Id}   ";
//            command.CommandText = $"UPDATE people SET fName = '{person.FName}', lName = '{person.FName}', DateOfBirth = #{person.DateOfBirth}#, Gmail = '{person.Gmail}', ID = WHERE(people.ID ={person.ID})";
//            base.ExecuteNonQuery();
//        }
//        public void Insert(Person person)
//        {
//            command.CommandText = $"INSERT INTO people (fName, lName, DateOfBirth, Gmail) VALUES  ('{person.FName}', '{person.LName}', #{person.DateOfBirth}#, '{person.Gmail}')";
//            base.ExecuteNonQuery();
//        }




//    }
//}
