using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PrywatnaPrzychodniaEntities;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Services;

namespace PrywatnaPrzychodniaLekarska
{
    public class PrzychodniaLekarskaSeeder : ConnectionService
    {
        private IPasswordHasher<User> _passwordHasher;

        public PrzychodniaLekarskaSeeder(PrywatnaPrzychodniaDbContext context, IPasswordHasher<User> passwordHasher) : base(context)
        {
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Doctors.Any())
                {
                    var doctor = SeedDoctor();
                    _context.Doctors.Add(doctor);
                    _context.SaveChanges();
                }

                if (!_context.Patients.Any())
                {
                    var patient = SeedPatient();
                    _context.Patients.Add(patient);
                    _context.SaveChanges();
                }

                if (!_context.Users.Any())
                {
                    var user = SeedUser();
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    var admin = SeedAdmin();
                    _context.Users.Add(admin);
                    _context.SaveChanges();
                }

                if (!_context.Visits.Any())
                {
                    var visit = SeedVisit();
                    _context.Visits.Add(visit);
                    _context.SaveChanges();
                }
            }
        }

        private Patient SeedPatient()
        {
            var patient = new Patient
            {
                Name = "Tomasz",
                DateOfBirth = new DateTime(1990, 05, 20),
                Pesel = 824512447,
                Surname = "Kowalski",
                Address = new Address
                {
                    HouseNumber = "34d",
                    Country = "Poland",
                    Street = "Polna",
                    PostCode = "33112",
                    City = "Katowice"
                }
            };

            return patient;
        }

        public Doctor SeedDoctor()
        {
            var doctor = new Doctor
            {
                Address = new Address
                {
                    HouseNumber = "29",
                    Country = "Poland",
                    Street = "Krakowska",
                    PostCode = "33-100",
                    City = "Krakow"
                },
                Name = "Tomasz",
                Specialization = "Dentysta",
                Surname = "Nowacki"
            };

            return doctor;
        }

        public User SeedUser()
        {
            var createUser = new User
            {
                Email = "user@gmail.com",
                FirstName = "Beata",
                LastName = "Milinska",
                Role = "user",
                Password = "user"
            };

            var hashedPassword = _passwordHasher.HashPassword(createUser, createUser.Password);
            createUser.Password = hashedPassword;

            return createUser;
        }

        public User SeedAdmin()
        {
            var createUser = new User
            {
                Email = "admin@gmail.com",
                FirstName = "Beata",
                LastName = "Milinska",
                Role = "admin",
                Password = "admin"
            };

            var hashedPassword = _passwordHasher.HashPassword(createUser, createUser.Password);
            createUser.Password = hashedPassword;

            return createUser;
        }

        public Visit SeedVisit()
        {
            var doctor = _context.Doctors.FirstOrDefault();
            var patient = _context.Patients.FirstOrDefault();
            var visit = new Visit
            {
                PatientId = patient.Id,
                DateOfVisit = DateTime.Now,
                DoctorId = doctor.Id,
                Price = new Price
                {
                    Description = "Wizyta lekarska",
                    Discount = 20,
                    VisitPrice = 300
                }
            };

            return visit;
        }
    }
}
