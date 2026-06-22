using MediProject_Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MediProject_Server
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        PurchaseList GetPurchasesByUser(User user);
        [OperationContract]
        void AddPurchase(Purchase p);
        [OperationContract]
        PeopleList GetAllPeople();
        [OperationContract] 
        Substance GetSubstanceDetails(int mediId);
       
        [OperationContract]
        KupatHolimList GetAllKupas();
        [OperationContract]
        UserList GetAllUsers();
        [OperationContract]
        List<MediProject_Server.Model.MedicationExpiration> GetMedicationExpirationList(User user);
        [OperationContract]

        MedicationsList GetAllMedications();
        [OperationContract]
        bool InsertUser(User user);

        [OperationContract]
        void DeleteMedication(Medication medication);
        [OperationContract]
        bool AddMedi(Medication medication);
        [OperationContract]
        List<Medication> GetMedicationsByUser(int userId); 

        [OperationContract]
        void RemoveFromUserList(int userId, int medicationId);

        [OperationContract]
        string GetData(int value);
        [OperationContract]
        MedicationsList GetUserMedications(User user);
        [OperationContract]

        void AddMedication(User user, Medication medication);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        
    }


    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
