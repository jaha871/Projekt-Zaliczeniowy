using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Mappers
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<Patient, PatientModel>()
                .ForMember(x => x.City, v => v.MapFrom(c => c.Address.City))
                .ForMember(x => x.Country, v => v.MapFrom(c => c.Address.Country))
                .ForMember(x => x.PostCode, v => v.MapFrom(c => c.Address.PostCode))
                .ForMember(x => x.HouseNumber, v => v.MapFrom(c => c.Address.HouseNumber))
                .ForMember(x => x.Street, v => v.MapFrom(c => c.Address.Street));
        }
    }
}
