using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_of_medicine.Models
{
    public class SubstanceList : List<Substance>
    {

        public SubstanceList() { }

        public SubstanceList(IEnumerable<Substance> list) : base(list) { }


        public SubstanceList(IEnumerable<BaseEntity> list) : base(list.Cast<Substance>().ToList()) { }

    }
}
