using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace NawazEyeWebProject_NEW_.Models
{
    public class Buyer
    {
        SqlConnection con;
        SqlCommand cmd;
        string query;
        int id;
        string name, phoneNumber, address, email;
        City city;
        public Buyer(int id)
        {
            SetValues(id);
        }
        public Buyer(string name, string phoneNumber, string address, string email, City city)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [BUYERS] ([Name] ,[PhoneNumber] ,[Address] ,[Email] ,[CityId]) VALUES('" + name + "' ,'" + phoneNumber + "','" + address + "' ,'" + email + "' ," + city.CityId+ "); Select MAX(BuyerId) from BUYERS;";
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
        private void SetValues(int id)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select * from BUYERS where BuyerId=" + id;
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.id = (int)reader[0];
                    name = (string)reader[1];
                    phoneNumber = (string)reader[2];
                    address = (string)reader[3];
                    email = (string)reader[4];
                    city = new City((int)reader[5]);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public int BuyerId
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
                    query = "Update BUYERS set Name='" + value + "' where BuyerId=" + id;
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
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update BUYERS set PhoneNumber='" + value + "' where BuyerId=" + id;
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
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update BUYERS set Email='" + value + "' where BuyerId=" + id;
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
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update BUYERS set Address='" + value + "' where BuyerId=" + id;
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
        public City City
        {
            get
            {
                return city;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "Update BUYERS set CityId=" + value.CityId + " where BuyerId=" + id;
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
        public static Account GetAccount(int id)
        {
            try
            {
                Account acnt = new Account(id);
                if (acnt.AccountId == null)
                {
                    return null;
                }
                else
                {
                    return acnt;
                }
            }
            catch(Exception)
            {
                return null;
            }
        }
        public void SetAccount(string AccountId)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "insert into BuyerHasAccount(BuyerId,AccountId) Values(" + BuyerId + ",'" + AccountId + "')";
                cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public Account GetAccount()
        {
            try
            {
                Account acnt = new Account(id);
                if (acnt.AccountId == null)
                {
                    return null;
                }
                else
                {
                    return acnt;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Cart GetCurrentCart()
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select CartId from CARTS where BuyerId=" + id + " and StatusFlag=1";
                cmd = new SqlCommand(query, con);
                con.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    con.Close();
                    return null;
                }
                else
                {
                    Cart c = new Cart((int)cmd.ExecuteScalar());
                    con.Close();
                    return c;
                }
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public List<Cart> GetCarts()
        {
            List<Cart> l = new List<Cart>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select CartId from CARTS where BuyerId="+id;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(new Cart((int)reader[0]));
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
        public static List<Buyer> GetAllBuyers()
        {
            List<Buyer> l = new List<Buyer>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select BuyerId from BUYERS";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l.Add(new Buyer((int)reader[0]));
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
        public bool Delete()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "DELETE FROM [dbo].BUYERS WHERE BuyerId = " + id;
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