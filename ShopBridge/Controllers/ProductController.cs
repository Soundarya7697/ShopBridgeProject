using ShopBridge.Models;
using System;
using System.Web.Mvc;
using ShopBridge.Respository.Interfaces;
using System.Linq;
using ShopBridge.Respository;

namespace ShopBridge.Controllers
{
    public class ProductController : Controller
    {
        IProductRespository _productRepository = new ProductRespository();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>Gets all Products/Products based on ID.</summary>
        /// <param name="productID">The product identifier is an optional parameter.</param>
        [HttpGet]
        public ActionResult GetAllProducts(int productID = 0)
        {
            try
            {
                if (productID > 0)
                {
                    var selectedProduct = _productRepository.GetProductById(productID);
                    return View("~/Views/Product/Product.cshtml", selectedProduct);

                }
                else
                {
                    var savedProductList = _productRepository.GetProducts();
                    return Json(savedProductList, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>Deletes the specified product identifier.</summary>
        /// <param name="productID">The product identifier.</param>
        public JsonResult Delete(int productID)
        {
            try
            {
                var response = _productRepository.DeleteApplication(productID);
                return Json(response);

            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        /// <summary>Modifies the specified product data based on the identifier.</summary>
        /// <param name="productID">The product identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public ActionResult Modify(int productID)
        {
            var savedProductList = _productRepository.GetProducts();
            if (productID > 0)
            {
                var selectedProduct = savedProductList.FirstOrDefault(i => i.productId == productID);
            }

         return View();
        }

        /// <summary>Adds the product.</summary>
        /// <param name="product">The product.</param>
        [HttpPost]
        public JsonResult AddProduct(Product product)
        {
            try
            { 
                var response = _productRepository.AddProduct(product);
                return Json(response);

            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
    }
}