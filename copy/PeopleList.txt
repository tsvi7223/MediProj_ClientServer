using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediProject_Server.Model
{
    [CollectionDataContract]
    public class PeopleList:List<Person>
    {
            public PeopleList() { }

            public PeopleList(IEnumerable<Person> list) : base(list) { }


            public PeopleList(IEnumerable<BaseEntity> list) : base(list.Cast<Person>().ToList()) { }
       
    }
}
