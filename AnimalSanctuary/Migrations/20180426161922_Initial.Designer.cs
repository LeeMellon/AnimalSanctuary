using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AnimalSanctuary.Models;

namespace AnimalSanctuary.Migrations
{
    [DbContext(typeof(AnimalDbContext))]
    [Migration("20180426161922_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("AnimalSanctuary.Models.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HabitatType");

                    b.Property<bool>("MedicalEmergency");

                    b.Property<string>("Name");

                    b.Property<string>("Sex");

                    b.Property<string>("Species");

                    b.Property<int>("VetrinarianId");

                    b.HasKey("AnimalId");

                    b.HasIndex("VetrinarianId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("AnimalSanctuary.Models.Vetrinarian", b =>
                {
                    b.Property<int>("VetrinarianId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Specialty");

                    b.HasKey("VetrinarianId");

                    b.ToTable("Vetrinarians");
                });

            modelBuilder.Entity("AnimalSanctuary.Models.Animal", b =>
                {
                    b.HasOne("AnimalSanctuary.Models.Vetrinarian", "VetrinarianX")
                        .WithMany()
                        .HasForeignKey("VetrinarianId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
