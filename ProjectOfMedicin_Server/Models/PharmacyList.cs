using Project_of_medicine.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_of_medicine.DB
{

    public class PharmacyList : List<Pharmacy>
    { 
        public PharmacyList() { }

        public PharmacyList(IEnumerable<Pharmacy> list) : base(list) { }

        
        public PharmacyList(IEnumerable<BaseEntity> list) : base(list.Cast<Pharmacy>().ToList()) { }
    }
}
