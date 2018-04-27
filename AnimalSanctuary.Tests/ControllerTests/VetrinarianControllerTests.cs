using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using AnimalSanctuary.Controllers;
using AnimalSanctuary.Models;
using Moq;
using AnimalSanctuary.Tests.Models;

namespace AnimalSanctuary.Tests.ControllerTests
{   
    [TestClass]
    public class VetrinarianControllerTests : IDisposable
    {
        private Mock<IVetrinarianRepository> mock = new Mock<IVetrinarianRepository>();
        EFVetrinarianRepository db = new EFVetrinarianRepository(new TestDbContext());
        private void DbSetup()
        {
            mock.Setup(m => m.Vetrinarians).Returns(new Vetrinarian[]
            {
                new Vetrinarian {VetrinarianId = 1, Name = "Doug", Specialty="BeingNice"},
                new Vetrinarian {VetrinarianId = 2, Name = "Claire", Specialty="Horses"}
            }.AsQueryable());
        }

        public void Dispose()
        {
           db.ClearAll();
        }
        
        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult()
       {
            //Arrange
            DbSetup();
            VetrinariansController controller = new VetrinariansController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));

        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            //Arrange
            DbSetup();
            ViewResult indexView = new VetrinariansController(mock.Object).Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Vetrinarian>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsVets_Collection()
        {
            //Arrange
            DbSetup();
            VetrinariansController controller = new VetrinariansController(mock.Object);
            Vetrinarian vetrinarian = new Vetrinarian { VetrinarianId = 2, Name = "Claire", Specialty = "Horses" };

            //Act
            ViewResult indexView = controller.Index() as ViewResult;
            List<Vetrinarian> collection = indexView.ViewData.Model as List<Vetrinarian>;

            //Assert
            CollectionAssert.Contains(collection, vetrinarian);
        }
        
        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            //Arrange
            DbSetup();
            Vetrinarian vetrinarian = new Vetrinarian { VetrinarianId = 1, Name = "Doug", Specialty = "BeingNice" };
            VetrinariansController controller = new VetrinariansController(mock.Object);

            //Act
            var resultView = controller.Create(vetrinarian);

            //Assert
            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            //Arrange
            DbSetup();
            Vetrinarian vetrinarian = new Vetrinarian { VetrinarianId = 2, Name = "Claire", Specialty = "Horses" };
            VetrinariansController controller = new VetrinariansController(mock.Object);

            //Act
            var resultView = controller.Details(vetrinarian.VetrinarianId) as ViewResult;
            var model = resultView.ViewData.Model as Vetrinarian;

            //Assert 
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Vetrinarian));

        }

        [TestMethod]
        public void DB_CreateNewEntries_Collection()
        {
            //Arrange
            VetrinariansController controller = new VetrinariansController(db);
            Vetrinarian testVet = new Vetrinarian
            {
                Name = "Beth"
            };

            //Act
            controller.Create(testVet);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Vetrinarian>;

            //Assert
            CollectionAssert.Contains(collection, testVet);
        }
    }
}
