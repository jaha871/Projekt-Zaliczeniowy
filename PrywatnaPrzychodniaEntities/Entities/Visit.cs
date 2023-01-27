using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaEntities.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public DateTime DateOfVisit { get; set; }
        public int PriceId { get; set; }
        public virtual Price Price { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
