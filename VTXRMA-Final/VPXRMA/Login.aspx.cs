using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace VPXRMA
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["const88"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_signin_Click(object sender, EventArgs e)
        {

            //try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_login_check", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txt_email.Text);
                cmd.Parameters.AddWithValue("@Password", txt_password.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //SqlDataReader rd = new SqlDataReader();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    string Name = dt.Rows[0]["UserName"].ToString();
                    string Role = dt.Rows[0]["UserType"].ToString();
                    FormsAuthentication.SetAuthCookie(
                   Name, false);

                    FormsAuthenticationTicket ticket1 =
                       new FormsAuthenticationTicket(
                            1,                                   // version
                            Name,   // get username  from the form
                            DateTime.Now,                        // issue time is now
                            DateTime.Now.AddMinutes(30),         // expires in 10 minutes
                            false,      // cookie is not persistent
                            Role                            // role assignment is stored
                                                            // in userData
                            );
                    HttpCookie cookie1 = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(ticket1));

                    Response.Cookies.Add(cookie1);


                    Session["UserName"] = dt.Rows[0]["UserName"].ToString();
                    Session["Role"] = dt.Rows[0]["UserType"].ToString();

                    Response.Redirect("Dashboard.aspx");

                }

                else
                {
                    Response.Write("<script>alert('Login Falied!!')</script>");
                }
            }

        }

        private void Hide()
        {
            throw new NotImplementedException();
        }

    }
}