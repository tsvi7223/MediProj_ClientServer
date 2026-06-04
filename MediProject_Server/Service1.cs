using MediProject_Server.DB;
using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows;

namespace MediProject_Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {

        public PeopleList GetAllPeople()
        {
            PeopleDB peopleDB = PeopleDB.GetInstance();
            PeopleList people = peopleDB.SelectAll();
            return people;
        }
        public void InsertUser(User user)
        {
            UsersDB usersDB = UsersDB.GetInstance();
            usersDB.Insert(user);
            Console.WriteLine("==========================user inserted!=========================");
            MessageBox.Show("Got User:\nUserName is: " + user.UserName + "\n" + "Address is: " + user.FullAddress +
                "\nPhone Number is: " + user.PhoneNumber + "\nGmail is: " + user.Gmail +
                "\nPassword is: " + user.Password);
        }
        public string GetData(int value)        
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
