using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Contracts
{
    public interface ICrudScheme<T>
    {
        bool Create<TV>(TV userModel);
        bool Delete(int id);
        public List<TV> Get<TV>();
        bool Update<TV>(int id, TV model);
        public TV GetById<TV>(int id);
    }
}
