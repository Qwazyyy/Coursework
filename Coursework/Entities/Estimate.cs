using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Entities
{
    public class Estimate
    {
        //добавить связи
        public int ID { get; set; }
        public double Quantity { get; set; }
        //public int FullPrice { get; set; }

        public int ContractID { get; set; }
        public int ServiceID { get; set; }
        public Service Service { get; set; }
        //public ICollection<Service> Services {get;set;}
       // public Estimate()
       // {
           // Services = new List<Service>();
        //}
    }
}
