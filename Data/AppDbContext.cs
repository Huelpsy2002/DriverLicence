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
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-----one-one relationship between person-user--------
            modelBuilder.Entity<Person>()
                .HasOne(p => p.User)
                .WithOne(u => u.Person)
                .HasForeignKey<User>(u => u.NationalNumber)
                .OnDelete(DeleteBehavior.NoAction);

            //make sure every forignkey record is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.NationalNumber)
                .IsUnique();
            //-------------------------------------------------------





          
        }

    }

}

