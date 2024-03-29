﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrywatnaPrzychodniaEntities.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialization { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
