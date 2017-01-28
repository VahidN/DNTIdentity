using System.Collections.Generic;
using ASPNETCoreIdentitySample.Entities;

namespace ASPNETCoreIdentitySample.Services.Contracts
{
    public interface IProductService
    {
        void AddNewProduct(Product product);
        IList<Product> GetAllProducts();
    }
}