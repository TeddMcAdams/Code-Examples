using System;
using FlooringProgram.BLL;
using FlooringProgram.Models;
using FlooringProgram.Models.Enums;
using FlooringProgram.UI.Utilities;

namespace FlooringProgram.UI.AdminWorkflows.ProductWorkflows
{
    class AddProductWorkflow
    {
        internal void Execute()
        {
            var manager = new ProductManager();

            // Ask for product type
            string productType = AdminPrompts.AdminAskForProductType(MenuChoices.Add, AdminChoices.Product);
            if (Prompts.CheckForCancel(productType))
                return;

            // Ask for cost per sqft
            string costPerSqFt = AdminPrompts.AdminAskForCostPerSqFt(MenuChoices.Add, AdminChoices.Product, productType, string.Empty);
            if (Prompts.CheckForCancel(costPerSqFt))
                return;

            // Ask for labor cost per sqft
            string laborCostPerSqFt = AdminPrompts.AdminAskForLaborCostPerSqFt(MenuChoices.Add, AdminChoices.Product, productType, string.Empty);
            if (Prompts.CheckForCancel(laborCostPerSqFt))
                return;

            // Create product
            var productToAdd = new Product()
            {
                ProductType = productType,
                CostPerSquareFoot = decimal.Parse(costPerSqFt),
                LaborCostPerSquareFoot = decimal.Parse(laborCostPerSqFt)
            };

            // Display product and ask to submit
            Console.Clear();
            Displays.DrawTitle(MenuChoices.Add, AdminChoices.Product);
            ProductDisplays.DrawProduct(productToAdd);

            Console.Write("\n\n{0,30} {1} ", "Add new product?", ":");

            if (Prompts.Confirmation())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Response<Product> response = manager.AddProduct(productToAdd);
                Console.Write("\n\n  {0}  Press any key to return. ", response.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\n\n  Add product cancelled.  Press any key to return. ");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();
        }
    }
}