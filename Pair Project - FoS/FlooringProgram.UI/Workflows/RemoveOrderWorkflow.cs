using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.Workflows
{
    internal class RemoveOrderWorkflow
    {
        internal void Execute()
        {
            var manager = new OrderManager();
            string inputDate;
            do
            {
                inputDate = OrderPrompts.AskForDate(MenuChoices.Remove, AdminChoices.Empty);
                if (Prompts.CheckForCancel(inputDate))
                    return;
                if (manager.CheckIfDateHasOrders(inputDate))
                    break;
                Console.Write("\n  No orders found for {0}.\n\n  Lookup another date? ", inputDate);
                if (!Prompts.Confirmation())
                    return;
            } while (true);

            Response<List<Order>> loadAllResponse = manager.LoadAllOrders(inputDate);
            if (loadAllResponse.Success)
            {
                string inputOrderNumber = OrderPrompts.AskForOrderNumber(MenuChoices.Remove, AdminChoices.Empty, loadAllResponse.Data, inputDate);
                if (Prompts.CheckForCancel(inputOrderNumber))
                    return;
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Remove, AdminChoices.Empty);
                OrderDisplays.DrawOrder(loadAllResponse.Data.Single(order => order.OrderNumber == int.Parse(inputOrderNumber)));
                Console.Write("\n\n{0,30} {1} ", "Remove order?", ":");
                if (Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Response<Order> removeReponse = manager.RemoveOrder(int.Parse(inputOrderNumber), inputDate);
                    Console.Write("\n\n  {0}  Press any key to return. ", removeReponse.Message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Remove order cancelled.  Press any key to return. ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", loadAllResponse.Message);
            }
            Console.ReadKey();
        }
    }
}