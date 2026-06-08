using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [DataContract]
    public class User:Person
    {

        public User()
        {
        }

        public User(int id, string FirstName, string LastName, DateTime dateOfBirth, string gmail,  string fullAddress,string PhoneNumber)
            : base(id, FirstName, LastName, PhoneNumber,dateOfBirth, gmail, fullAddress )
        {

        }
        [DataMember]
       public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public KupatHolim Kupa { get; set; }
    }
}
