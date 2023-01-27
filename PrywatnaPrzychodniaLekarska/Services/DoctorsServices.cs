using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrywatnaPrzychodniaEntities;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Contracts;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Services
{
    public class DoctorsServices : ConnectionService, ICrudScheme<DoctorsServices>
    {
        private IMapper _mapper;

        public DoctorsServices(PrywatnaPrzychodniaDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public bool Create<TV>(TV userModel)
        {
            var doctorDto = userModel as DoctorModel;
            var doctor = new Doctor
            {
                Address = new Address
                {
                    HouseNumber = doctorDto.HouseNumber,
                    Country = doctorDto.Country,
                    Street = doctorDto.Street,
                    PostCode = doctorDto.PostCode,
                    City = doctorDto.City
                },
                Name = doctorDto.Name,
                Specialization = doctorDto.Specialization,
                Surname = doctorDto.Surname
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var doctor = _context.Doctors?.Where(x => x.Id == id).FirstOrDefault();
            if (doctor != null)
            {
                _context.Remove(doctor);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<TV> Get<TV>()
        {
           var doctors = _context.Doctors.Include(x => x.Address).ToList();
           return _mapper.Map<List<TV>>(doctors);
        }

        public bool Update<TV>(int id, TV model)
        {
            var doctorModel = model as DoctorModel;
            var doctor = _context.Doctors.Include(x => x.Address)?.Where(x => x.Id == id).FirstOrDefault();
            if (doctor != null)
            {
                doctor.Address.HouseNumber = doctorModel.HouseNumber;
                doctor.Address.City = doctorModel.City;
                doctor.Address.Country = doctorModel.Country;
                doctor.Address.PostCode = doctorModel.PostCode;
                doctor.Address.Street = doctorModel.Street;
                doctor.Specialization = doctorModel.Specialization;
                doctor.Name = doctorModel.Name;
                doctor.Surname = doctorModel.Surname;

                _context.Update(doctor);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
