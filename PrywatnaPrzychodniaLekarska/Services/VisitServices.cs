using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PrywatnaPrzychodniaEntities;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Contracts;
using PrywatnaPrzychodniaLekarska.Exeptions;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Services
{
    public class VisitServices : ConnectionService, ICrudScheme<VisitServices>
    {
        private IMapper _mapper;

        public VisitServices(PrywatnaPrzychodniaDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public bool Create<TV>(TV userModel)
        {
            var visitDto = userModel as VisitModel;
            if (visitDto.Discount < 10)
            {
                throw new BadRequestException("Cena jest nie jest poprawna. Musi być wieksza od 10zł"); ;
            }
            var visit = new Visit
            {
                PatientId = visitDto.PatientId,
                DateOfVisit = visitDto.DateOfVisit,
                DoctorId = visitDto.DoctorId,
                Price = new Price
                {
                    Description = visitDto.Description,
                    Discount = visitDto.Discount,
                    VisitPrice = visitDto.VisitPrice
                }
            };

            _context.Visits.Add(visit);
            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var visit = _context.Visits.Where(x => x.Id == id)?.FirstOrDefault();
            if (visit != null)
            {
                _context.Remove(visit);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<TV> Get<TV>()
        {
            var visits = _context.Visits
                .Include(x => x.Price)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.Patient.Address)
                .ToList();

            return _mapper.Map<List<TV>>(visits);
        }

        public bool Update<TV>(int id, TV model)
        {
            var visitDto = model as VisitModel;
            var visit = _context.Visits.Include(x => x.Price).Where(x => x.Id == id)?.FirstOrDefault();
            if (visit != null)
            {
                visit.PatientId = visitDto.PatientId;
                visit.DoctorId = visitDto.DoctorId;
                visit.Price = new Price
                {
                    Description = visitDto.Description,
                    Discount = visitDto.Discount,
                    VisitPrice = visitDto.VisitPrice
                };
                _context.Update(visit);

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public TV GetById<TV>(int id)
        {
            var value = _context.Visits
                .Include(x => x.Price)
                .Include(x => x.Patient)
                .Include(x => x.Doctor)
                .Include(x => x.Patient.Address)
                .FirstOrDefault(x => x.Id == id);
            return _mapper.Map<TV>(value);
        }
    }
}
