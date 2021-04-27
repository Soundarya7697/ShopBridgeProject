using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string productDescription { get; set; }
        public string productPrice { get; set; }
        public int productAvailableCount { get; set; }

    }
}