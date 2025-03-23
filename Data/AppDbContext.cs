using DriverLicence.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;



namespace DriverLicence.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<LicenceClasses>LicenceClasses { get; set; }
        public DbSet<TestClasses> TestClasses { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Certifications>Certifications { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<TestAppointments> TestAppointments { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<Licences>Licences { get; set; }
        public DbSet<Requests> Requests { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-----one-one person-user--------
            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithOne(u => u.Person)
                .HasForeignKey<User>(u => u.NationalNumber)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.NationalNumber)
                .IsUnique();
            //-------------------------------------------------------

            //----- one-many user-certfications----
            modelBuilder.Entity<Certifications>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certifications)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            //-----------------------------------------------------------

            //----- one-many user-Licences------------------------------
            modelBuilder.Entity<Licences>()
                .HasOne(l => l.User)
                .WithMany(u => u.Licences)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            //----------------------------------------------------------


            //-----one-one licence-LicenceCLass-------------------------
            modelBuilder.Entity<LicenceClasses>()
                .HasOne(l => l.Licence)
                .WithOne(c => c.LicenceClass)
                .HasForeignKey<Licences>(l => l.LicenceClassId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Licences>()
                .HasIndex(l => l.LicenceClassId)
                .IsUnique();
            //----------------------------------------------------------


            //----- one-one Driver-user--------------------------------
            modelBuilder.Entity<User>()
                .HasOne(u => u.Driver)
                .WithOne(d => d.User)
                .HasForeignKey<Drivers>(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Drivers>()
                .HasIndex(d => d.UserId)
                .IsUnique();
            //----------------------------------------------------------


            //----- one-one Driver-Licence--------------------------------
            modelBuilder.Entity<Licences>()
                .HasOne(l=>l.Driver)
                .WithOne(d => d.Licence)
                .HasForeignKey<Drivers>(d => d.LicenceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Drivers>()
                .HasIndex(d => d.LicenceId)
                .IsUnique();
            //----------------------------------------------------------


            //------one-many user-requests------------------------------
            modelBuilder.Entity<Requests>()
                .HasOne(r => r.User)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            //------one-many service-requests------------------------------
            modelBuilder.Entity<Requests>()
                .HasOne(r => r.Services)
                .WithMany(s=>s.Requests)
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            //------one-one testAppointment-request-------------------------
            modelBuilder.Entity<Requests>()
                 .HasOne(r=>r.TestAppointment)
                 .WithOne(t=>t.Request)
                 .HasForeignKey<TestAppointments>(t=>t.RequestNumber)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TestAppointments>()
                .HasIndex(t=>t.RequestNumber)
                .IsUnique();

            //---------one-many LicenceClass-TestAppointments-------------
            modelBuilder.Entity<TestAppointments>()
                .HasOne(t=>t.LicenceClass)
                .WithMany(l=>l.TestAppointments)
                .HasForeignKey(t=>t.LicenceClassId)
                .OnDelete(DeleteBehavior.NoAction);

            //---------one-many TestAppointment-Tests------------
            modelBuilder.Entity<Tests>()
                .HasOne(t => t.TestAppointment)
                .WithMany(t=>t.Tests)
                .HasForeignKey(t => t.TestAppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            //------one-one test-testClass-------------------------
            modelBuilder.Entity<TestClasses>()
                 .HasOne(t=>t.Test)
                 .WithOne(t=>t.TestClass)
                 .HasForeignKey<Tests>(t => t.TestClassId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Tests>()
                .HasIndex(t => t.TestClassId)
                .IsUnique();
        }

    }

}

