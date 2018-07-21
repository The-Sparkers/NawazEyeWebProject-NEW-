using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace NawazEyeWebProject_NEW_.Models
{
    public class Order
    {
        SqlConnection con;
        SqlCommand cmd;
        string query, status;
        int id;
        DateTime oDate, dDate;
        public Order(int id)
        {
            SetValues(id);
        }
        public Order(Cart cart, DateTime orderDate, PromoCode promoCode = null)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [ORDERS] ([OrderDate]) VALUES ('" + orderDate.ToString() + "'); Select MAX(OrderID)from ORDERS; ";
                cmd = new SqlCommand(query, con);
                con.Open();
                id = (int)cmd.ExecuteScalar();
                con.Close();
                SetValues(id);
                if (promoCode != null)
                {
                    query = "INSERT INTO [ORDER_HAS_CART_WITH_PROMO] ([OrderId] ,[CartId] ,[PromoId]) VALUES (" + id + " ," + cart.CartId + " ," + promoCode.PromoId + ")";
                }
                else
                {
                    query = "INSERT INTO [ORDER_HAS_CART_WITH_PROMO] ([OrderId] ,[CartId]) VALUES (" + id + " ," + cart.CartId + ")";
                }
                cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() != 1)
                {
                    throw new Exception("Data Not Inserted correctly.");
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public int OrderId
        {
            get
            {
                return id;
            }
        }
        public DateTime OrderDate
        {
            get
            {
                return oDate;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "UPDATE ORDERS SET Status='" + value + "' WHERE OrderId=" + id;
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
        public DateTime DispatchDate
        {
            get
            {
                return dDate;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "UPDATE ORDERS SET DispatchDate='" + value.ToString() + "' WHERE OrderId=" + id;
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
        public Cart Cart
        {
            get
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select CartId from ORDER_HAS_CART_WITH_PROMO where OrderId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    Cart c = new Cart((int)cmd.ExecuteScalar());
                    con.Close();
                    return c;
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public PromoCode Promo
        {
            get
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select PromoId from ORDER_HAS_CART_WITH_PROMO where OrderId=" + id;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    PromoCode p = new PromoCode((int)cmd.ExecuteScalar());
                    con.Close();
                    return p;
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
        }
        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select s.ProductId from CARTS c, CART_HAS_SUNGLASSES s, ORDER_HAS_CART_WITH_PROMO o where o.OrderId=" + id + " and c.CartId=o.CartId and s.CartId=c.CartId";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Sunglasses s = new Sunglasses((int)reader[0]);
                        total += s.GetDiscountedPrice();
                    }
                    con.Close();
                    query = "select p.ProductId from CARTS c, CART_HAS_PRESCRPTION_GLASSES p, ORDER_HAS_CART_WITH_PROMO o where o.OrderId=" + id + " and c.CartId=o.CartId and p.CartId=c.CartId";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PrescriptionGlasses p = new PrescriptionGlasses((int)reader[0]);
                        total += p.GetDiscountedPrice();
                    }
                    con.Close();
                    return total;
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public decimal GetDiscountedPrice()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select p.Discount from PROMO_CODES p, ORDER_HAS_CART_WITH_PROMO o where p.PromoId=o.PromoId and o.OrderId=" + id;
                cmd = new SqlCommand(query, con);
                con.Open();
                decimal discount = (int)cmd.ExecuteScalar();
                con.Close();
                decimal x = discount / 100;
                decimal tot = TotalPrice * x;
                return TotalPrice - tot;
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public static List<Order> Search(string status)
        {
            List<Order> l = new List<Order>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select OrderId from ORDERS where Status LIKE '%" + status + "%'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(new Order((int)reader[0]));
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
        private void SetValues(int id)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select * from ORDERS where OrderId=" + id;
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.id = (int)reader[0];
                    status = (string)reader[1];
                    oDate = (DateTime)reader[2];
                    dDate = (DateTime)reader[3];
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public bool Delete()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "DELETE FROM [dbo].ORDERS WHERE OrderId = " + id;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    con.Close();
                    return true;
                }
                else if (cmd.ExecuteNonQuery() < 1)
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
    }
}