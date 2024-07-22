using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsertion
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=AdventureWorksDW2022;Trusted_Connection=True;TrustServerCertificate=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProspectiveBuyer>(entity =>
            {
                entity.HasKey(e => e.ProspectiveBuyerKey);

                entity.Property(e => e.ProspectAlternateKey).HasMaxLength(15);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.MiddleName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.BirthDate);
                entity.Property(e => e.MaritalStatus).HasMaxLength(1).IsFixedLength(true);
                entity.Property(e => e.Gender).HasMaxLength(1);
                entity.Property(e => e.EmailAddress).HasMaxLength(50);
                entity.Property(e => e.YearlyIncome).HasColumnType("money");
                entity.Property(e => e.TotalChildren);
                entity.Property(e => e.NumberChildrenAtHome);
                entity.Property(e => e.Education).HasMaxLength(40);
                entity.Property(e => e.Occupation).HasMaxLength(100);
                entity.Property(e => e.HouseOwnerFlag).HasMaxLength(1).IsFixedLength(true);
                entity.Property(e => e.NumberCarsOwned);
                entity.Property(e => e.AddressLine1).HasMaxLength(120);
                entity.Property(e => e.AddressLine2).HasMaxLength(120);
                entity.Property(e => e.City).HasMaxLength(30);
                entity.Property(e => e.StateProvinceCode).HasMaxLength(3);
                entity.Property(e => e.PostalCode).HasMaxLength(15);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Salutation).HasMaxLength(8);
                entity.Property(e => e.Unknown);
            });

        }

        public DbSet<ProspectiveBuyer> ProspectiveBuyer { get; set; }

    }
        public class ProspectiveBuyer
        {
            public int ProspectiveBuyerKey { get; set; }
            public string ProspectAlternateKey { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public DateTime? BirthDate { get; set; }
            public char? MaritalStatus { get; set; }
            public string Gender { get; set; }
            public string EmailAddress { get; set; }
            public decimal? YearlyIncome { get; set; }
            public byte? TotalChildren { get; set; }
            public byte? NumberChildrenAtHome { get; set; }
            public string Education { get; set; }
            public string Occupation { get; set; }
            public char? HouseOwnerFlag { get; set; }
            public byte? NumberCarsOwned { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string StateProvinceCode { get; set; }
            public string PostalCode { get; set; }
            public string Phone { get; set; }
            public string Salutation { get; set; }
            public int? Unknown { get; set; }
        }
}
