using System;
using System.Collections.Generic;
using System.Linq;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Utilities
{
    internal static class ProductDisplays
    {
        internal static void DrawProductChoices()
        {
            var manager = new ProductManager();

            Response<List<Product>> response = manager.LoadAllProducts();

            if (response.Success)
            {
                List<string> products = response.Data.Select(product => product.ProductType).ToList();
                Console.Write("\n  Choices: ");
                foreach (string product in products)
                {
                    Console.Write((product == products.Last()) ? "{0}.\n" : "{0}, ", product);
                }
            }
        }

        internal static void DrawProduct(Product product)
        {
            Console.Write("\n{0,30} {1} {2}", product.ProductType, ":", "Product Type");
            Console.Write("\n{0,30:C} {1} {2}", product.CostPerSquareFoot, ":", "Cost Per SqFt");
            Console.Write("\n{0,30:C} {1} {2}", product.LaborCostPerSquareFoot, ":", "Labor Cost Per SqFt");
        }
    }
}