using System;
using System.Runtime.Serialization;

namespace ProjectOfMedicin_Server.Model
{
    [DataContract]
    public class Person : BaseEntity
    {
        [DataMember]
        public string FName { get; set; }

        [DataMember]
        public string LName { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public string Gmail { get; set; }

        [DataMember]
        public string FullAddress { get; set; }

        public Person(int id, string fname, string lName, string phoneNumber, DateTime dateOfBirth, string gmail, string fullAddress)
        {
            this.ID = id;
            this.FName = fname;
            this.LName = lName;
            this.PhoneNumber = phoneNumber;
            this.DateOfBirth = dateOfBirth;
            this.Gmail = gmail;
            this.FullAddress = fullAddress;
        }

        public Person() { }

        // אין צורך ב-[DataMember] כי זה שדה מחושב לקריאה בלבד
        public string FullName
        {
            get { return $"{FName} {LName}"; }
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

//namespace ProjectOfMedicin_Server.Model
//{
//    [DataContract]
//    public class Person:BaseEntity
//    {
//        [DataMember]
//        public string FName { get; set; }
//        [DataMember]

//        public string LName { get; set; }
//        [DataMember]

//        public string PhoneNumber { get; set; }
//        [DataMember]

//        public DateTime DateOfBirth { get; set; }
//        [DataMember]

//        public string Gmail { get; set; }
//        [DataMember]

//        public string FullAddress { get; set; }
//        public Person(int id, string fname, string lName
//            , string phoneNumber, DateTime dateOfBirth, string gmail, string fullAddress)
//        {
//            this.ID = id;
//            this.FName = fname;
//            this.LName = lName;
//            this.PhoneNumber = phoneNumber;
//            DateOfBirth = dateOfBirth;
//            this.Gmail = gmail;
//            FullAddress = fullAddress;
//        }

//        public Person()
//        {
//        }



//        public string FullName
//        {
//            get { return $"{FName} {LName}"; }

//        }

//    }

//}
