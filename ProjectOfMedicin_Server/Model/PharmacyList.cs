using ProjectOfMedicin_Server.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProjectOfMedicin_Server.DB
{

    public class PharmacyList : List<Pharmacy>
    { 
        public PharmacyList() { }

        public PharmacyList(IEnumerable<Pharmacy> list) : base(list) { }

        
        public PharmacyList(IEnumerable<BaseEntity> list) : base(list.Cast<Pharmacy>().ToList()) { }
    }
}
