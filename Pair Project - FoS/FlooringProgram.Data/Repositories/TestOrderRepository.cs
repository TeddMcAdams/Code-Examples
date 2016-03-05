using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Contracts;
using FlooringProgram.Models;

namespace FlooringProgram.Data.Repositories
{
    public class TestOrderRepository : IOrderRepository
    {
        public void AddOrder(Order orderToAdd)
        {
            string date = orderToAdd.OrderDate;
            List<Order> allOrders = LoadAllOrders(date);

            allOrders.Add(orderToAdd);
            WriteAllOrders(allOrders, date);
        }

        public void EditOrder(Order orderToEdit)
        {
            string date = orderToEdit.OrderDate;
            List<Order> allOrders = LoadAllOrders(date);

            allOrders = allOrders.Where(order => order.OrderNumber != orderToEdit.OrderNumber).ToList();
            allOrders.Add(orderToEdit);
            allOrders = allOrders.OrderBy(order => order.OrderNumber).ToList();
            WriteAllOrders(allOrders, date);
        }

        public void RemoveOrder(int orderNumberToRemove, string date)
        {
            List<Order> orders = LoadAllOrders(date);

            orders = orders.Where(order => order.OrderNumber != orderNumberToRemove).ToList();
            WriteAllOrders(orders, date);
        }

        public Order LoadOrder(int orderNumberToLoad, string date)
        {
            return LoadAllOrders(date).Single(order => order.OrderNumber == orderNumberToLoad);
        }

        public List<Order> LoadAllOrders(string date)
        {
            if (!MockDatabase.TestOrderRepositoryData.ContainsKey(date))
                MockDatabase.TestOrderRepositoryData.Add(date, new List<Order>());
            return MockDatabase.TestOrderRepositoryData[date];
        }

        private void WriteAllOrders(List<Order> allOrders, string date)
        {
            var tempAllOrders = new List<Order>(allOrders);

            if (MockDatabase.TestOrderRepositoryData.ContainsKey(date))
            {
                MockDatabase.TestOrderRepositoryData[date].Clear();
                foreach (Order order in tempAllOrders)
                {
                    MockDatabase.TestOrderRepositoryData[date].Add(order);
                }
            }
            else
            {
                MockDatabase.TestOrderRepositoryData.Add(date, tempAllOrders); 
                
            }
        }
    }
}