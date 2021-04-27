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
        public JsonResult GetAllProducts(int productID = 0)
        {
            try
            {
                if (productID > 0)
                {
                    var selectedProduct = _productRepository.GetProductById(productID);
                    return Json(selectedProduct, JsonRequestBehavior.AllowGet);

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

        [HttpPost]
        /// <summary>Deletes the specified product identifier.</summary>
        /// <param name="productID">The product identifier.</param>
        public JsonResult DeleteProduct(int productID)
        {
            try
            {
                var response = _productRepository.DeleteProduct(productID);
                return Json(response);

            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        /// <summary>Modifies the specified product data based on the identifier.</summary>
        /// <param name="productID">The product identifier.</param>
        [HttpPost]
        public JsonResult ModifyProduct (Product product)
        {
            try
            {
                var response = _productRepository.ModifyProduct(product);
                return Json(response);

            }
            catch (Exception e)
            {
                return Json(false);
            }
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