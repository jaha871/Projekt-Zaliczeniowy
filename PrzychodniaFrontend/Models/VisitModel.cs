using System;
using System.ComponentModel.DataAnnotations;

namespace PrzychodniaFrontend.Models
{
    public class VisitModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateOfVisit { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public double VisitPrice { get; set; }

    }
}
