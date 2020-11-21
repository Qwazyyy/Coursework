using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Entities
{
    public class Contract
    {
        //добавить связи
        public int ID { get; set; }
        public DateTime DateConclusionContract { get; set; }
        public DateTime DateOfCompletion { get; set; }
        public int TotalAmount { get; set; }

        public int? ClientID { get; set; }
        public Client Client { get; set; }

        public ICollection<Estimate> Estimates {get;set;}
        public Contract()
        {
            Estimates = new List<Estimate>();
        }
    }
}
