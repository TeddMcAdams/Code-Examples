using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models.Enums;
using FlooringProgram.Models;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.Workflows
{
    internal class EditOrderWorkflow
    {
        internal void Execute()
        {
            var orderManager = new OrderManager();
            var productManager = new ProductManager();
            var stateTaxManager = new StateTaxManager();
            string inputDate;
            string inputOrderNumber;

            do
            {
                inputDate = OrderPrompts.AskForDate(MenuChoices.Edit, AdminChoices.Empty);
                if (Prompts.CheckForCancel(inputDate))
                    return;

                if (orderManager.CheckIfDateHasOrders(inputDate))
                    break;

                Console.Write("\n  No orders found for {0}.\n\n  Lookup another date? ", inputDate);
                if (!Prompts.Confirmation())
                    return;
            } while (true);

            Response<List<Order>> loadAllResponse = orderManager.LoadAllOrders(inputDate);

            if (loadAllResponse.Success)
            {
                inputOrderNumber = OrderPrompts.AskForOrderNumber(MenuChoices.Edit, AdminChoices.Empty, loadAllResponse.Data, inputDate);
                if (Prompts.CheckForCancel(inputOrderNumber))
                    return;
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Edit, AdminChoices.Empty);
                OrderDisplays.DrawOrder(loadAllResponse.Data.Single(order => order.OrderNumber == int.Parse(inputOrderNumber)));
                Console.Write("\n\n{0,30} {1} ", "Edit this order?", ":");
                if (!Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Edit order cancelled.  Press any key to return. ");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return;
                }
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", loadAllResponse.Message);
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Displays.DrawTitle(MenuChoices.Edit, AdminChoices.Empty);

            var loadResponse = orderManager.LoadOrder(int.Parse(inputOrderNumber), inputDate);

            if (loadResponse.Success)
            {
                // Ask to edit customer name
                string editedCustomerName = OrderPrompts.AskForCustomerName(MenuChoices.Edit, AdminChoices.Empty,
                    loadResponse.Data.CustomerName);
                if (Prompts.CheckForCancel(editedCustomerName))
                    return;
                if (Prompts.CheckForNotEmpty(editedCustomerName))
                    loadResponse.Data.CustomerName = editedCustomerName;

                // Ask to edit state abbreviation
                string editedStateAbbreviation = OrderPrompts.AskForStateAbbreviation(MenuChoices.Edit,
                    AdminChoices.Empty, loadResponse.Data.StateAbbreviation);
                if (Prompts.CheckForCancel(editedStateAbbreviation))
                    return;
                if (Prompts.CheckForNotEmpty(editedStateAbbreviation))
                    loadResponse.Data.StateAbbreviation = editedStateAbbreviation;

                // Ask to edit product type
                string editedProductType = OrderPrompts.AskForProductType(MenuChoices.Edit, AdminChoices.Empty,
                    loadResponse.Data.ProductType);
                if (Prompts.CheckForCancel(editedProductType))
                    return;
                if (Prompts.CheckForNotEmpty(editedProductType))
                    loadResponse.Data.ProductType = editedProductType;

                // Ask to edit total area
                string editedTotalArea = OrderPrompts.AskForTotalArea(MenuChoices.Edit,
                    loadResponse.Data.TotalArea.ToString(CultureInfo.CurrentCulture));
                if (Prompts.CheckForCancel(editedTotalArea))
                    return;
                if (Prompts.CheckForNotEmpty(editedTotalArea))
                    loadResponse.Data.TotalArea = decimal.Parse(editedTotalArea);

                loadResponse.Data.CostPerSquareFoot =
                    productManager.LoadProduct(loadResponse.Data.ProductType).Data.CostPerSquareFoot;
                loadResponse.Data.LaborCostPerSquareFoot =
                    productManager.LoadProduct(loadResponse.Data.ProductType).Data.LaborCostPerSquareFoot;
                loadResponse.Data.TaxRate =
                    stateTaxManager.LoadStateTax(loadResponse.Data.StateAbbreviation).Data.TaxRate;

                Console.Clear();
                Displays.DrawTitle(MenuChoices.Edit, AdminChoices.Empty);
                OrderDisplays.DrawOrder(loadResponse.Data);

                Console.Write("\n\n{0,30} {1} ", "Submit edited order?", ":");

                if (Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Response<Order> editRepsonse = orderManager.EditOrder(loadResponse.Data);
                    Console.Write("\n\n  {0}  Press any key to return. ", editRepsonse.Message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Order edit cancelled.  Press any key to return. ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.Write("\n {0}  Press any key to return. ", loadResponse.Message);
            }
            Console.ReadKey();
        }
    }
}