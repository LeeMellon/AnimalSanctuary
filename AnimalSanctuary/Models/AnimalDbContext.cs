using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalSanctuary.Models
{
    public class AnimalDbContext : DbContext
    {
        public AnimalDbContext()
        {

        }

        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Vetrinarian> Vetrinarians { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=8889;database=AnimalDb;uid=root;pwd=root;");
        }

        public AnimalDbContext(DbContextOptions<AnimalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
