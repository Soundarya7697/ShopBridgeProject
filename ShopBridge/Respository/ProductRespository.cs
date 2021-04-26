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
                                                           ,[ProductCode])
                                                     VALUES
                                                           (@ProductName,@ProductDescription,@ProductPrice,@ProductCode)";
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
                        cmd.CommandText = sql;
                        using (SqlDataReader rowrd = cmd.ExecuteReader())
                        {
                            if (rowrd.HasRows)
                            {
                                productDetails.productId = (int)rowrd["ProductID"];
                                productDetails.productName = rowrd["ProductName"].ToString();
                                productDetails.productCode = rowrd["ProductCode"].ToString();
                                productDetails.productDescription = rowrd["ProductDescription"].ToString();
                                productDetails.productPrice = rowrd["ProductPrice"].ToString();

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
        public bool DeleteApplication(int productId)
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