using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace NawazEyeWebProject_NEW_.Models
{
    public class PromoCode
    {
        SqlConnection con;
        SqlCommand cmd;
        string query;
        int id;
        string code;
        int discount;
        public PromoCode(int id)
        {
            SetValues(id);
        }
        public PromoCode(string code)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select PromoId from PROMO_CODES where Code='" + code + "'";
                cmd = new SqlCommand(query, con);
                con.Open();
                SetValues((int)cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public PromoCode(int discount, bool flag=true)
        {
            int flagCount = 0;
            again:
            code = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] selection = code.ToCharArray();
            Random rand = new Random();
            char[] s = new char[5];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = selection[rand.Next(0, selection.Length)];
            }
            code = s.ToString();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "insert into PROMO_CODES (Code, Discount) values ('" + code + "'," + discount + "); Select MAX(PromoId) from PROMO_CODES;"; 
                cmd = new SqlCommand(query, con);
                con.Open();
                id = (int)cmd.ExecuteScalar();
                con.Close();
                SetValues(id);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 2601 && flagCount<5)
                {
                    flagCount++;
                    goto again;
                }
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public int PromoId
        {
            get
            {
                return id;
            }
        }
        public string Code
        {
            get
            {
                return code;
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
                    query = "update PROMO_CODES set Discount=" + value + " where PromoId=" + id;
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
        public bool IsUsed(Account buyerAccount)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select * from ACCOUNT_USE_PROMOS where PromoId=" + id + " and BuyerId=" + buyerAccount.Buyer.BuyerId; 
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public decimal GetDiscountedPrice(Cart cart)
        {
            decimal t = cart.TotalPrice * (Discount / 100);
            t = cart.TotalPrice - t;
            return t;
        }
        private void SetValues(int id)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select * from PROMO_CODES where PromoId=" + id;
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.id = (int)reader[0];
                    code = (string)reader[1];
                    discount = (int) reader[2];
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public static List<PromoCode> GetAllPromos()
        {
            try
            {
                List<PromoCode> list = new List<PromoCode>();
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select PromoId from PROMO_CODES";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PromoCode((int)reader[0]));
                }
                return list;
            }
            catch(SqlException ex)
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
                string query = "DELETE FROM [dbo].PROMO_CODES WHERE PromoId = " + id;
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