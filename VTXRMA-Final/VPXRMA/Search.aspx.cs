using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;





namespace VPXRMA
{
    public partial class Search : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binduser();
                
            }
        }
        public void binduser()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select UserName from tbl_User", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlUserName.DataSource = dt;
            ddlUserName.DataBind();
            ddlUserName.DataTextField = "UserName";
            ddlUserName.DataValueField = "UserName";
            ddlUserName.DataBind();
            con.Close();
            ddlUserName.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        


        protected void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {

                string strcmd = "";

                strcmd = "Select UserId,UserName,Password,Remarks  from tbl_User where 1=1";

                if (ddlUserName.SelectedItem.Text != "--Select--")
                {

                    strcmd = strcmd + " and UserName like '%" + ddlUserName.SelectedItem.Text + "%'";

                }
               
                strcmd = strcmd + " group by UserId,UserName,Password,Remarks  order by  UserId,UserName,Password,Remarks";
                con.Open();
                SqlCommand command = new SqlCommand(strcmd, con);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                con.Close();
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {

                con.Close();
            }
        }

    }

}
