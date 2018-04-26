using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AnimalSanctuary.Models;

namespace AnimalSanctuary.Tests.Models
{
    class TestDbContext : AnimalDbContext
    {
        public override DbSet<Animal> Animals { get; set; }
        public override DbSet<Vetrinarian> Vetrinarians { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(@"Server=localhost;Port=8889;database=animaldb_test;uid=root;pwd=root;");
        }
    }
}
