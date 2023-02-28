using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaLekarska.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public DateTime DateOfVisit { get; set; }
        [Required]
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }

        public double VisitPrice { get; set; }

    }
}
