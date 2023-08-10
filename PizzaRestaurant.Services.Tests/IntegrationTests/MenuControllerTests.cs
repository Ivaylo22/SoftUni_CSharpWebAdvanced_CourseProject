﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaRestaurant.Services.Data.Interfaces;
using PizzaRestaurant.Web.Controllers;
using PizzaRestaurant.Web.ViewModels.Menu;
using PizzaRestaurant.Web.ViewModels.Pizza;

namespace PizzaRestaurant.Services.Tests.IntegrationTests
{
    [TestFixture]
    public class MenuControllerTests
    {
        private MenuController controller;
        private Mock<IMenuService> mockMenuService;
        private Mock<IPizzaService> mockPizzaService;

        [SetUp]
        public void Setup()
        {
            mockMenuService = new Mock<IMenuService>();
            mockPizzaService = new Mock<IPizzaService>();

            controller = new MenuController(mockMenuService.Object, mockPizzaService.Object);
        }

        [Test]
        public async Task All_ReturnsViewWithMenus()
        {
            mockMenuService
                .Setup(service => service.GetAllMenusAsync())
                .ReturnsAsync(new List<MenuViewModel>());

            var result = await controller.All() as ViewResult;

            Assert.NotNull(result);
            Assert.AreEqual(typeof(List<MenuViewModel>), result.Model.GetType());
        }
        [Test]
        public async Task Pizzas_ReturnsViewWithPizzas()
        {
            // Arrange
            mockPizzaService.Setup(service => service.GetPizzasByMenuIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(new List<PizzasForMenuViewModel>());

            // Act
            var result = await controller.Pizzas(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(typeof(List<PizzasForMenuViewModel>), result.Model.GetType());
        }
    }
}
