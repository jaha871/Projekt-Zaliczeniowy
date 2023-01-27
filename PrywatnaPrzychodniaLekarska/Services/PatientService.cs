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
    public class PatientService : ConnectionService, ICrudScheme<PatientService>
    {
        private IMapper _mapper;

        public PatientService(PrywatnaPrzychodniaDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public bool Create<TV>(TV userModel)
        {
            var patientDto = userModel as PatientModel;
            var patient = new Patient
            {
                Name = patientDto.Name,
                DateOfBirth = patientDto.DateOfBirth,
                Pesel = patientDto.Pesel,
                Surname = patientDto.Surname,
                Address = new Address
                {
                    HouseNumber = patientDto.HouseNumber,
                    Country = patientDto.Country,
                    Street = patientDto.Street,
                    PostCode = patientDto.PostCode,
                    City = patientDto.City
                }
            };

            _context.Add(patient);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var patient = _context.Patients?.Where(x => x.Id == id).FirstOrDefault();
            if (patient != null)
            {
                var address = _context.Addresses?.Where(x => x.Id == patient.AddressId).FirstOrDefault();
                _context.Remove(address);
                _context.Remove(patient);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<TV> Get<TV>()
        {
            var patients = _context.Patients.Include(x => x.Address).ToList();
            var patientsDto = _mapper.Map<List<TV>>(patients);
            return patientsDto;
        }

        public bool Update<TV>(int id, TV model)
        {
            var patientModel = model as PatientModel;
            var patient = _context.Patients.Include(x => x.Address).Where(x => x.Id == id)?.FirstOrDefault();
            if (patient != null)
            {
                patient.Address.HouseNumber = patientModel.HouseNumber;
                patient.Address.City = patientModel.City;
                patient.Address.Country = patientModel.Country;
                patient.Address.PostCode = patientModel.PostCode;
                patient.Address.Street = patientModel.Street;
                patient.Name = patientModel.Name;
                patient.DateOfBirth = patientModel.DateOfBirth;
                patient.Pesel = patientModel.Pesel;
                patient.Surname = patientModel.Surname;

                _context.Update(patient);
                _context.SaveChanges();
                return true;

            }
            return false;
        }
    }
}
