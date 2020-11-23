using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Entities
{
    public class EstimateAndService
    {
        public int ContractID { get; set; }
        //public int ServiceID { get; set; }
        public string SeviceName { get; set; }
        public int ServicePrice { get; set; }
        public string ServiceUnit { get; set; }
        public int EstimateFullPrice { get; set; }
        public int EstimateCount { get; set; }
    }
}
