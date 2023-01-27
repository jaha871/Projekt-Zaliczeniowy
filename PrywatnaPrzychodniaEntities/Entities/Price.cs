using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaEntities.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public double VisitPrice { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
    }
}
