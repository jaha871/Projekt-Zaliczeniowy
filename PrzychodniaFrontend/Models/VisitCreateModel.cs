using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzychodniaFrontend.Models
{
    public class VisitCreateModel
    {
        public VisitModel VisitModel { get; set; }
        public List<PatientModel> PatientModel { get; set; }
        public List<DoctorModel> DoctorModel { get; set; }
    }
}
