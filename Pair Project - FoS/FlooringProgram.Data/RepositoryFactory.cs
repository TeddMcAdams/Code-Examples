using System.Configuration;
using FlooringProgram.Contracts;
using FlooringProgram.Data.Repositories;

namespace FlooringProgram.Data
{
    public static class RepositoryFactory
    {
        public static IOrderRepository GetOrderRepository()
        {
            if (ConfigurationManager.AppSettings.Get("Production") == "true")
                return new OrderRepository();

            return new TestOrderRepository();
        }

        public static IStateTaxRepository GetStateTaxRepository()
        {
            return new StateTaxRepository();
        }

        public static IProductRepository GetProductRepository()
        {
           return new ProductRepository();
        }
    }
}