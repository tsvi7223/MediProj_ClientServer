using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MediProject_Server.Model
{
    [CollectionDataContract]
    public class KupatHolimList : List<KupatHolim>
    {
        public KupatHolimList() { }
        public KupatHolimList(IEnumerable<KupatHolim> list) : base(list) { }

        public KupatHolimList(IEnumerable<BaseEntity> list) : base(list.Cast<KupatHolim>().ToList()) { }


    }
}