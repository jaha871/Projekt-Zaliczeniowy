using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzychodniaFrontend.Models
{
    public class DoctorsModelView
    {
        public DoctorModel DoctorModel { get; set; }
        public string ResponseMessage { get; set; }

        public DoctorsModelView()
        {
            ResponseMessage = "";
        }
    }
}
