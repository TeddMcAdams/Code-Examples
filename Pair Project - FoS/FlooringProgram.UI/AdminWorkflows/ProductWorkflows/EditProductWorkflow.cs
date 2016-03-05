using System;
using System.Globalization;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.ProductWorkflows
{
    internal class EditProductWorkflow
    {
        internal void Execute()
        {
            var manager = new ProductManager();


            string inputProductType = AdminPrompts.AdminAskForProductType(MenuChoices.Edit, AdminChoices.Product);
            if (Prompts.CheckForCancel(inputProductType))
                return;

            Response<Product> loadResponse = manager.LoadProduct(inputProductType);

            if (loadResponse.Success)
            {
                Console.Clear();
                Displays.DrawTitle(MenuChoices.Edit, AdminChoices.Product);
                ProductDisplays.DrawProduct(loadResponse.Data);
                Console.Write("\n\n{0,30} {1} ", "Edit this product?", ":");
                if (!Prompts.Confirmation())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\n\n  Edit product cancelled.  Press any key to return. ");
                    Console.ReadKey();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return;
                }
            }
            else
            {
                Console.Write("\n  {0}  Press any key to return. ", loadResponse.Message);
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Edit, AdminChoices.Product);

            // Ask to edit cost per sqft
            string editedCostPerSqFt = AdminPrompts.AdminAskForCostPerSqFt(MenuChoices.Edit, AdminChoices.Product, inputProductType,
                loadResponse.Data.CostPerSquareFoot.ToString(CultureInfo.CurrentCulture));
            if (Prompts.CheckForCancel(editedCostPerSqFt))
                return;
            if (Prompts.CheckForNotEmpty(editedCostPerSqFt))
                loadResponse.Data.CostPerSquareFoot = decimal.Parse(editedCostPerSqFt);

            // Ask to edit labor cost per sqft
            string editedLaborCostPerSqFt = AdminPrompts.AdminAskForLaborCostPerSqFt(MenuChoices.Edit, AdminChoices.Product, inputProductType,
                loadResponse.Data.LaborCostPerSquareFoot.ToString(CultureInfo.CurrentCulture));
            if (Prompts.CheckForCancel(editedLaborCostPerSqFt))
                return;
            if (Prompts.CheckForNotEmpty(editedLaborCostPerSqFt))
                loadResponse.Data.LaborCostPerSquareFoot = decimal.Parse(editedLaborCostPerSqFt);

            Console.Clear();
            Displays.DrawTitle(MenuChoices.Edit, AdminChoices.Product);
            ProductDisplays.DrawProduct(loadResponse.Data);

            Console.Write("\n\n{0,30} {1} ", "Submit edited product?", ":");

            if (Prompts.Confirmation())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Response<Product> editRepsonse = manager.EditProduct(loadResponse.Data);
                Console.Write("\n\n  {0}  Press any key to return. ", editRepsonse.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n  Order edit cancelled.  Press any key to return. ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();
        }
    }
}