using ShopBridge.Models;
using ShopBridge.Respository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.UnitTestProject
{
    class TestMockRespository : IProductRespository
    {
        List<Product> _products = null;

        public TestMockRespository (List<Product> products)
        {
            _products = products;
        }

        public List<Product> GetProducts ()
        {
            return _products;
        }

        public Product GetProductById (int productId)
        {
            return _products.SingleOrDefault( product => product.productId == productId);
        }

        public bool AddProduct (Product product)
        {
            _products.Add(product);
             return true;
        }

        public bool ModifyProduct (Product product)
        {
            int id = product.productId;
            Product productToUpdate = _products.SingleOrDefault(prod => prod.productId == product.productId);
            return true;
            
        }

        public bool DeleteProduct (int productId)
        {
            _products.SingleOrDefault(product => product.productId == productId);
            return true;
        }

    }
}
