using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
    public class User:Person
    {
        public User()
        {
        }

        public User(int id, string fname, string lName, DateTime dateOfBirth, string gmail,  string fullAddress,string PhoneNumber)
            : base(id, fname, lName, PhoneNumber,dateOfBirth, gmail, fullAddress )
        {

        }

       public string UserName { get; set; }
        public string Password { get; set; }
        public KupatHolim Kupa { get; set; }
    }
}
