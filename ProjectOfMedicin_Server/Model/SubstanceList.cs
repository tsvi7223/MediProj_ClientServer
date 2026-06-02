using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [CollectionDataContract]
    public class SubstanceList : List<Substance>
    {

        public SubstanceList() { }

        public SubstanceList(IEnumerable<Substance> list) : base(list) { }


        public SubstanceList(IEnumerable<BaseEntity> list) : base(list.Cast<Substance>().ToList()) { }

    }
}
