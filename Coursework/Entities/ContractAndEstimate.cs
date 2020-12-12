using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Entities
{
    public class ContractAndEstimate
    {
        public int ContractID { get; set; }
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateConclusionContract { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public int TotalAmount { get; set; }
    }
}
