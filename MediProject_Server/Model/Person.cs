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
        // מאפיין לקריאה בלבד (get) המחזיר מחרוזת המשלבת את השם הפרטי ושם המשפחה יחד
        public string FulLastName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}