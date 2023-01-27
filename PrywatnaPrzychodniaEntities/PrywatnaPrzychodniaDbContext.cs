using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrywatnaPrzychodniaEntities.Entities;

namespace PrywatnaPrzychodniaEntities
{
    public class PrywatnaPrzychodniaDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public PrywatnaPrzychodniaDbContext(DbContextOptions<PrywatnaPrzychodniaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Visit>().HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);*/

        }
    }
}
