using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace NawazEyeWebProject_NEW_.Models
{
    public class Cart
    {
        SqlConnection con;
        SqlCommand cmd;
        string query;
        int id;
        bool status;
        Buyer buyer;
        public Cart(int id)
        {
            SetValues(id);
        }
        public Cart(Buyer buyer)
        {
            if (buyer.GetCurrentCart() == null)
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "INSERT INTO [CARTS] ([BuyerId]) VALUES (" + buyer.BuyerId + "); Select MAX(CartId) from CARTS;";
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
            else
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select CartId from CARTS where StatusFlag=1 and BuyerId="+buyer.BuyerId;
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    SetValues((int)cmd.ExecuteScalar());
                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public int CartId
        {
            get
            {
                return id;
            }
        }
        public bool IsCurrent
        {
            get
            {
                return status;
            }
            set
            {
                try
                {
                    int i = Convert.ToInt16(value);
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "update CARTS set StatusFlag=" + i + " where CartId=" + id;
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
        public Buyer Buyer
        {
            get
            {
                return buyer;
            }
        }
        public List<CartSunglasses> Sunglasses
        {
            get
            {
                List<CartSunglasses> l = new List<CartSunglasses>();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    string query = "select ProductId, Quantity from CART_HAS_SUNGLASSES where CartId="+id;
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Sunglasses s = new Sunglasses((int)reader[0]);
                        CartSunglasses c = new CartSunglasses { Sunglasses = s, Quantity = (int)reader[1] };
                        l.Add(c);
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
        public List<CartPrescriptionGalsses> PrescriptionGlasses
        {
            get
            {
                List<CartPrescriptionGalsses> l = new List<CartPrescriptionGalsses>();
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    string query = "select ProductId, Quantity, Prescription from CART_HAS_PRESCRPTION_GLASSES where CartId=" + id;
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PrescriptionGlasses p = new PrescriptionGlasses((int)reader[0]);
                        CartPrescriptionGalsses c = new CartPrescriptionGalsses { PrescriptionGlasses = p, Quantity = (int)reader[1], Prescription = (string)reader[2] };
                        l.Add(c);
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
        public decimal TotalPrice
        {
            get
            {
                decimal total=0;
                foreach (CartSunglasses s in Sunglasses)
                {
                    total += s.ItemTotal;
                }
                foreach (CartPrescriptionGalsses p in PrescriptionGlasses)
                {
                    total += p.ItemTotal;
                }
                return total;
            }
        }
        public void AddSunglasses(Sunglasses s, int quantity)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [CART_HAS_SUNGLASSES] ([CartId] ,[ProductId] ,[Quantity]) VALUES (" + CartId + " ," + s.ProductId + " ," + quantity + ")";
                cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    SetValues(id);
                }
                else
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
        public void AddPrescriptionglasses(PrescriptionGlasses p, int quantity, string prescription)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [CART_HAS_PRESCRPTION_GLASSES] ([CartId] ,[ProductId] ,[Quantity]) VALUES (" + CartId + " ," + p.ProductId + " ," + quantity + ", " + prescription + ")";
                cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    SetValues(id);
                }
                else
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
        private void SetValues(int id)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select * from CARTS where CartId="+id;
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.id = (int)reader[0];
                    status = (bool)reader[1];
                    buyer = new Buyer((int)reader[2]);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public int GetItemsCount()
        {
            int count = 0;
            count += Sunglasses.Count;
            count += PrescriptionGlasses.Count;
            return count;
        }
        public bool Delete()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "DELETE FROM [dbo].CARTS WHERE CartId = " + id;
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
    public class CartSunglasses
    {
        public CartSunglasses()
        {

        }
        public Sunglasses Sunglasses { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal
        {
            get
            {
                if (Sunglasses.Discount > 0)
                {
                    return Sunglasses.GetDiscountedPrice() * Quantity;
                }
                else
                {
                    return Sunglasses.Price * Quantity;
                }
            }
        }
    }
    public class CartPrescriptionGalsses
    {
        public CartPrescriptionGalsses()
        {
            
        }
        public PrescriptionGlasses PrescriptionGlasses { get; set; }
        public int Quantity { get; set; }
        public string Prescription { get; set; }
        public decimal ItemTotal
        {
            get
            {
                if (PrescriptionGlasses.Discount > 0)
                {
                    return PrescriptionGlasses.GetDiscountedPrice() * Quantity;
                }
                else
                {
                    return PrescriptionGlasses.Price * Quantity;
                }
            }
        }
    }
}