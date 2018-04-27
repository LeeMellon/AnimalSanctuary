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
    public class AnimalsControllerTest
    {
        private Mock<IAnimalRepository> mock = new Mock<IAnimalRepository>();
        EFAnimalRepository db = new EFAnimalRepository(new TestDbContext());
        private void DbSetup()
        {
            mock.Setup(m => m.Animals).Returns(new Animal[]
                {
                    new Animal { AnimalId = 1, Species = "bird", VetrinarianId = 0 },
                    new Animal { AnimalId = 2, Species = "fish", VetrinarianId = 0 }
                }.AsQueryable());
        }

        public void Dispose()
        {
            db.ClearAll();
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult() // Confirms route returns view
        {
            //Arrange
            DbSetup();
            AnimalsController controller = new AnimalsController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List() // Confirms model as list of items
        {
            // Arrange
            DbSetup();
            ViewResult indexView = new AnimalsController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Animal>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsAnimals_Collection()
        {
            DbSetup();
            AnimalsController controller = new AnimalsController(mock.Object);
            Animal animal = new Animal { AnimalId = 1, Species = "bird", VetrinarianId = 0 };

            ViewResult indexView = controller.Index() as ViewResult;
            List<Animal> collection = indexView.ViewData.Model as List<Animal>;

            CollectionAssert.Contains(collection, animal);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            // Arrange
            Animal animal = new Animal { AnimalId = 1, Species = "bird", VetrinarianId = 0 };

            DbSetup();
            AnimalsController controller = new AnimalsController(mock.Object);

            // Act
            var resultView = controller.Create(animal);


            // Assert
            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));

        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            // Arrange
            Animal animal = new Animal { AnimalId = 1, Species = "bird", VetrinarianId = 0 };

            DbSetup();
            AnimalsController controller = new AnimalsController(mock.Object);

            // Act
            var resultView = controller.Details(animal.AnimalId) as ViewResult;
            var model = resultView.ViewData.Model as Animal;

            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Animal));
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            //Arrange
            AnimalsController controller = new AnimalsController(db);
            Animal animal = new Animal
            {
                Name = "Kreiger"
            };

            //Act
            controller.Create(animal);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Animal>;

            //Assert
            CollectionAssert.Contains(collection, animal);
        }
    }
}
