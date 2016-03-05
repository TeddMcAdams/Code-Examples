using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Contracts;
using FlooringProgram.Data;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public class OrderManager
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager()
        {
            _orderRepository = RepositoryFactory.GetOrderRepository();
        }

        public Response<Order> AddOrder(Order orderToAdd)
        {
            var response = new Response<Order>();

            try
            {
                _orderRepository.AddOrder(orderToAdd);
                response.Success = true;
                response.Message = "Order added.";
                response.Data = _orderRepository.LoadOrder(orderToAdd.OrderNumber, orderToAdd.OrderDate);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to add order.";
            }
            return response;
        }

        public Response<Order> RemoveOrder(int orderNumberToRemove, string date)
        {
            var response = new Response<Order>();

            try
            {
                _orderRepository.RemoveOrder(orderNumberToRemove, date);
                response.Success = true;
                response.Message = "Order removed.";
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to remove order.";
            }
            return response;
        }

        public Response<Order> EditOrder(Order orderToEdit)
        {
            var response = new Response<Order>();

            try
            {
                _orderRepository.EditOrder(orderToEdit);
                response.Success = true;
                response.Message = "Order edited.";
                response.Data = _orderRepository.LoadOrder(orderToEdit.OrderNumber, orderToEdit.OrderDate);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to edit order.";
            }
            return response;
        }

        public Response<Order> LoadOrder(int orderNumberToLoad, string date)
        {
            var response = new Response<Order>();

            try
            {
                response.Success = true;
                response.Message = "Order loaded.";
                response.Data = _orderRepository.LoadOrder(orderNumberToLoad, date);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to load order.";
            }
            return response;
        } 

        public Response<List<Order>> LoadAllOrders(string date)
        {
            var response = new Response<List<Order>>();

            try
            {
                response.Success = true;
                response.Message = "All orders loaded.";
                response.Data = _orderRepository.LoadAllOrders(date);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to load orders.";
            }
            return response;
        }

        public Response<int> GenerateOrderNumber(string date)
        {
            var response = new Response<int>();
            
            try
            {
                response.Success = true;
                response.Message = "Order number generated.";
                response.Data = _orderRepository.LoadAllOrders(date).OrderByDescending(order => order.OrderNumber).First().OrderNumber + 1;
                
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to generate order number.";
                response.Data = 1;
            }
            return response;
        }

        public bool CheckIfDateHasOrders(string date)
        {
            try
            {
                return (_orderRepository.LoadAllOrders(date).Count > 0);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                return false;
            }
        }
    }
}