using System.Collections.Generic;
using FlooringProgram.Models;

namespace FlooringProgram.Contracts
{
    public interface IProductRepository
    {
        void AddProduct(Product productToAdd);
        void EditProduct(Product productToEdit);
        void RemoveProduct(string productTypeToRemove);
        Product LoadProduct(string productType);
        List<Product> LoadAllProducts();
    }
}