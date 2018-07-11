using System;
using System.Data.SqlClient;
using System.Configuration;

namespace NawazEyeWebProject_NEW_.Models
{
    public class Sunglasses:Product
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string query;
        string lensColor;
        public Sunglasses(int id)
        {
            SetValues(id);
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select LensColor from SUNGLASSES where ProductId="+id;
                cmd = new SqlCommand(query, con);
                con.Open();
                lensColor = (string)cmd.ExecuteScalar();
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public Sunglasses(string name, decimal price, int quantity, int discount, string frameColor, string productDescription, bool stopOrder, string lensColor)
        {
            Product p = new Product(name, price, quantity, discount, frameColor, productDescription, stopOrder);
            id = p.ProductId;
            Name = p.Name;
            Price = p.Price;
            Quantity = p.Quantity;
            Discount = p.Discount;
            FrameColor = p.FrameColor;
            StopOrder = p.StopOrder;
            ProductDescription = p.ProductDescription;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [SUNGLASSES] ([ProductId],[LensColor]) VALUES ("+ProductId+",'"+lensColor+"')";
                cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    Exception e = new Exception("Data not inserted into the database");
                    throw e;
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public string LensColor
        {
            get
            {
                return lensColor;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "update SUNGLASSES set LensColor='" + value + "' where ProductId=" + id;
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
    }
}