using System.Collections.Generic;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    class ManagerTests
    {
        [Test]
        public void CanAddOrder()
        {
            var manager = new OrderManager();

            Response<Order> response = manager.AddOrder(new Order()
            {
                OrderDate = "01/01/2016",
                OrderNumber = 3,
                CustomerName = "Scoopy Bro"
            });

            Response<Order> actual = manager.LoadOrder(3, "01/01/2016");

            Assert.AreEqual("Scoopy Bro", actual.Data.CustomerName);
            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("Order added.", response.Message);
        }

        [Test]
        public void CanDeleteOrder()
        {
            var manager = new OrderManager();

            Response<Order> response = manager.RemoveOrder(1, "01/01/2016");

            Response<Order> actual = manager.LoadOrder(1, "01/01/2016");

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("Order removed.", response.Message);
            Assert.IsFalse(actual.Success);
        }

        [Test]
        public void CanEditOrder()
        {
            var manager = new OrderManager();

            Assert.AreEqual("Eileen", manager.LoadOrder(1, "01/03/2016").Data.CustomerName);

            Response<Order> response = manager.EditOrder(new Order()
            {
                OrderDate = "01/03/2016",
                OrderNumber = 1,
                CustomerName = "Edward"
            });

            Response<Order> actual = manager.LoadOrder(1, "01/03/2016");

            Assert.AreEqual(true, response.Success);
            Assert.AreEqual("Order edited.", response.Message);
            Assert.AreEqual("Edward", actual.Data.CustomerName);
        }

        [Test]
        public void CanLoadOrder()
        {
            var manager = new OrderManager();

            Response<Order> actual = manager.LoadOrder(3, "01/03/2016");
            
            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Order loaded.", actual.Message);
            Assert.AreEqual("Sally", actual.Data.CustomerName);
        }

        [Test]
        public void CanLoadAllOrders()
        {
            var manager = new OrderManager();

            Response<List<Order>> actual = manager.LoadAllOrders("01/01/2016");

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("All orders loaded.", actual.Message);
            Assert.AreEqual(2, actual.Data.Count);
        }

        [Test]
        public void CanLoadProduct()
        {
            var manager = new ProductManager();

            Response<Product> actual = manager.LoadProduct("Wood");

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("Product loaded.", actual.Message);
            Assert.AreEqual(5.15M, actual.Data.CostPerSquareFoot);
            Assert.AreEqual(4.75M, actual.Data.LaborCostPerSquareFoot);
        }

        [Test]
        public void CanLoadAllProducts()
        {
            var manager = new ProductManager();

            Response<List<Product>> actual = manager.LoadAllProducts();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("All products loaded.", actual.Message);
            Assert.AreEqual(4, actual.Data.Count);
        }

        [Test]
        public void CanLoadStateTax()
        {
            var manager = new StateTaxManager();

            Response<StateTax> actual = manager.LoadStateTax("IN");

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("State loaded.", actual.Message);
            Assert.AreEqual(6.00M, actual.Data.TaxRate);
        }

        [Test]
        public void CanLoadAllStateTax()
        {
            var manager = new StateTaxManager();

            Response<List<StateTax>> actual = manager.LoadAllStateTax();

            Assert.IsTrue(actual.Success);
            Assert.AreEqual("All states loaded.", actual.Message);
            Assert.AreEqual(4, actual.Data.Count);
        }

        [Test]
        public void CanGenerateOrderNumber()
        {
            var manager = new OrderManager();

            var actual = manager.GenerateOrderNumber("01/02/2016");

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(3, actual.Data);
        }
    }
}