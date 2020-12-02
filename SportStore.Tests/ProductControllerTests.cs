using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using NUnit.Framework;
using SportStore.Controllers;
using SportStore.Interfaces;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductID=1, Name="P1"},
            new Product {ProductID=2, Name="P2"},
            new Product {ProductID=3,Name="P3"},
            new Product {ProductID=4,Name="P4"},
            new Product {ProductID=5,Name="P5"}
            }).AsQueryable<Product>());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //ACT
            ProductListViewModel result =
                controller.List(null, 2).ViewData.Model as  ProductListViewModel;

            //ASSERT
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.AreEqual("P4", prodArray[0].Name);
            Assert.AreEqual("P5", prodArray[1].Name);
        }
    }
}
