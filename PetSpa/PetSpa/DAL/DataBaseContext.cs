using PetSpa.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace PetSpa.DAL
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        #region Properties
        public DbSet<Service> Services { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetDetails> PetsDetails { get; set; }
        #endregion



        //Vamos a crear un índice para la tabla Countries
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<Service>().HasIndex(s => s.Name).IsUnique();
             modelBuilder.Entity<Pet>().HasIndex(p => p.Id).IsUnique();
             modelBuilder.Entity<PetDetails>().HasIndex(d => d.Id).IsUnique();
           
        }
    }
}
