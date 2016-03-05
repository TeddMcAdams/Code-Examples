using System.Collections.Generic;
using FlooringProgram.Models;

namespace FlooringProgram.Contracts
{
    public interface IOrderRepository
    {
        void AddOrder(Order orderToAdd);
        void EditOrder(Order orderToEdit);
        void RemoveOrder(int orderNumberToRemove, string date);
        Order LoadOrder(int orderNumberToLoad, string date);
        List<Order> LoadAllOrders(string date);
    }
}