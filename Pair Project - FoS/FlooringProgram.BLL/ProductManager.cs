using System;
using System.Collections.Generic;
using FlooringProgram.Contracts;
using FlooringProgram.Data;
using FlooringProgram.Models;

namespace FlooringProgram.BLL
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager()
        {
            _productRepository = RepositoryFactory.GetProductRepository();
        }

        public Response<Product> AddProduct(Product productToAdd)
        {
            var response = new Response<Product>();

            try
            {
                _productRepository.AddProduct(productToAdd);
                response.Success = true;
                response.Message = "Product added.";
                response.Data = _productRepository.LoadProduct(productToAdd.ProductType);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to add product.";
            }
            return response;
        }

        public Response<Product> RemoveProduct(string productTypeToRemove)
        {
            var response = new Response<Product>();

            try
            {
                _productRepository.RemoveProduct(productTypeToRemove);
                response.Success = true;
                response.Message = "Product removed.";
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to remove product.";
            }
            return response;
        }

        public Response<Product> EditProduct(Product productToEdit)
        {
            var response = new Response<Product>();

            try
            {
                _productRepository.EditProduct(productToEdit);
                response.Success = true;
                response.Message = "Product edited.";
                response.Data = _productRepository.LoadProduct(productToEdit.ProductType);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to edit product.";
            }
            return response;
        }

        public Response<Product> LoadProduct(string productType)
        {
            var response = new Response<Product>();

            try
            {
                response.Success = true;
                response.Message = "Product loaded.";
                response.Data = _productRepository.LoadProduct(productType);
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to load product.";
            }
            return response;
        }

        public Response<List<Product>> LoadAllProducts()
        {
            var response = new Response<List<Product>>();

            try
            {
                response.Success = true;
                response.Message = "All products loaded.";
                response.Data = _productRepository.LoadAllProducts();
            }
            catch (Exception exception)
            {
                CsvWriter.WriteException(exception);
                response.Success = false;
                response.Message = "Failed to load products.";
            }
            return response;
        }
    }
}