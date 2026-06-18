using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MediProject_Server.Model
{
   
    public class MedicationExpiration
  {
    public string MedicationName { get; set; }
    public DateTime ExpirationDate { get; set; }
   }
}