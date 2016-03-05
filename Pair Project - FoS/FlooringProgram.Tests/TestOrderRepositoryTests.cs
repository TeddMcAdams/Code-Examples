using System.Linq;
using FlooringProgram.Contracts;
using FlooringProgram.Data.Repositories;
using FlooringProgram.Models;
using NUnit.Framework;

namespace FlooringProgram.Tests
{
    [TestFixture]
    class TestOrderRepositoryTests
    {
        [Test]
        public void CanAddNewOrder()
        {
            IOrderRepository repo = new TestOrderRepository();

            repo.AddOrder(new Order() {OrderNumber = 3, OrderDate = "01/01/2016", CustomerName = "Ted"});

            int actual = repo.LoadAllOrders("01/01/2016").Count;

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void CanEditAnOrder()
        {
            IOrderRepository repo = new TestOrderRepository();

            repo.EditOrder(new Order() { OrderNumber = 3, OrderDate = "01/03/2016", CustomerName = "Bean" });

            Order actual = repo.LoadOrder(3, "01/03/2016");

            Assert.AreEqual("Bean", actual.CustomerName);
        }

        [Test]
        public void CanRemoveAnOrder()
        {
            IOrderRepository repo = new TestOrderRepository();

            repo.RemoveOrder(2, "01/03/2016");

            Assert.AreEqual(false, repo.LoadAllOrders("01/03/2016").Select(order => order.OrderNumber).Contains(2));
            Assert.AreEqual(true, repo.LoadAllOrders("01/03/2016").Select(order => order.OrderNumber).Contains(1));
        }

        [Test]
        public void CanLoadAnOrder()
        {
            IOrderRepository repo = new TestOrderRepository();

            Order actual = repo.LoadOrder(2, "01/02/2016");

            Assert.AreEqual("Bob Balla", actual.CustomerName);
        }

        [Test]
        public void CanLoadAllOrders()
        {
            IOrderRepository repo = new TestOrderRepository();

            int actual = repo.LoadAllOrders("01/02/2016").Count;

            Assert.AreEqual(2, actual);
        }
    }
}