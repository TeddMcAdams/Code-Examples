using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.Workflows
{
    class LookupOrderWorkflow
    {
        internal void Execute()
        {
            var manager = new OrderManager();
            string inputDate;
            do
            {
                inputDate = OrderPrompts.AskForDate(MenuChoices.Lookup, AdminChoices.Empty);
                if (Prompts.CheckForCancel(inputDate))
                    return;

                if (manager.CheckIfDateHasOrders(inputDate))
                    break;
                
                Console.Write("\n  No orders found for {0}.\n\n  Lookup another date? ", inputDate);
                if (!Prompts.Confirmation())
                    return;
            } while (true);

            Response<List<Order>> response = manager.LoadAllOrders(inputDate);
            if (response.Success)
            {
                string inputOrderNumber = OrderPrompts.AskForOrderNumber(MenuChoices.Lookup, AdminChoices.Empty, response.Data, inputDate);
                if (Prompts.CheckForCancel(inputOrderNumber))
                    return;
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Lookup, AdminChoices.Empty);
                OrderDisplays.DrawOrder(response.Data.Single(order => order.OrderNumber == int.Parse(inputOrderNumber)));
                Console.Write("\n\n  Press any key to return. ");
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", response.Message);
            }
            Console.ReadKey();
        }
    }
}