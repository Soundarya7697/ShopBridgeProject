using ShopBridge.Models;
using ShopBridge.Respository.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ShopBridge.Respository
{
    public class ProductRespository : IProductRespository
    {
        List<Product> _productsList = new List<Product>();

        /// <summary>Adds the product.</summary>
        /// <param name="product">The product.</param>
        public bool AddProduct(Product product)
        {
            string sql= @"INSERT INTO [dbo].[ProductTable]
                                                           ([ProductName]
                                                           ,[ProductDescription]
                                                           ,[ProductPrice]
                                                           ,[ProductCode],[ProductAvailableCount])
                                                     VALUES
                                                           (@ProductName,@ProductDescription,@ProductPrice,@ProductCode,@ProductAvailableCount)";
            using (SqlConnection connection = GetSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ProductName", product.productName);
                        cmd.Parameters.AddWithValue("@ProductDescription", product.productDescription);
                        cmd.Parameters.AddWithValue("@ProductPrice", product.productPrice);
                        cmd.Parameters.AddWithValue("@ProductCode", product.productCode);
                        cmd.Parameters.AddWithValue("@ProductAvailableCount", product.productAvailableCount);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
            return true;
        }

        /// <summary>Modifies the product details.</summary>
        /// <param name="product">The product.</param>
        public bool ModifyProduct (Product product)
        {
            string sql = @"UPDATE [dbo].[ProductTable]
                                               SET [ProductName] = @ProductName
                                                  ,[ProductDescription] = @ProductDescription
                                                  ,[ProductPrice] = @ProductPrice
                                                  ,[ProductCode] = @ProductCode
                                                  ,[ProductAvailableCount] = @ProductAvailableCount
                                             WHERE ([ProductID]= @ProductID);";
            using (SqlConnection connection = GetSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ProductName", product.productName);
                        cmd.Parameters.AddWithValue("@ProductDescription", product.productDescription);
                        cmd.Parameters.AddWithValue("@ProductPrice", product.productPrice);
                        cmd.Parameters.AddWithValue("@ProductCode", product.productCode);
                        cmd.Parameters.AddWithValue("@ProductAvailableCount", product.productAvailableCount);
                        cmd.Parameters.AddWithValue("@ProductID", product.productId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
            return true;
        }

        /// <summary>Gets the product by ID.</summary>
        public Product GetProductById (int productID)
        {
            string sql = @"SELECT * FROM [dbo].[ProductTable] WHERE ([ProductID]= @ProductID);";
            Product productDetails = new Product();

            using (SqlConnection connection = GetSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        cmd.CommandText = sql;
                        using (SqlDataReader rowrd = cmd.ExecuteReader())
                        {
                            if (rowrd.HasRows)
                            {
                                if (rowrd.Read())
                                {
                                    productDetails.productId = (int)rowrd["ProductID"];
                                    productDetails.productName = rowrd["ProductName"].ToString();
                                    productDetails.productCode = rowrd["ProductCode"].ToString();
                                    productDetails.productDescription = rowrd["ProductDescription"].ToString();
                                    productDetails.productPrice = rowrd["ProductPrice"].ToString();
                                    productDetails.productAvailableCount = (int)rowrd["ProductAvailableCount"];
                                }
                            }
                        }

                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
            return productDetails;

        }

        /// <summary>Gets the list of all products.</summary>
        public List<Product> GetProducts()
        {
            string sql = @"SELECT * FROM [dbo].[ProductTable]";
            using (SqlConnection connection = GetSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.CommandText = sql;
                        using (SqlDataReader rowrd = cmd.ExecuteReader())
                        {
                            if (rowrd.HasRows)
                            {

                                while (rowrd.Read())
                                {
                                    Product prodTemp = new Product()
                                    {
                                        productId = (int)rowrd["ProductID"],
                                        productName = rowrd["ProductName"].ToString(),
                                        productCode = rowrd["ProductCode"].ToString(),
                                        productDescription = rowrd["ProductDescription"].ToString(),
                                        productPrice = rowrd["ProductPrice"].ToString(),
                                        productAvailableCount = (int)rowrd["ProductAvailableCount"]
                                    };
                                    _productsList.Add(prodTemp);
                                }
                            }
                        }

                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
            return _productsList;

        }

        /// <summary>Deletes the application.</summary>
        /// <param name="productId">The product identifier.</param>
        public bool DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                return false;
            }
            string sql = @"DELETE FROM [dbo].[ProductTable] WHERE ([ProductID]= @ProductID);";
            using (SqlConnection connection = GetSQLConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
            return true;
        }

        /// <summary>Establishes SQL connection.</summary>
        public static SqlConnection GetSQLConnection()
        {
            try
            {
                //Obtain the DB connections from Web.Config file.
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dBConnection"].ConnectionString);
                con.Open();
                return con;
            }
            catch (SqlException ex)
            {
                throw;

            }

        }
    }
}