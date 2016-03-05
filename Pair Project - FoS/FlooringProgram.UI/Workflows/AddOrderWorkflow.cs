using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.Workflows
{
    internal class AddOrderWorkflow
    {
        internal void Execute()
        {
            var orderManager = new OrderManager();
            var productManager = new ProductManager();
            var stateTaxManager = new StateTaxManager();

            // Ask for date
            string date = OrderPrompts.AskForDate(MenuChoices.Add, AdminChoices.Empty);
            if (Prompts.CheckForCancel(date))
                return;

            // Set order number
            int orderNumber = orderManager.GenerateOrderNumber(date).Data;
            

            // Ask for customer name
            string customerName = OrderPrompts.AskForCustomerName(MenuChoices.Add, AdminChoices.Empty, string.Empty);
            if (Prompts.CheckForCancel(customerName))
                return;

            // Ask for state abbreviation
            string stateAbbreviation = OrderPrompts.AskForStateAbbreviation(MenuChoices.Add, AdminChoices.Empty, string.Empty);
            if (Prompts.CheckForCancel(stateAbbreviation))
                return;

            // Ask for product type
            string productType = OrderPrompts.AskForProductType(MenuChoices.Add, AdminChoices.Empty, string.Empty);
            if (Prompts.CheckForCancel(productType))
                return;

            // Ask for total area
            string totalArea = OrderPrompts.AskForTotalArea(MenuChoices.Add, string.Empty);
            if (Prompts.CheckForCancel(totalArea))
                return;

            // Create order
            var orderToAdd = new Order()
            {
                OrderNumber = orderNumber,
                OrderDate = date,
                CustomerName = customerName,
                StateAbbreviation = stateAbbreviation,
                ProductType = productType,
                TotalArea = decimal.Parse(totalArea),
                CostPerSquareFoot = productManager.LoadProduct(productType).Data.CostPerSquareFoot,
                LaborCostPerSquareFoot = productManager.LoadProduct(productType).Data.LaborCostPerSquareFoot,
                TaxRate = stateTaxManager.LoadStateTax(stateAbbreviation).Data.TaxRate
            };

            // Display order and ask to submit
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Add, AdminChoices.Empty);
            OrderDisplays.DrawOrder(orderToAdd);

            Console.Write("\n\n{0,30} {1} ", "Submit order?", ":");

            if (Prompts.Confirmation())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Response<Order> response = orderManager.AddOrder(orderToAdd);
                Console.Write("\n\n  {0}  Press any key to return. ", response.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n  Order cancelled.  Press any key to return. ");
            }
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}