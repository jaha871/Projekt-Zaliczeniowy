using System;
using System.ComponentModel.DataAnnotations;

namespace PrzychodniaFrontend.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Pesel { get; set; }
        public string Street { get; set; }
 
        public string HouseNumber { get; set; }

        public string PostCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
