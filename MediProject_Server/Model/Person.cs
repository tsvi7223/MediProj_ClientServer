using System;
using System.Runtime.Serialization;

namespace MediProject_Server.Model
{
    [DataContract]
    [KnownType(typeof(User))]
    public class Person : BaseEntity
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [DataMember]
        public string Gmail { get; set; }

        [DataMember]
        public string FullAddress { get; set; }

        public Person(int id, string FirstName, string LastName, string phoneNumber, DateTime dateOfBirth, string gmail, string fullAddress)
        {
            this.ID = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = phoneNumber;
            this.DateOfBirth = dateOfBirth;
            this.Gmail = gmail;
            this.FullAddress = fullAddress;
        }

        public Person() { }

        // אין צורך ב-[DataMember] כי זה שדה מחושב לקריאה בלבד
        public string FulLastName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
//using System;
 //using System.Collections.Generic;
 //using System.Linq;
 //using System.Runtime.Serialization;
 //using System.ServiceModel;
 //using System.Text;
 //using System.Threading.Tasks;

//namespace MediProject_Server.Model
//{
//    [DataContract]
//    public class Person:BaseEntity
//    {
//        [DataMember]
//        public string FirstName { get; set; }
//        [DataMember]

//        public string LastName { get; set; }
//        [DataMember]

//        public string PhoneNumber { get; set; }
//        [DataMember]

//        public DateTime DateOfBirth { get; set; }
//        [DataMember]

//        public string Gmail { get; set; }
//        [DataMember]

//        public string FullAddress { get; set; }
//        public Person(int id, string FirstName, string LastName
//            , string phoneNumber, DateTime dateOfBirth, string gmail, string fullAddress)
//        {
//            this.ID = id;
//            this.FirstName = FirstName;
//            this.LastName = LastName;
//            this.PhoneNumber = phoneNumber;
//            DateOfBirth = dateOfBirth;
//            this.Gmail = gmail;
//            FullAddress = fullAddress;
//        }

//        public Person()
//        {
//        }



//        public string FulLastName
//        {
//            get { return $"{FirstName} {LastName}"; }

//        }

//    }

//}
