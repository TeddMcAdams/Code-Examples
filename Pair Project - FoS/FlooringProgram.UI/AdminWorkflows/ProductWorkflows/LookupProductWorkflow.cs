using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.ProductWorkflows
{
    internal class LookupProductWorkflow
    {
        internal void Execute()
        {
            var manager = new ProductManager();

            string inputProductType = AdminPrompts.AdminAskForProductType(MenuChoices.Lookup, AdminChoices.Product);
            if (Prompts.CheckForCancel(inputProductType))
                return;

            Response<Product> response = manager.LoadProduct(inputProductType);
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Lookup, AdminChoices.Product);
            if (response.Success)
            {
                ProductDisplays.DrawProduct(response.Data);
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