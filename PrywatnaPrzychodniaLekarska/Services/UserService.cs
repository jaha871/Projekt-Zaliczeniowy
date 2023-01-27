using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PrywatnaPrzychodniaEntities;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Contracts;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Services
{
    public class UserService : ConnectionService, ICrudScheme<UserService>
    {
        private IPasswordHasher<User> _passwordHasher;
        public UserService(PrywatnaPrzychodniaDbContext context, IPasswordHasher<User> passwordHasher) : base(context)
        {
            _passwordHasher = passwordHasher;
        }

        public bool Create<TV>(TV userModel)
        {
            var userDto = userModel as UserModel;
            var user = _context.Users.Any(x => x.Email == userDto.Email);
            if (!user)
            {
                var createUser = new User
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Role = userDto.Role
                };

                var hashedPassword = _passwordHasher.HashPassword(createUser, userDto.Password);
                createUser.Password = hashedPassword;

                _context.Users.Add(createUser);
                _context.SaveChanges();

                return true;
            }

            return false;

        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TV> Get<TV>()
        {
            throw new NotImplementedException();
        }

        public bool Update<TV>(int id, TV model)
        {
            throw new NotImplementedException();
        }
    }
}
