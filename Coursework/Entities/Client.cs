using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Entities
{
    public class Client
    {
        //добавить связи
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Contract> Contracts { get; set; }
        public Client()
        {
            Contracts = new List<Contract>();
        }
            
    }
}
