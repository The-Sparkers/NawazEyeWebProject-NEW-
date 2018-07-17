using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace NawazEyeWebProject_NEW_.Models
{
    public class PrescriptionGlasses : Product
    {
        Lens lens;
        Frame frame;
        SqlConnection con;
        SqlCommand cmd;
        string query;
        public PrescriptionGlasses(int id)
        {
            SetValues(id);
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                query = "select LensId, FrameId from PRESCRIPTION_GLASSES where ProductId=" + id;
                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lens = new Lens((int)reader[0]);
                    frame = new Frame((int)reader[1]);
                }
                con.Close();
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public PrescriptionGlasses(string name, decimal price, int discount, int quantity, string frameColor, string productDescription, bool stopOrder, Lens lens, Frame frame)
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
                query = "INSERT INTO [PRESCRIPTION_GLASSES] ([ProductId] ,[LensId] ,[FrameId]) VALUES (" + id + " ," + lens.LensId + " ," + frame.FrameId+")";
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
        public Frame Frame
        {
            get
            {
                return frame;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "UPDATE PRESCRIPTION_GLASSES set FrameId=" + frame.FrameId + " where ProductId=" + id;
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
        public Lens Lens
        {
            get
            {
                return lens;
            }
            set
            {
                try
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                    query = "UPDATE PRESCRIPTION_GLASSES set LensId=" + lens.LensId + " where ProductId=" + id;
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
        public static List<PrescriptionGlasses> GetAllPrescriptionGlasses()
        {
            List<PrescriptionGlasses> lst = new List<PrescriptionGlasses>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select s.ProductId from PRESCRIPTION_GLASSES s, PRODUCTS p where p.ProductId=s.ProductId and p.StopOrder=0";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lst.Add(new PrescriptionGlasses((int)reader[0]));
                }
                con.Close();
                return lst;
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
        public static List<PrescriptionGlasses> FeaturedPrescriptionGlasses()
        {
            List<PrescriptionGlasses> lst = new List<PrescriptionGlasses>();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conString"].ConnectionString);
                string query = "select Top(4) ProductId from PRESCRIPTION_GLASSES order by ProductId desc";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lst.Add(new PrescriptionGlasses((int)reader[0]));
                }
                con.Close();
                return lst;
            }
            catch (SqlException ex)
            {
                Exception e = new Exception("Database Connection Error. " + ex.Message);
                throw e;
            }
        }
    }

}