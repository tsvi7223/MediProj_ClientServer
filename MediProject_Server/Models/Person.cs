using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
    public class Person:BaseEntity
    { 

        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gmail { get; set; }
        public string FullAddress { get; set; }
        public Person(int id, string fname, string lName
            , string phoneNumber, DateTime dateOfBirth, string gmail, string fullAddress)
        {
            this.ID = id;
            this.FName = fname;
            this.LName = lName;
            this.PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            this.Gmail = gmail;
            FullAddress = fullAddress;
        }

        public Person()
        {
        }

     

        public string FullName
        {
            get { return $"{FName} {LName}"; }

        }

    }
    
}
