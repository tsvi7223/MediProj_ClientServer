using MediProject_Server.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MediProject_Server.Model
{
    [CollectionDataContract]
    public class PurchaseList : List<Purchase>
    { 
        public PurchaseList() { }

        public PurchaseList(IEnumerable<Purchase> list) : base(list) { }

        
        public PurchaseList(IEnumerable<BaseEntity> list) : base(list.Cast<Purchase>().ToList()) { }
    }
}
