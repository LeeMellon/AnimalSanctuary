using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuary.Controllers
{
    public class AnimalsController : Controller
    {
        private IAnimalRepository AnimalRepo;

        public AnimalsController(IAnimalRepository repo = null)
        {
            if (repo == null)
            {
                this.AnimalRepo = new EFAnimalRepository();
            }
            else
            {
                this.AnimalRepo = repo;
            }
        }

        public IActionResult Index()
        {
            return View(AnimalRepo.Animals.ToList());
        }

        public IActionResult Details(int id)
        {
            return View(AnimalRepo.Animals.FirstOrDefault(x => x.AnimalId == id));
        }

        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Animal animal)
        {
            AnimalRepo.Save(animal);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(AnimalRepo.Animals.FirstOrDefault(x => x.AnimalId == id));
        }

        [HttpPost]
        public IActionResult Edit(Animal animal)
        {
            AnimalRepo.Edit(animal);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(AnimalRepo.Animals.FirstOrDefault(x => x.AnimalId == id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            AnimalRepo.Remove(AnimalRepo.Animals.FirstOrDefault(x => x.AnimalId == id));
            return RedirectToAction("Index");
        }
    }
}
