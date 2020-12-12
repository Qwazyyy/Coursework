using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Entities
{
    public class Service
    {
        //добавить связи
        public int ID { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int Price { get; set; }
        public bool Delete { get; set; }
    }
}
