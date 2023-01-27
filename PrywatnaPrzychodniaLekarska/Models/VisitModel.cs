using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaLekarska.Models
{
    public class VisitModel
    {
        public int DoctorId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public double VisitPrice { get; set; }

    }
}
