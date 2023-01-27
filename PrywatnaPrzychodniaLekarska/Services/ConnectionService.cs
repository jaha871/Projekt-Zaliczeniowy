using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrywatnaPrzychodniaEntities;

namespace PrywatnaPrzychodniaLekarska.Services
{
    public abstract class ConnectionService 
    {
        public PrywatnaPrzychodniaDbContext _context;
        public ConnectionService(PrywatnaPrzychodniaDbContext context)
        {
            _context = context;
        }

        public ConnectionService()
        {
        }
    }
}
