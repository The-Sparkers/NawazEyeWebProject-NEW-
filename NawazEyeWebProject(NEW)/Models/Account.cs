using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NawazEyeWebProject_NEW_.Models
{
    public class Account
    {
        SqlConnection con;
        SqlCommand cmd;
        string query;
        int buyerId, accessfailedCount;
        DateTime lockoutEnd;
        string username, password, acntId, email, securityStamp, phoneNumber;
        bool blockFlag, emailCnfrm, phoneCnfrm, twoFactEn, lockoutEn;
        public Account(int buyerId)
        {
            this.buyerId = buyerId;
            GetAccountByBuyerId(buyerId);
            SetValues(acntId);
        }
        public Account(string AccountId)
        {
            SetValues(AccountId);
        }
       /* public Account(string userName, string password)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "INSERT INTO [ACCOUNTS] ([BuyerId] ,[Username] ,[Password]) VALUES (" + id + " ,'" + userName + "' ,'" + password + "'); Select MAX(BuyerId) from ACCOUNTS;";
                cmd = new SqlCommand(query, con);
                con.Open();
                id = (int)cmd.ExecuteScalar();
                con.Close();
                SetValues(id);
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == 2601)
                {
                    Exception e = new Exception("The Username is already Taken");
                    throw e;
                }
                else
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }*/
        public Buyer Buyer
        {
            get
            {
                int id;
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "select buyerId from buyerHasAccount where AccountId='" + AccountId + "'";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    id = (int)cmd.ExecuteScalar();
                    con.Close();
                    return new Buyer(id);

                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
        }
        public string AccountId
        {
            get
            {
                return acntId;
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
        }
        public bool IsBlocked
        {
            get
            {
                return blockFlag;
            }
            set
            {
                try
                {
                    int i = Convert.ToInt16(value);
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "update AspNetUsers set IsBlocked=" + i + " where Id='" + AccountId + "'";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SetValues(AccountId);
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
                Buyer.Email = value;
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "update AspNetUsers set Email='" + value + "' where Id='" + AccountId + "'";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    SetValues(AccountId);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    Exception e = new Exception("Database Connection Error. " + ex.Message);
                    throw e;
                }
            }
          
        }
        public bool EmailConfirmed
        {
            get
            {
                return emailCnfrm;
            }
        }
        public string SecurityStamp
        {
            get
            {
                return securityStamp;
            }
        }
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
        }
        public bool PhoneNumberConfirmed
        {
            get
            {
                return phoneCnfrm;
            }
        }
        public bool TwoFactorEnabled
        {
            get
            {
                return twoFactEn;
            }
        }
        public int AccessFailedCount
        {
            get
            {
                return accessfailedCount;
            }
        }
        public int GetNumberOfOrders()
        {
            int i = 0;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select count(*) from CARTS where BuyerId=" + buyerId + " and StatusFlag=1"; 
                cmd = new SqlCommand(query, con);
                con.Open();
                i = (int)cmd.ExecuteScalar();
                con.Close();
                return i;
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        private void SetValues(string AccountId)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select * from AspNetUsers where Id='" + AccountId + "'";
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    acntId = (string)reader[0];
                    email = (string)reader[1];
                    emailCnfrm = (bool)reader[2];
                    password = (string)reader[3];
                    securityStamp = (string)reader[4];
                    phoneNumber = (string)reader[5];
                    phoneCnfrm = (bool)reader[6];
                    twoFactEn = (bool)reader[7];
                    lockoutEnd = (DateTime)reader[8];
                    lockoutEn = (bool)reader[9];
                    accessfailedCount = (int)reader[10];
                    blockFlag = (bool)reader[11];
                    username = (string)reader[12];
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
            catch (InvalidCastException)
            {
                lockoutEnd = DateTime.MaxValue;
            }
        }
        private void GetAccountByBuyerId(int buyerId)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select AccountId from BuyerHasAccount where buyerId=" + buyerId;
                cmd = new SqlCommand(query, con);
                con.Open();
                acntId = (string)cmd.ExecuteScalar();
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
                string query = "DELETE FROM [AspNetUsers] WHERE Id = '" + AccountId + "'";
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