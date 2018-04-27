using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuary.Models
{
    public class EFVetrinarianRepository : IVetrinarianRepository
    {
        AnimalDbContext db;
        public EFVetrinarianRepository()
        {
            db = new AnimalDbContext();
        }
        public EFVetrinarianRepository(AnimalDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Vetrinarian> Vetrinarians
        { get { return db.Vetrinarians; } }

        public Vetrinarian Save(Vetrinarian vetrinarian)
        {
            db.Vetrinarians.Add(vetrinarian);
            db.SaveChanges();
            return vetrinarian;
        }

        public Vetrinarian Edit(Vetrinarian vetrinarian)
        {
            db.Entry(vetrinarian).State = EntityState.Modified;
            db.SaveChanges();
            return vetrinarian;
        }

        public void Remove(Vetrinarian vetrinarian)
        {
            db.Vetrinarians.Remove(vetrinarian);
            db.SaveChanges();
        }

        public void ClearAll()
        {
            db.Vetrinarians.RemoveRange(db.Vetrinarians);
            db.SaveChanges();
        }
        
    }
}
