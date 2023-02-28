using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaLekarska.Models
{
    public class GetVisitModel
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string Specialization { get; set; }
        public double VisitPrice { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
