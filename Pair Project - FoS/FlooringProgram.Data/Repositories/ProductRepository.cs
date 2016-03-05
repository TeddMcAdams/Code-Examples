using System.Collections.Generic;
using System.Linq;
using FlooringProgram.Contracts;
using FlooringProgram.Models;

namespace FlooringProgram.Data.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        public void AddProduct(Product productToAdd)
        {
            List<Product> allProducts = LoadAllProducts();

            allProducts.Add(productToAdd);
            CsvWriter.WriteAllProducts(allProducts);
        }

        public void EditProduct(Product productToUpdate)
        {
            List<Product> allProducts = LoadAllProducts();

            allProducts = allProducts.Where(product => product.ProductType != productToUpdate.ProductType).ToList();
            allProducts.Add(productToUpdate);
            allProducts = allProducts.OrderBy(product => product.ProductType).ToList();
            CsvWriter.WriteAllProducts(allProducts);
        }

        public void RemoveProduct(string productTypeToRemove)
        {
            List<Product> allProducts = LoadAllProducts();

            allProducts = allProducts.Where(product => product.ProductType != productTypeToRemove).ToList();
            CsvWriter.WriteAllProducts(allProducts);
        }

        public Product LoadProduct(string productType)
        {
            return LoadAllProducts().Single(product => product.ProductType == productType);
        }

        public List<Product> LoadAllProducts()
        {
            return CsvWriter.ReadAllProducts();
        }
    }
}