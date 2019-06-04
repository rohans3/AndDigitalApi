using AndDigital.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AndDigital.Controllers
{
    public class CustomerController : ApiController
    {


        [HttpGet]
        [ActionName("InsertDetails")]
        [Route("InsertDetails")]
        public bool InsertDetails(int id,string phone)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);

            string SqlString = "Insert into CustomerDetails (Customer_Id,PhoneNumber) values (" + id + "," + phone + ");";
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            Conn.Open();
            command = new SqlCommand(SqlString, Conn);

            adapter.InsertCommand = new SqlCommand(SqlString, Conn);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            Conn.Close();
            return true;

        }
        [HttpGet]
        [ActionName("GetCustomerPhoneByID")]
        [Route("GetCustomerPhoneByID")]
        public Customer GetCustomer(int id)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);

            string SqlString = "SELECT * FROM CustomerDetails WHERE Customer_Id =" + id + ";";
            SqlDataAdapter sda = new SqlDataAdapter(SqlString, Conn);
            DataTable dt = new DataTable();

            Conn.Open();
            sda.Fill(dt);
            Customer cust = new Customer();
            List<string[]> myTable = new List<string[]>();
            cust.PhoneNumber = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cust.PhoneNumber[i] = dt.Rows[i].ItemArray[1].ToString();
            }


            return cust;

        }

        [HttpGet]
        [ActionName("GetCustomerPhone")]
        [Route("GetCustomerPhone")]
        public Customer GetPhone()
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);

            string SqlString = "SELECT PhoneNumber FROM CustomerDetails;";
            SqlDataAdapter sda = new SqlDataAdapter(SqlString, Conn);
            DataTable dt = new DataTable();

            Conn.Open();
            sda.Fill(dt);
            Customer cust = new Customer();
            List<string[]> myTable = new List<string[]>();
            cust.PhoneNumber = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cust.PhoneNumber[i] = dt.Rows[i].ItemArray[0].ToString();
            }


            return cust;

        }

        [HttpGet]
        [ActionName("SetPhoneActive")]
        [Route("SetPhoneActive")]
        public bool SetPhoneActive(string phone)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            SqlConnection Conn = new SqlConnection(ConnStr);

            string SqlString = "Update CustomerDetails set isActive = 1 where  PhoneNumber = " + phone + ";";
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            Conn.Open();
            command = new SqlCommand(SqlString, Conn);

            adapter.InsertCommand = new SqlCommand(SqlString, Conn);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            Conn.Close();
            return true;

        }
    }

}
