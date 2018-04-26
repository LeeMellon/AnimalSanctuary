using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace AnimalSanctuary.Models
{
    public class EFAnimalRepository : IAnimalRepository
    {
        AnimalDbContext db = new AnimalDbContext();

        public IQueryable<Animal> Animals
        { get { return db.Animals; } }

        public Animal Edit(Animal animal)
        {
            db.Entry(animal).State = EntityState.Modified;
            db.SaveChanges();
            return animal;
        }

        public void Remove(Animal animal)
        {
            db.Animals.Remove(animal);
            db.SaveChanges();
        }

        public Animal Save(Animal animal)
        {
            db.Animals.Add(animal);
            db.SaveChanges();
            return animal;
        }
    }
}
