using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Mappers
{
    public class VisitMapperProfile : Profile
    {
        public VisitMapperProfile()
        {
            CreateMap<Visit, GetVisitModel>()
                .ForMember(x => x.HouseNumber, v => v.MapFrom(c => c.Patient.Address.HouseNumber))
                .ForMember(x => x.VisitPrice, v => v.MapFrom(c => c.Price.VisitPrice))
                .ForMember(x => x.Discount, v => v.MapFrom(c => c.Price.Discount))
                .ForMember(x => x.Street, v => v.MapFrom(c => c.Patient.Address.Street))
                .ForMember(x => x.Country, v => v.MapFrom(c => c.Patient.Address.Country))
                .ForMember(x => x.City, v => v.MapFrom(c => c.Patient.Address.City))
                .ForMember(x => x.PostCode, v => v.MapFrom(c => c.Patient.Address.PostCode))
                .ForMember(x => x.DoctorName, v => v.MapFrom(c => c.Doctor.Name))
                .ForMember(x => x.DoctorSurname, v => v.MapFrom(c => c.Doctor.Surname))
                .ForMember(x => x.PatientName, v => v.MapFrom(c => c.Patient.Name))
                .ForMember(x => x.PatientSurname, v => v.MapFrom(c => c.Patient.Surname))
                .ForMember(x => x.Specialization, v => v.MapFrom(c => c.Doctor.Specialization))
                .ForMember(x => x.Description, v => v.MapFrom(c => c.Price.Description));
        }
    }
}
