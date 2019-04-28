using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace NawazEyeWebProject_NEW_.Models
{
    public class Product
    {
        SqlConnection con;
        SqlCommand cmd;
        string query;
        public int id;
        int quantity, discount;
        decimal price;
        string name, prodDes, frameclr;
        bool stopO;
        int itemsSold;
        public Product()
        {

        }
        public Product(int id)
        {
            SetValues(id);
        }
        public Product(string name, decimal price, int quantity, int discount, string frameColor, string productDescription, bool stopOrder)
        {
            try
            {
                int i = Convert.ToInt16(stopO);
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [PRODUCTS] ([Name],[Quantity],[Price],[Discount],[FrameColor],[ProductDescription],[StopOrder]) VALUES('" + name + "'," + quantity.ToString() + "," + price.ToString() + "," + discount.ToString() + ",'" + frameColor + "','" + productDescription + "'," + i + "); Select MAX(ProductId) from PRODUCTS;";
                cmd = new SqlCommand(query, con);
                con.Open();
                id = (int)cmd.ExecuteScalar();
                con.Close();
                SetValues(id);
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public int ProductId
        {
            get
            {
                return id;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set Name='" + value + "' where ProductId=" + id.ToString();
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public string ProductDescription
        {
            get
            {
                return prodDes;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set ProductDescription='" + value + "' where ProductId=" + id.ToString();
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set Price=" + value + " where ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set Quantity=" + value + " where ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public int Discount
        {
            get
            {
                return discount;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set Discount=" + value + " where ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public string FrameColor
        {
            get
            {
                return frameclr;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set FrameColor='" + value + "' where ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public bool StopOrder
        {
            get
            {
                return stopO;
            }
            set
            {
                try
                {
                    int i = Convert.ToInt16(value);
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update PRODUCTS set StopOrder=" + i + " where ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public List<string> Images
        {
            get
            {
                List<string> l = new List<string>();
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select * from PRODUCT_IMAGES where ProductId=" + id.ToString();
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        l.Add((string)reader[1]);
                    }
                    con.Close();
                    return l;
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public string PrimaryImage
        {
            get
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select Image from PRODUCT_IMAGES where PrimaryFlag=1  AND ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    string pi = (string)cmd.ExecuteScalar();
                    con.Close();
                    return pi;
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        protected void SetValues(int id)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "Select * From PRODUCTS where ProductId=" + id.ToString();
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.id = (int)reader[0];
                    name = (string)reader[1];
                    quantity = (int)reader[2];
                    price = (decimal)reader[3];
                    discount = (int)reader[4];
                    frameclr = (string)reader[5];
                    prodDes = (string)reader[6];
                    stopO = (bool)reader[7];
                    itemsSold = (int)reader[8];
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public decimal GetDiscountedPrice()
        {
            decimal discount = decimal.Divide(Discount, 100); 
            decimal dPercent = Price * discount; 
            decimal dPrice = Price - dPercent;
            return dPrice;
        }
        public static List<Product> SearchProduct(string matchName)
        {
            List<Product> l = new List<Product>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select ProductId from PRODUCTS where StopOrder=0 And Name like '%" + matchName + "%'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(new Product((int)reader[0]));
                }
                con.Close();
                return l;
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public void AddImage(string image)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [PRODUCT_IMAGES] ([ProductId],[Image],[PrimaryFlag]) VALUES (" + id + ",'" + image + "',0)";
                cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    Exception e = new Exception("Database Proccessing Error.");
                    throw e;
                }
                else
                {
                    SetValues(id);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public void SetPrimaryImage(string image)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "update PRODUCT_IMAGES set PrimaryFlag=1 where Image='" + image + "' and ProductId=" + id + "; update PRODUCT_IMAGES set PrimaryFlag = 0 where ProductId = " + id + " and Image!='" + image + "'; ";
                cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                SetValues(id);

                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public int ItemsSold 
        {
            get
            {
                return itemsSold;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "update PRODUCTS set ItemsSold=" + value + " where ProductId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(id);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public static List<Product> GetHotItems()
        {
            List<Product> l = new List<Product>(3);
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select top (3) ProductId from PRODUCTS where StopOrder=0 order by ItemsSold desc";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(new Product((int)reader[0]));
                }
                con.Close();
                return l;
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public static bool IsSunglasses(int productId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select * from SUNGLASSES where ProductId=" + productId; 
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsPrescriptionGlasses(int productId)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select * from PRESCRIPTION_GLASSES where ProductId=" + productId;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsProduct(int productId)
        {
            if (IsSunglasses(productId))
            {
                return true;
            }
            else if (IsPrescriptionGlasses(productId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete()
        {
            foreach (string image in Images)
            {
                DeleteImage(image);
            }
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "DELETE FROM [PRODUCTS] WHERE ProductId= " + id; 
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else if(cmd.ExecuteNonQuery()<1)
                {
                    con.Close();
                    return false;
                }
                else
                {
                    throw new Exception("Something Went Wrong in deleting.");
                }
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        private void DeleteImage(string fileName)
        {
            /*Some Code to Delete From FTP Server
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             * */
            File.Delete(@"D:\New folder\VS Projects\NawazEyeWebProject\123NawazEyeWebProject\NawazEyeWebProject\images\Uploads\" + fileName);
        }
    }
}