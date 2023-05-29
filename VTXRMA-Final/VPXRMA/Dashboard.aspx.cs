using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Services;


namespace VPXRMA
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        SqlConnection conmesp = new SqlConnection(ConfigurationManager.ConnectionStrings["conmes"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();


            }
        }
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static string fetchPopuData(string val)
        {
            string str1 = "";
            if (int.Parse(val) == 1)
            {
                str1 = orderpoppending();
            }
            else if (int.Parse(val) == 2)
            {
                str1 = orderpopapproved();
            }
            else if (int.Parse(val) == 3)
            {
                str1 = orderpopnotapproved();
            }
            else if (int.Parse(val) == 4)
            {
                str1 = OnHold();
            }
           


            return str1;
        }
        public static string orderpoppending()
        {
            StringBuilder builderOppmodel = new StringBuilder();

            int j = 0;
            builderOppmodel.Append("<table border=\"1\" width=\"100%\" align=\"Center\"  bordercolor=\"Blue\">");

            builderOppmodel.Append("<tr bgcolor=\"#80d4ff\">");

            builderOppmodel.Append("<td style=\"text-align:center\">Sr.No</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Model</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Brand</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">mfgDate</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">PanelSrNo</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">OcNo</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Warrenty</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Current status</td>");


            builderOppmodel.Append("</tr>");

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                con.Open();
                string q = " SELECT SrNo,Model,Brand,MfgDate,PanelSrNo,OCNo,Warranty,status FROM tbl_RMAReq WHERE status LIKE '%pending'";
                SqlCommand cmd1 = new SqlCommand(q, con);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                j = 1;

                while (rd1.Read())
                {

                    builderOppmodel.Append("<tr>");

                    builderOppmodel.Append("<td style=\"text-align:center\">" + j.ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["SrNo"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Model"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Brand"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["MfgDate"].ToString() + "</td>");

                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["PanelSrNo"].ToString() + "</td>");

                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["OCNo"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Warranty"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["status"].ToString() + "</td>");

                    builderOppmodel.Append("</tr>");


                    j = j + 1;

                }

                con.Close();
                con.Dispose();
            }

            builderOppmodel.Append("<tr bgcolor=\"#ffcc99\">");

            builderOppmodel.Append("<td colspan=\"8\" style=\"text - align:Left\">Pending</td>");

            builderOppmodel.Append("</table>");

            return builderOppmodel.ToString();
        }
        public static string orderpopapproved()
        {
            StringBuilder builderOppmodel = new StringBuilder();

            int j = 0;
            builderOppmodel.Append("<table border=\"1\" width=\"100%\" align=\"Center\"  bordercolor=\"Blue\">");

            builderOppmodel.Append("<tr bgcolor=\"#80d4ff\">");

            builderOppmodel.Append("<td style=\"text-align:center\">Sr.No</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Model</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Brand</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">mfgDate</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">PanelSrNo</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">OcNo</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Warrenty</td>");
            builderOppmodel.Append("<td style=\"text-align:center\">Current status</td>");


            builderOppmodel.Append("</tr>");

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                con.Open();
                string q = " SELECT SrNo,Model,Brand,MfgDate,PanelSrNo,OCNo,Warranty,status FROM tbl_RMAReq WHERE status LIKE '%Approved'"; 
                SqlCommand cmd1 = new SqlCommand(q, con);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                j = 1;

                while (rd1.Read())
                {

                    builderOppmodel.Append("<tr>");

                    builderOppmodel.Append("<td style=\"text-align:center\">" + j.ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["SrNo"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Model"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Brand"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["MfgDate"].ToString() + "</td>");

                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["PanelSrNo"].ToString() + "</td>");

                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["OCNo"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Warranty"].ToString() + "</td>");
                    builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["status"].ToString() + "</td>");

                    builderOppmodel.Append("</tr>");


                    j = j + 1;

                }

                con.Close();
                con.Dispose();
            }

            builderOppmodel.Append("<tr bgcolor=\"#ffcc99\">");

            builderOppmodel.Append("<td colspan=\"8\" style=\"text - align:Left\">WIP</td>");

            builderOppmodel.Append("</table>");

            return builderOppmodel.ToString();
        }
        public static string orderpopnotapproved()
        {
           
                StringBuilder builderOppmodel = new StringBuilder();

                int j = 0;
                builderOppmodel.Append("<table border=\"1\" width=\"100%\" align=\"Center\"  bordercolor=\"Blue\">");

                builderOppmodel.Append("<tr bgcolor=\"#80d4ff\">");

                builderOppmodel.Append("<td style=\"text-align:center\">Sr.No</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Model</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Brand</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">mfgDate</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">PanelSrNo</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">OcNo</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Warrenty</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Current status</td>");


                builderOppmodel.Append("</tr>");

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                {
                    con.Open();
                string q = " SELECT SrNo,Model,Brand,MfgDate,PanelSrNo,OCNo,Warranty,status FROM tbl_RMAReq WHERE status LIKE '%Not approved'";
                SqlCommand cmd1 = new SqlCommand(q, con);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    j = 1;

                    while (rd1.Read())
                    {

                        builderOppmodel.Append("<tr>");

                        builderOppmodel.Append("<td style=\"text-align:center\">" + j.ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["SrNo"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Model"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Brand"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["MfgDate"].ToString() + "</td>");

                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["PanelSrNo"].ToString() + "</td>");

                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["OCNo"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Warranty"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["status"].ToString() + "</td>");

                        builderOppmodel.Append("</tr>");


                        j = j + 1;

                    }

                    con.Close();
                    con.Dispose();
                }

                builderOppmodel.Append("<tr bgcolor=\"#ffcc99\">");

                builderOppmodel.Append("<td colspan=\"8\" style=\"text - align:Left\">Not Approved</td>");

                builderOppmodel.Append("</table>");

                return builderOppmodel.ToString();
            }
        public static string OnHold()

        {
            
                StringBuilder builderOppmodel = new StringBuilder();

                int j = 0;
                builderOppmodel.Append("<table border=\"1\" width=\"100%\" align=\"Center\"  bordercolor=\"Blue\">");

                builderOppmodel.Append("<tr bgcolor=\"#80d4ff\">");

                builderOppmodel.Append("<td style=\"text-align:center\">Sr.No</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Model</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Brand</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">mfgDate</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">PanelSrNo</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">OcNo</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Warrenty</td>");
                builderOppmodel.Append("<td style=\"text-align:center\">Current status</td>");


                builderOppmodel.Append("</tr>");

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                {
                    con.Open();
                string q = "SELECT SrNo,Model,Brand,MfgDate,PanelSrNo,OCNo,Warranty,status FROM tbl_RMAReq WHERE status LIKE '%OnHold'";
                SqlCommand cmd1 = new SqlCommand(q, con);
                    SqlDataReader rd1 = cmd1.ExecuteReader();
                    j = 1;

                    while (rd1.Read())
                    {

                        builderOppmodel.Append("<tr>");

                        builderOppmodel.Append("<td style=\"text-align:center\">" + j.ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["SrNo"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Model"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Brand"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["MfgDate"].ToString() + "</td>");

                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["PanelSrNo"].ToString() + "</td>");

                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["OCNo"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["Warranty"].ToString() + "</td>");
                        builderOppmodel.Append("<td style=\"text-align:center\">" + rd1["status"].ToString() + "</td>");

                        builderOppmodel.Append("</tr>");


                        j = j + 1;

                    }

                    con.Close();
                    con.Dispose();
                }

                builderOppmodel.Append("<tr bgcolor=\"#ffcc99\">");

                builderOppmodel.Append("<td colspan=\"8\" style=\"text - align:Left\">On Hold</td>");

                builderOppmodel.Append("</table>");

                return builderOppmodel.ToString();
            }

        void GetData()
        {
            //totalPending.InnerText = "0";
            //totalNew.InnerText = "0";
            //totalCancel.InnerText = "0";
            //Support.InnerText = "0";
            count_pending();
            count_approved();
            count_Notapproved();
            count_Onhold();

        }
        public void count_pending()


        {
            string str = "SELECT * FROM tbl_RMAReq WHERE status LIKE '%pending' AND (EntryBy ='" + Session["UserName"].ToString() + "' or '" + Session["Role"].ToString() + "'='Admin') ";
            count_status1(str);
        }
        public void count_status1(string st)
        {
            string stt = st;
            //string str1 = "select * from tbl_RMAReq where status like %pending";
            //string str2 = "select * from tbl_RMAReq where status like %approved"; 
            //string str3 = "select * from tbl_RMAReq where status like %not approved"; 
            //string str4 = "select * from tbl_RMAReq where status like %on hold";


            con.Open();
            SqlCommand command = new SqlCommand(stt, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            lblpending.InnerText = dt.Rows.Count.ToString();
            con.Close();
        }

        public void count_approved()
        {


            string str = "SELECT * FROM tbl_RMAReq WHERE status LIKE '%Approved' AND (EntryBy ='" + Session["UserName"].ToString() + "' or '" + Session["Role"].ToString() + "'='Admin') ";

            count_status2(str);
        }
        public void count_status2(string st)
        {
            string stt = st;
            //string str1 = "select * from tbl_RMAReq where status like %pending";
            //string str2 = "select * from tbl_RMAReq where status like %approved"; 
            //string str3 = "select * from tbl_RMAReq where status like %not approved"; 
            //string str4 = "select * from tbl_RMAReq where status like %on hold";


            con.Open();
            SqlCommand command = new SqlCommand(stt, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            lblapproved.InnerText = dt.Rows.Count.ToString();
            con.Close();

        }
        public void count_Notapproved()
        {
            string str = "SELECT * FROM tbl_RMAReq WHERE status LIKE '%Not approved' AND (EntryBy ='" + Session["UserName"].ToString() + "' or '" + Session["Role"].ToString() + "'='Admin') ";


            count_status3(str);
        }

        public void count_status3(string st)
        {
            string stt = st;



            con.Open();
            SqlCommand command = new SqlCommand(stt, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            lblNotApproved.InnerText = dt.Rows.Count.ToString();
            con.Close();
        }
        public void count_Onhold()
        {
            string str = "SELECT * FROM tbl_RMAReq WHERE status LIKE '%OnHold' AND (EntryBy ='" + Session["UserName"].ToString() + "' or '" + Session["Role"].ToString() + "'='Admin') ";



            count_status4(str);

        }
        public void count_status4(string st)
        {
            string stt = st;
            //string str1 = "select * from tbl_RMAReq where status like %pending";
            //string str2 = "select * from tbl_RMAReq where status like %approved"; 
            //string str3 = "select * from tbl_RMAReq where status like %not approved"; 
            //string str4 = "select * from tbl_RMAReq where status like %on hold";


            con.Open();
            SqlCommand command = new SqlCommand(stt, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            lblOnHold.InnerText = dt.Rows.Count.ToString();
            con.Close();
        }

        protected void lbtotalPending_Click(object sender, EventArgs e)
        {
            //ViewState["statusid"] = "Pending";
            Response.Redirect("RMASearch.aspx?statusid=" + "Pending");
        }

        protected void lbtotalApproved_Click(object sender, EventArgs e)
        {
            Response.Redirect("RMASearch.aspx?statusid=" + "Approved");

        }

        protected void lbtotalNotApproved_Click(object sender, EventArgs e)
        {
            Response.Redirect("RMASearch.aspx?statusid=" + "Not Approved");
        }

        protected void lbtotalOnHold_Click(object sender, EventArgs e)
        {
            Response.Redirect("RMASearch.aspx?statusid=" + "On Hold");
        }
    }
}