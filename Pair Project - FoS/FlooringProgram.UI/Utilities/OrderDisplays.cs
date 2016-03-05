using System;
using FlooringProgram.Models;

namespace FlooringProgram.UI.Utilities
{
    internal static class OrderDisplays
    {
        internal static void DrawOrder(Order order)
        {
            Console.Write("\n{2,30} {1} {0}", "Order Date", ":", order.OrderDate);
            Console.Write("\n{2,30} {1} {0}", "Order #", ":", order.OrderNumber);
            Console.Write("\n{2,30} {1} {0}", "State", ":", order.StateAbbreviation);
            Console.Write("\n{2,30} {1} {0}", "Customer name", ":", order.CustomerName);
            Console.Write("\n{2,30} {1} {0}", "Total area", ":", order.TotalArea + " SqFt");
            Console.Write("\n{2,30} {1} {0}", "Product type", ":", order.ProductType);
            Console.Write("\n\n{2,30:C} {1} {0}", "Cost Per SqFt", ":", order.CostPerSquareFoot);
            Console.Write("\n{2,30:C} {1} {0}", "Labor Cost Per SqFt", ":", order.LaborCostPerSquareFoot);
            Console.Write("\n\n{2,30:C} {1} {0}", "Material Cost", ":", order.MaterialCost);
            Console.Write("\n{2,30:C} {1} {0}", "Labor Cost", ":", order.LaborCost);
            Console.Write("\n{2,30:C} {1} {0} @ {3}%", "Tax", ":", order.Tax, order.TaxRate);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n\n{2,30:C} {1} {0}", "Total Price", ":", order.TotalPrice);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        internal static void DrawOrderSmall(Order order)
        {
            Console.Write("\n    {0,-10}{1,-29}{2,-8}{3,10:C}", order.OrderNumber, order.CustomerName, order.StateAbbreviation, order.TotalPrice);
        }
    }
}