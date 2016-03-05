using System.Collections.Generic;
using System.Configuration;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    internal static class MockDatabase
    {
        internal static readonly Dictionary<string, List<Order>> TestOrderRepositoryData =
            new Dictionary<string, List<Order>>();

        static MockDatabase()
        {
            if (ConfigurationManager.AppSettings.Get("Production") == "true") return;

            TestOrderRepositoryData.Add("01/01/2016", new List<Order>
            {
                new Order()
                {
                    OrderNumber = 1,
                    OrderDate = "01/01/2016",
                    CustomerName = "Bob Smith",
                    StateAbbreviation = "OH",
                    ProductType = "Tile",
                    TotalArea = 10m,
                    CostPerSquareFoot = 3.50M,
                    LaborCostPerSquareFoot = 4.15M,
                    TaxRate = 6.25M
                },
                new Order()
                {
                    OrderNumber = 2,
                    OrderDate = "01/01/2016",
                    CustomerName = "GE Inc.",
                    StateAbbreviation = "OH",
                    ProductType = "Tile",
                    TotalArea = 10m,
                    CostPerSquareFoot = 3.50M,
                    LaborCostPerSquareFoot = 4.15M,
                    TaxRate = 6.25M
                }

            });
            TestOrderRepositoryData.Add("01/02/2016", new List<Order>
            {
                new Order()
                {
                    OrderNumber = 1,
                    OrderDate = "01/02/2016",
                    CustomerName = "Mike Masterson",
                    StateAbbreviation = "MI",
                    ProductType = "Wood",
                    TotalArea = 5m,
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M,
                    TaxRate = 5.75M
                },
                new Order()
                {
                    OrderNumber = 2,
                    OrderDate = "01/02/2016",
                    CustomerName = "Bob Balla",
                    StateAbbreviation = "MI",
                    ProductType = "Laminate",
                    TotalArea = 50m,
                    CostPerSquareFoot = 1.75M,
                    LaborCostPerSquareFoot = 2.10M,
                    TaxRate = 5.75M
                }

            });
            TestOrderRepositoryData.Add("01/03/2016", new List<Order>
            {
                new Order()
                {
                    OrderNumber = 1,
                    OrderDate = "01/03/2016",
                    CustomerName = "Eileen",
                    StateAbbreviation = "MI",
                    ProductType = "Wood",
                    TotalArea = 5m,
                    CostPerSquareFoot = 5.15M,
                    LaborCostPerSquareFoot = 4.75M,
                    TaxRate = 5.75M
                },
                new Order()
                {
                    OrderNumber = 2,
                    OrderDate = "01/03/2016",
                    CustomerName = "Erin",
                    StateAbbreviation = "MI",
                    ProductType = "Laminate",
                    TotalArea = 50m,
                    CostPerSquareFoot = 1.75M,
                    LaborCostPerSquareFoot = 2.10M,
                    TaxRate = 5.75M
                },
                new Order()
                {
                    OrderNumber = 3,
                    OrderDate = "01/03/2016",
                    CustomerName = "Sally",
                    StateAbbreviation = "MI",
                    ProductType = "Laminate",
                    TotalArea = 50m,
                    CostPerSquareFoot = 1.75M,
                    LaborCostPerSquareFoot = 2.10M,
                    TaxRate = 5.75M
                }
            });
        }
    }
}