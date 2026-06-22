using MediProject_Server.DB;
using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace MediProject_Server
{
    public class Service1 : IService1
    {
        public PurchaseList GetPurchasesByUser(User user)
        {
            List<Purchase> allPurchases = PurchasesDB.GetInstance().SelectAll();
            List<Purchase> filtered = new List<Purchase>();
            foreach (Purchase p in allPurchases)
            {
                if (p.user != null && p.user.ID == user.ID && p.medication != null)
                {
                    filtered.Add(p);
                }
            }
            return new PurchaseList(filtered);
        }

        public PeopleList GetAllPeople()
        {
            return PeopleDB.GetInstance().SelectAll();
        }

        public Substance GetSubstanceDetails(int mediId)
        {
            return SubstanceDB.GetInstance().SelectById(mediId);
        }

        public void AddPurchase(Purchase p)
        {
            PurchasesDB.GetInstance().Insert(p);

            bool exists = false;
            List<MediProject_Server.Model.Medication> userMeds = MedicationsDB.GetInstance().GetByUser(p.user).ToList();

            foreach (MediProject_Server.Model.Medication m in userMeds)
            {
                if (m.ID == p.medication.ID)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                UserMedicationsDB.GetInstance().Insert(p.user, p.medication);
            }
        }

        public void DeleteMedication(Medication medication)
        {
            MedicationsDB.GetInstance().Delete(medication);
        }

        public bool AddMedi(Medication medication)
        {
            // 1. מושכים את הרשימה מה-DB
            SubstanceList allSubstances = SubstanceDB.GetInstance().SelectAll();
            bool substanceExists = false;
            foreach (Substance s in allSubstances)
            {
                if (s.ID == medication.MainSubstanceId)
                {
                    substanceExists = true;
                    break; 
                }
            }
            if (!substanceExists)
            {
                return false; 
            }
            MedicationsDB.GetInstance().Insert(medication);
            return true;
        }

        public MedicationsList GetAllMedications()
        {
            return MedicationsDB.GetInstance().SelectAll();
        }

        public List<MediProject_Server.Model.Medication> GetMedicationsByUser(int userId)
        {
            User tempUser = new User();
            tempUser.ID = userId;
            return MedicationsDB.GetInstance().GetByUser(tempUser).ToList();
        }

        public void RemoveFromUserList(int userId, int medicationId)
        {
            MedicationsDB.GetInstance().DeleteFromUserMedications(userId, medicationId);
        }

        public KupatHolimList GetAllKupas()
        {
            return KupatHolimDB.GetInstance().SelectAll();
        }

        public bool InsertUser(User user)
        {
            
            UserList allUsers = UsersDB.GetInstance().SelectAll();
            foreach (User u in allUsers)
            {
                if (u.UserName == user.UserName)
                {
                    return false; 
                }
            }

            try
            {
                UsersDB.GetInstance().Insert(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserList GetAllUsers()
        {
            return UsersDB.GetInstance().SelectAll();
        }

        public MedicationsList GetUserMedications(User user)
        {
            return MedicationsDB.GetInstance().GetByUser(user);
        }

        public List<MedicationExpiration> GetMedicationExpirationList(User user)
        {
            PurchaseList purchases = GetPurchasesByUser(user);
            if (purchases == null) return new List<MedicationExpiration>();

            List<MedicationExpiration> list = new List<MedicationExpiration>();

         
            foreach (Purchase p in purchases)
            {
                if (p.medication != null && p.PillsPerDay > 0)
                {
                    int daysLeft = p.PillAmount / p.PillsPerDay;
                    list.Add(new MedicationExpiration
                    {
                        MedicationName = p.medication.OriginalName,
                        ExpirationDate = p.PurchaseDate.AddDays(daysLeft)
                    });
                }
            }
            return list;
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