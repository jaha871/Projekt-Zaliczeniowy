using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Models;
using PrywatnaPrzychodniaLekarska.Services;

namespace PrywatnaPrzychodniaLekarska.Mappers
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            CreateMap<Doctor, DoctorModel>()
                .ForMember(x => x.City, v => v.MapFrom(c => c.Address.City))
                .ForMember(x => x.Country, v => v.MapFrom(c => c.Address.Country))
                .ForMember(x => x.PostCode, v => v.MapFrom(c => c.Address.PostCode))
                .ForMember(x => x.HouseNumber, v => v.MapFrom(c => c.Address.HouseNumber))
                .ForMember(x => x.Street, v => v.MapFrom(c => c.Address.Street))
                .ForMember(x => x.Id, v => v.MapFrom(a => a.Id) );
        }
    }
}
