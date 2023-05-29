using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace VPXRMA
{
    public partial class User : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindgrid();
            }
           
        }
        public void bindgrid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Sp_User_Select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            gv_CreateUser.DataSource = dt;
            gv_CreateUser.DataBind();

        }
        protected void btn_save_Click(object sender, EventArgs e)
        {

            if (btn_save.Text == "Save")
            {
                if ((txtUserName.Text == null) || (txtUserName.Text == " "))
                {
                    string message = "Please fill User Name.";
                    string script = "window.onload = function(){ alert('";
                    script += message;
                    script += "')};";
                    ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

                }
                else
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_User_Create", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", txtUserId.Text);
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@EmailId", txtemail.Text);
                    cmd.Parameters.AddWithValue("@MobileNo", txtmobileno.Text);
                    cmd.Parameters.AddWithValue("@UserType", ddl_Status.SelectedItem.Text);
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    bindgrid();
                }
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Sp_User_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", ViewState["Id"]);
                cmd.Parameters.AddWithValue("@UserId", txtUserId.Text);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                cmd.Parameters.AddWithValue("@EmailId", txtemail.Text);
                cmd.Parameters.AddWithValue("@MobileNo", txtmobileno.Text);
                cmd.Parameters.AddWithValue("@UserType", ddl_Status.SelectedItem.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                bindgrid();
                clear();
            }
            clear();
        }

        //Function for clearing  values of fields
        public void clear()
        {
            btn_save.Text = "Save";
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtRemarks.Text = "";
            txtmobileno.Text = "";
            ddl_Status.ClearSelection();
            txtemail.Text = "";

        }


        //Function for changing row  values of  Grid
        protected void gv_CreateUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "abc")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from tbl_User where Id='" + e.CommandArgument + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                bindgrid();

            }

            if (e.CommandName == "abc1")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tbl_User where Id='" + e.CommandArgument + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                txtUserId.Text = dt.Rows[0]["UserId"].ToString();
                txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
                txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
                txtmobileno.Text = dt.Rows[0]["MobileNo"].ToString();
                txtemail.Text = dt.Rows[0]["EmailId"].ToString();
                ddl_Status.Items.FindByValue(dt.Rows[0]["UserType"].ToString());
                    
                btn_save.Text = "Update";
                ViewState["Id"] = e.CommandArgument;
            }
        }

    }
}

