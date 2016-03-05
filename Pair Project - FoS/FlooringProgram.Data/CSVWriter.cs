using System;
using System.Collections.Generic;
using System.IO;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public static class CsvWriter
    {
        internal static List<Order> ReadAllOrders(string date)
        {
            string filePath = $"Data\\Orders_{date.Replace("/", "")}.txt";
            string[] delimiter = { "\",\"" };

            if (!File.Exists(filePath))
                return new List<Order>();

            string[] reader = File.ReadAllLines(filePath);
            var allOrders = new List<Order>();

            for (var i = 1; i < reader.Length; i++)
            {
                string[] columns = reader[i].Split(delimiter, StringSplitOptions.None);
                var order = new Order()
                {
                    OrderNumber = int.Parse(columns[0]),
                    OrderDate = columns[1].Replace(",,", ","),
                    CustomerName = columns[2].Replace(",,", ","),
                    StateAbbreviation = columns[3].Replace(",,", ","),
                    ProductType = columns[4].Replace(",,", ","),
                    TotalArea = decimal.Parse(columns[5]),
                    CostPerSquareFoot = decimal.Parse(columns[6]),
                    LaborCostPerSquareFoot = decimal.Parse(columns[7]),
                    TaxRate = decimal.Parse(columns[8]),
                };
                allOrders.Add(order);
            }
            return allOrders;
        }

        internal static void WriteAllOrders(List<Order> allOrders, string date)
        {
            string filePath = $"Data\\Orders_{date.Replace("/", "")}.txt";
            if (File.Exists(filePath))
                File.Delete(filePath);
            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine("OrderNumber,OrderDate,CustomerName,StateAbbreviation," +
                                 "ProductType,TotalArea,CostPerSquareFoot,LaborCostPerSquareFoot," +
                                 "TaxRate,MaterialCost,LaborCost,Tax,TotalPrice"); 
                foreach (Order order in allOrders)
                {
                    writer.WriteLine("{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}",
                        order.OrderNumber,
                        order.OrderDate.Replace(",", ",,"),
                        order.CustomerName.Replace(",", ",,"),
                        order.StateAbbreviation.Replace(",", ",,"),
                        order.ProductType.Replace(",", ",,"),
                        order.TotalArea,
                        order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot,
                        order.TaxRate,
                        order.MaterialCost,
                        order.LaborCost,
                        order.Tax,
                        order.TotalPrice
                        );
                }
            }
        }

        internal static List<Product> ReadAllProducts()
        {
            const string filePath = @"Data\Products.txt";
            string[] delimiter = { "\",\""};
            string[] reader = File.ReadAllLines(filePath);
            var allProducts = new List<Product>();

            for (var i = 1; i < reader.Length; i++)
            {
                string[] columns = reader[i].Split(delimiter, StringSplitOptions.None);
                var product = new Product()
                {
                    ProductType = columns[0].Replace(",,", ","),
                    CostPerSquareFoot = decimal.Parse(columns[1]),
                    LaborCostPerSquareFoot = decimal.Parse(columns[2])
                };
                allProducts.Add(product);
            }
            return allProducts;
        }

        internal static void WriteAllProducts(List<Product> allProducts)
        {
            string filePath = @"Data\Products.txt";

            File.Delete(filePath);
            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine("ProductType,CostPerSquareFoot,LaborCostPerSquareFoot");
                foreach (Product product in allProducts)
                {
                    writer.WriteLine("{0}\",\"{1}\",\"{2}",
                        product.ProductType.Replace(",", ",,"),
                        product.CostPerSquareFoot,
                        product.LaborCostPerSquareFoot);
                }
            }
        }

        internal static List<StateTax> ReadAllStateTax()
        {
            const string filePath = @"Data\StateTax.txt";
            string[] delimiter = new string[] { "\",\""};
            string[] reader = File.ReadAllLines(filePath);
            var allStateTax = new List<StateTax>();

            for (var i = 1; i < reader.Length; i++)
            {   
                string[] columns = reader[i].Split(delimiter, StringSplitOptions.None);
                var stateTax = new StateTax()
                {
                    StateAbbreviation = columns[0].Replace(",,", ","),
                    StateName = columns[1].Replace(",,", ","),
                    TaxRate = decimal.Parse(columns[2])
                };
                allStateTax.Add(stateTax);
            }
            return allStateTax;
        }

        internal static void WriteAllStateTax(List<StateTax> allStateTax)
        {
            const string filePath = @"Data\StateTax.txt";

            File.Delete(filePath);
            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine("StateAbbreviation,StateName,TaxRate");
                foreach (StateTax stateTax in allStateTax)
                {
                    writer.WriteLine("{0}\",\"{1}\",\"{2}",
                        stateTax.StateAbbreviation.Replace(",",",,"),
                        stateTax.StateName.Replace(",", ",,"),
                        stateTax.TaxRate
                        );
                }
            }
        }

        public static void WriteException(Exception exception)
        {
            const string filePath = @"Data\ErrorLog.txt";

            using (var writer = File.CreateText(filePath))
            {
                writer.WriteLine("{0} {1}", DateTime.Now.ToString("MM/dd/yyyy h:mm:ss"), exception.Message);
            }
        }
    }
}