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
   
    public class Service1 : IService1
    {
        public PurchaseList GetPurchasesByUser(User user)
        {
            return new PurchaseList(PurchasesDB.GetInstance().SelectAll().FindAll(p => p.user.ID == user.ID));
        }

        public PeopleList GetAllPeople()
        {
            PeopleDB peopleDB = PeopleDB.GetInstance();
            PeopleList people = peopleDB.SelectAll();
            return people;
        }
        public void AddPurchase(Purchase p)
        {
            PurchasesDB.GetInstance().Insert(p);
        }

        public void DeleteMedication(Medication medication)
        {
            MedicationsDB medicationsDB = MedicationsDB.GetInstance();
            medicationsDB.Delete(medication);
        }
        public void AddMedi(Medication medication)
        {

            MedicationsDB medicationsDB = MedicationsDB.GetInstance();

            medicationsDB.Insert(medication);
        }


        public MedicationsList GetAllMedications()
        {
            MedicationsDB medicationsDB = MedicationsDB.GetInstance();
            MedicationsList medications = medicationsDB.SelectAll();
            return medications;
        }
        public List<MediProject_Server.Model.Medication> GetMedicationsByUser(int userId)
        { 
            MediProject_Server.Model.User tempUser = new MediProject_Server.Model.User();
            tempUser.ID = userId;

            return MedicationsDB.GetInstance().GetByUser(tempUser).ToList();
        }

        public void RemoveFromUserList(int userId, int medicationId)
        {
            MedicationsDB.GetInstance().DeleteFromUserMedications(userId, medicationId);
        }
        public KupatHolimList GetAllKupas()
        {
            KupatHolimDB kupatHolimDB = KupatHolimDB.GetInstance();
            KupatHolimList kupas = kupatHolimDB.SelectAll();
            return kupas;
        }
        public UserList GetAllUsers()
        {
            UsersDB usersDB = UsersDB.GetInstance();
            UserList users = usersDB.SelectAll();
            return users;
        }
        public MedicationsList GetUserMedications(User user)
        {
            return MedicationsDB.GetInstance().GetByUser(user);
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

        public void AddMedication(User user, Medication medication)
        {
            UserMedicationsDB.GetInstance().Insert(user, medication);
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
