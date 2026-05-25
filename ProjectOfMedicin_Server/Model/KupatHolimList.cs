using System.Collections.Generic;
using System.Linq;

namespace ProjectOfMedicin_Server.Model
{
    public class KupatHolimList : List<KupatHolim>
    {
        public KupatHolimList() { }
        public KupatHolimList(IEnumerable<KupatHolim> list) : base(list) { }

        public KupatHolimList(IEnumerable<BaseEntity> list) : base(list.Cast<KupatHolim>().ToList()) { }


    }
}