using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Contracts;
using FlooringProgram.Models;

namespace FlooringProgram.Data.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        public void AddOrder(Order orderToAdd)
        {
            string date = orderToAdd.OrderDate;
            List<Order> allOrders = LoadAllOrders(date);

            allOrders.Add(orderToAdd);
            CsvWriter.WriteAllOrders(allOrders, date);
        }

        public void EditOrder(Order orderToEdit)
        {
            string date = orderToEdit.OrderDate;
            List<Order> allOrders = LoadAllOrders(date);

            allOrders = allOrders.Where(order => order.OrderNumber != orderToEdit.OrderNumber).ToList();
            allOrders.Add(orderToEdit);
            allOrders = allOrders.OrderBy(order => order.OrderNumber).ToList();
            CsvWriter.WriteAllOrders(allOrders, date);
        }

        public void RemoveOrder(int orderNumberToRemove, string date)
        {
            List<Order> allOrders = LoadAllOrders(date);

            allOrders = allOrders.Where(order => order.OrderNumber != orderNumberToRemove).ToList();
            CsvWriter.WriteAllOrders(allOrders, date);
        }

        public Order LoadOrder(int orderNumberToLoad, string date)
        {
            return LoadAllOrders(date).Single(order => order.OrderNumber == orderNumberToLoad);
        }

        public List<Order> LoadAllOrders(string date)
        {
            return CsvWriter.ReadAllOrders(date);
        }
    }
}