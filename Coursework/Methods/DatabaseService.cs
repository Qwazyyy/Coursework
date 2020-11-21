using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Methods
{ 
    public class DatabaseService
    {
        public static void DeleteRowFromClients(int id, DatabaseContext _context)
        {
            var delete = _context.Clients.Where(c => c.ID == id).FirstOrDefault();
            _context.Clients.Remove(delete);
            _context.SaveChanges();
        }
    }
}
