using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ZamtelWebAPI.Models
{
    public partial class ZamtelContext : DbContext
    {
        public ZamtelContext()
        {
        }

        public ZamtelContext(DbContextOptions<ZamtelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<BackOfficeAgent> BackOfficeAgents { get; set; }
        public virtual DbSet<Corporate> Corporates { get; set; }
        public virtual DbSet<CorporateId> CorporateIds { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<NextOfKin> NextOfKins { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Representative> Representatives { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SimRegistrationDetail> SimRegistrationDetails { get; set; }
        public virtual DbSet<Supervisor> Supervisors { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ZamtelDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agent");

                entity.Property(e => e.AgentCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentContractFormUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AlternativeMobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.DeviceOwnership)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdBackUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdFrontUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PotrailUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AuditLog");

                entity.Property(e => e.Action)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeLogged).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestObject).IsUnicode(false);

                entity.Property(e => e.ResponseObject).IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BackOfficeAgent>(entity =>
            {
                entity.ToTable("BackOfficeAgent");

                entity.Property(e => e.AgentContractFormUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AlternativeMobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdBackUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdFrontUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.PotrailUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Corporate>(entity =>
            {
                entity.ToTable("Corporate");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AlternativeMobileNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BatchUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CertificateUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCoinumber)
                    .HasColumnName("CompanyCOINumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorporateIdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CorporateMobileNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IsCompanyGo).HasColumnName("IsCompanyGO");

                entity.Property(e => e.LetterUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");
            });

            modelBuilder.Entity<CorporateId>(entity =>
            {
                entity.ToTable("CorporateId");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AlternativeMobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Chiefdom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Landmark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastDayOfStay).HasColumnType("date");

                entity.Property(e => e.LastDayOfStaySupportingDocumentUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdBackUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdFrontUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Occupation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlotNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PortraitUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProofOfStayUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Road)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UnitNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Village)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.ToTable("Nationality");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NextOfKin>(entity =>
            {
                entity.ToTable("NextOfKin");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Representative>(entity =>
            {
                entity.ToTable("Representative");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvitingEntity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdBackUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdFrontUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NationalIdNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PortraitUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SimRegistrationDetail>(entity =>
            {
                entity.ToTable("SimRegistrationDetail");

                entity.Property(e => e.CategoryType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.RegistrationType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SimSerialNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.ToTable("Supervisor");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.ToTable("Town");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
