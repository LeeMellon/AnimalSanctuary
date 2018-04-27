using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnimalSanctuary.Controllers
{
    public class VetrinariansController : Controller
    {
        private IVetrinarianRepository VetrinarianRepo;

        public VetrinariansController(IVetrinarianRepository repo = null)
        {
            if (repo == null)
            {
                this.VetrinarianRepo = new EFVetrinarianRepository();
            }
            else
            {
                this.VetrinarianRepo = repo;
            }
        }
        
        public IActionResult Index()
        {
            return View(VetrinarianRepo.Vetrinarians.ToList());
        }

        public IActionResult Details(int id)
        {
            return View(VetrinarianRepo.Vetrinarians.FirstOrDefault(x => x.VetrinarianId == id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vetrinarian vetrinarian)
        {
            VetrinarianRepo.Save(vetrinarian);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(VetrinarianRepo.Vetrinarians.FirstOrDefault(x => x.VetrinarianId == id));
        }

        [HttpPost]
        public IActionResult Edit(Vetrinarian vetrinarian)
        {
            VetrinarianRepo.Edit(vetrinarian);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View(VetrinarianRepo.Vetrinarians.FirstOrDefault(x => x.VetrinarianId == id));
        }

        [HttpPost, ActionName("Delete")]
            public IActionResult DeleteConfirmed(int id)
        {
            VetrinarianRepo.Remove(VetrinarianRepo.Vetrinarians.FirstOrDefault(x => x.VetrinarianId == id));
            return RedirectToAction("Index");
        }

    }
}
