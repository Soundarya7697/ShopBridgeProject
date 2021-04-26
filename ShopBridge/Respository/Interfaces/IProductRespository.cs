using ShopBridge.Models;
using System.Collections.Generic;

namespace ShopBridge.Respository.Interfaces
{
    public interface IProductRespository
    {
        bool AddProduct(Product product);

        List<Product> GetProducts ();

        Product GetProductById (int productID);

        bool DeleteApplication(int productId);

        
    }

}