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

namespace VPXRMA
{
    public partial class RMASearch : System.Web.UI.Page
    {
        SqlConnection conmesp = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExportToExcel();
                //bindimages();
                //bindgrid();
                bindstatus();

                if (Request.QueryString["statusid"] != null && Request.QueryString["statusid"] != "")
                {
                    showData(Request.QueryString["statusid"].ToString());
                }
                else
                {
                     //bindgrid();
                }
            }

        }



        public void bindgrid()
        {
            conmesp.Open();
            SqlCommand cmd = new SqlCommand("Sp_RMAReq_select", conmesp);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conmesp.Close();
            grd_Search.DataSource = dt;
            grd_Search.DataBind();
        }
        //-------------------------- bind grid for export to excel ----------------

        //public void bindgrid1()
        //{
        //    conmesp.Open();
        //    SqlCommand cmd = new SqlCommand("Sp_RMAReq_select", conmesp);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dr = new DataTable();
        //    da.Fill(dr);
        //    conmesp.Close();
        //    grd_Search.DataSource = dr;
        //    grd_Search.DataBind();

        //}

        //---------------- EXPORT OT EXCEL----------------------
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        public void ExportToExcel()
        {
            if (grd_Search.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=report_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd_Search.RenderControl(hw);
                System.Text.StringBuilder sbResponseString = new System.Text.StringBuilder();
                sbResponseString.Append("<html xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:xlExcel8\" xmlns=\"http://www.w3.org/TR/REC-html40\"> <head><style> table { mso-number-format:@;} </style></head> <body>");
                sbResponseString.Append(sw + "</body></html>");
                Response.Write(sbResponseString.ToString());
                Response.End();

                Response.Write(sw.ToString());
                Response.Flush();
                Response.End();


            }
            else
            {
                //lblAck.Text = "Nothing to export!";
                //lblAck.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void showData(string statusid)
        {
            try
            {
                conmesp.Open();
                SqlCommand cmd = new SqlCommand("select * from tbl_RMAReq where status='" + statusid.ToString() + "'", conmesp);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                grd_Search.DataSource = dt;
                grd_Search.DataBind();

                conmesp.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        ////public void bindimages()
        ////{
        ////    string[] ImagePaths = Directory.GetFiles(Server.MapPath("~/Uploads/"));
        ////    List<ListItem> Imgs = new List<ListItem>();
        ////    foreach (string imgPath in ImagePaths)
        ////    {
        ////        string ImgName = Path.GetFileName(imgPath);
        ////        Imgs.Add(new ListItem(ImgName, "~/Uploads/" + ImgName));
        ////    }
        ////    grd_Search.DataSource = Imgs;
        ////    grd_Search.DataBind();
        ////}

        public void bindstatus()
        {
            conmesp.Open();
            SqlCommand cmd = new SqlCommand("Sp_status_select", conmesp);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlStatus.DataSource = dt;
            //ddl_Status.DataSource = dt;

            ddlStatus.DataTextField = "status";
            ddlStatus.DataValueField = "id";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("--Select--", "0"));
            //conmesp.Close();

            ddl_Status.DataSource = dt;
            ddl_Status.DataTextField = "status";
            ddl_Status.DataValueField = "id";
            ddl_Status.DataBind();
            ddl_Status.Items.Insert(0, new ListItem("--Select--", "0"));
            conmesp.Close();

        }


        protected void btn_Search_Click(object sender, EventArgs e)
        {



            if (btn_Search.Text == "Search")
            {


                if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
                {
                    btn_Allcheck.Visible = true;

                }
                else
                {
                    btn_Allcheck.Visible = false;
                }

                string q = "";

                q = "select * from tbl_RMAReq where 1=1 ";

                int i = 0;
                if (!string.IsNullOrEmpty(txtSrNo.Text))
                {
                    i = 1;
                    q = q + " and SrNo like '%" + txtSrNo.Text + "%'";
                }


                if (!string.IsNullOrEmpty(txtmodel.Text))
                {
                    i = 1;
                    q = q + " and Model like '%" + txtmodel.Text + "%'";
                }

                if (!string.IsNullOrEmpty(txtentrydate.Text))
                {
                    i = 1;
                    q = q + " and EntryDate like '%" + DateTime.Now + "%'";
                }
                if (ddlStatus.SelectedItem.Text != "--Select--")
                {
                    i = 1;
                    q = q + " and " + "( " + "status = '" + ddlStatus.SelectedItem.Text + "'" + " OR " + "status IS NULL" + " OR " + "status = '' )";
                }
                if (i == 0)
                {
                    lblMsg.Text = "Please enter at least one search term.";
                    lblMsg.Visible = true;
                }
                else
                {
                    string q1 = q + "AND(EntryBy = '" + Session["UserName"].ToString() + "' or '" + Session["Role"].ToString() + "' = 'Admin')";
                    SqlCommand sqlcmd = new SqlCommand(q1, conmesp);
                    conmesp.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    SqlDataReader sdr = sqlcmd.ExecuteReader();
                    if (sdr.Read())
                    {

                        grd_Search.DataSource = dt;
                        grd_Search.DataBind();
                        grd_Search.Visible = true;
                        txtSrNo.Text = "";
                        txtmodel.Text = "";

                        //ddlStatus.ClearSelection();
                        //txtref_no.Text = "";
                        lblMsg.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = "No Records Found...!!";
                        grd_Search.Visible = false;
                        lblMsg.Visible = true;
                    }
                }

            }

        }

        
        
      
            
       
        protected void grd_Search_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //if (e.CommandName == "abc")
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand("delete from mst_User where id='" + e.CommandArgument + "' ", con);
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    bindgrid();
            //}
            if (e.CommandName == "abc1")
            {
                Response.Redirect("RMA_.aspx?rid=" + e.CommandArgument);
            }
        }

        //click event of page indexing of grid view
        protected void grd_Search_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_Search.PageIndex = e.NewPageIndex;
            this.bindgrid();

            lblMsg.Text = "";
            lblMsg.Visible = false;
        }


        protected void btn_Allcheck_Click(object sender, EventArgs e)
        {
            if ((grd_Search.Rows.Count) > 0)
            {
                if (btn_Allcheck.Text == "Check All")
                {
                    foreach (GridViewRow row in grd_Search.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("MyCheckBox");
                        chk.Checked = true;
                    }
                    btn_Allcheck.Text = "Uncheck All";
                }
                else
                {
                    foreach (GridViewRow row in grd_Search.Rows)
                    {
                        CheckBox chk = (CheckBox)row.FindControl("MyCheckBox");
                        chk.Checked = false;
                    }
                    btn_Allcheck.Text = "Check All";
                }
            }
            else
            {

            }
            bool isAnyRowChecked = false;

            // Check if any row in the GridView is checked
            foreach (GridViewRow row in grd_Search.Rows)
            {
                CheckBox chkRow = (CheckBox)row.FindControl("MyCheckBox");
                if (chkRow.Checked)
                {
                    isAnyRowChecked = true;
                    break;
                }
            }

            // Show or hide the DataPanel based on the checkbox status
            if (isAnyRowChecked)
            {
                Panelallchecked.Visible = true;
            }
            else
            {
                Panelallchecked.Visible = false;
            }
        }


        protected void BindGridView()
        {
            // GridView ko bind karne ka code yahan likhein
            // Example:
            conmesp.Open();
            SqlCommand cmd = new SqlCommand("Sp_status_select", conmesp);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conmesp.Close();
            grd_Search.DataSource = dt;
            grd_Search.DataBind();
        }
        protected DataTable GetGridData()
        {
            // Yahan aapki grid data source se data fetch karne ka code likhein
            // Example:
            DataTable dt = new DataTable();
            dt.Columns.Add("Remark");
            dt.Columns.Add("Status");

            // Dummy data for example
            dt.Rows.Add("Remark 1", "Status 1");
            dt.Rows.Add("Remark 2", "Status 2");

            return dt;
        }



        protected void btnSave1_Click(object sender, EventArgs e)
        {
            //DataTable dt = GetGridData();
            //dt.Rows.Add(txt_Remarks.Text, ddl_Status.Text);
            //DataRow newRow = dt.NewRow();
            //newRow["Remark"] = txt_Remarks.Text;
            //newRow["Status"] = ddl_Status.Text;
            //dt.Rows.Add(newRow);

            ////// Clear input fields
            ////txt_Remarks.Text = string.Empty;
            ////ddlStatus.Text = string.Empty;

            //// Rebind GridView
            //BindGridView();
            //// Save button ke click hone par, textboxes ki values clear kar dein
            ////txt_Remarks.Text = string.Empty;
            ////ddlStatus.Text = string.Empty;

            //BindGridView(); // GridView ko refresh karein, updated data ko dikhane ke liye



            foreach (GridViewRow row in grd_Search.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("MyCheckBox");
                if (chk.Checked)
                {
                    //int idValue = Convert.ToInt32(grd_Search.DataKeys[row.RowIndex].Value);
                    string idValue = row.Cells[2].Text;
                
                 //idValue = grd_Search.DataKeys[row.RowIndex].Value.ToString();

                string query1 = "INSERT INTO tbl_status_log(FileRefNo, Status,Remarks,UserName,UserDate) " +
                    "values ('" + idValue.ToString() + "','" + ddl_Status.SelectedItem.Text + "','" + txt_Remarks.Text + "','" + Session["UserName"].ToString() + "','" + DateTime.Now + "')";

                string query2 = "UPDATE tbl_RMAReq SET status = '" + ddl_Status.SelectedItem.Text + "' WHERE id = " + idValue;

                // Create SQL command object
                using (SqlCommand cmd = new SqlCommand(query1, conmesp))
                {
                    conmesp.Open();
                    // Execute SQL command
                    cmd.ExecuteNonQuery();
                    conmesp.Close();
                }

                using (SqlCommand cmd = new SqlCommand(query2, conmesp))
                {
                    conmesp.Open();
                    // Execute SQL command
                    //cmd.ExecuteNonQuery();
                    conmesp.Close();
                }

                conmesp.Dispose();

                }
                else
                {
                    return;
                }
            }




        }


        protected void txtexport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
    }
    
    
}


