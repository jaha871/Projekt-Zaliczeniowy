using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaLekarska.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(20)]
        public string Specialization { get; set; }

        public string Street { get; set; }
        [Required]
        [MaxLength(5)]
        public string HouseNumber { get; set; }
        [Required]
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
