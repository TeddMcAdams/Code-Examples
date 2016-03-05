using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.ProductWorkflows
{
    internal class RemoveProductWorkflow
    {
        internal void Execute()
        {
            var manager = new ProductManager();

            var inputProductType = AdminPrompts.AdminAskForProductType(MenuChoices.Remove, AdminChoices.Product);
            if (Prompts.CheckForCancel(inputProductType))
                return;

            Response<Product> loadResponse = manager.LoadProduct(inputProductType);
            if (loadResponse.Success)
            {
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Remove, AdminChoices.Product);
                ProductDisplays.DrawProduct(loadResponse.Data);
                Console.Write("\n\n{0,30} {1} ", "Remove product?", ":");
                if (Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Response<Product> removeResponse = manager.RemoveProduct(inputProductType);
                    Console.Write("\n\n  {0}  Press any key to return. ", removeResponse.Message);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Remove product cancelled.  Press any key to return. ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", loadResponse.Message);
            }
            Console.ReadKey();
        }
    }
}