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
using System.Net;
using System.Net.Mail;



namespace VPXRMA
{
    public partial class RMARequest : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        SqlConnection conmesp = new SqlConnection(ConfigurationManager.ConnectionStrings["conmes"].ConnectionString);
        SqlConnection con88 = new SqlConnection(ConfigurationManager.ConnectionStrings["const88"].ConnectionString);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                check();

                if (Request.QueryString["rid"] != null && Request.QueryString["rid"].ToString() != "")
                {
                    ddl_Status.ClearSelection();
                    getstatus();
                    Edit();
                    statusPanel.Visible = true;
                    btnpanel.Visible = true;
                    pnlImage.Visible = false;
                    statusPanel.Visible = false;
                    btn_save.Visible = false;
                    pnlBrand.Enabled = false;
                    Panel1.Enabled = false;
                    PanelWarranty.Enabled = false;
                    jobid();

                }
            }
        }
       
     
       

        protected void txtSr_NO_TextChanged(object sender, EventArgs e)
        {


            try
            {
                //string strcmd = "";
                if (txt_SrNO.Text != "")
                {
                    //strcmd = @"select  Brand, Model, Line_No,+''''+" + " Sr_No as Sr_No,  Line_In_Date,   Line_Out_Date,   + '''' + " + "brdsrno as brdsrno ,  MI_In_Date,  MI_Out_Date,   SMT_Out_Date,+''''+" + "  pnlsrno as pnlsrno,   UserDate,  + '''' + " + "Macid as Macid from Newreporttable where sr_no like '%" + txt_SrNO.Text + "%' or brdsrno like '%" + txt_SrNO.Text + "%' or pnlsrno like '%" + txt_SrNO.Text + "%'  or Macid like '%" + txt_SrNO.Text + "%' ";
                    con88.Open();
                    SqlCommand cmd = new SqlCommand("usp_Newreporttable", con88);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Action", "Select");
                    cmd.Parameters.AddWithValue("@Sr_No", txt_SrNO.Text);
                    cmd.Parameters.AddWithValue("@brdsrno", txt_SrNO.Text);
                    cmd.Parameters.AddWithValue("@pnlsrno", txt_SrNO.Text);
                    cmd.Parameters.AddWithValue("@Macid", txt_SrNO.Text);

                    //SqlCommand cmd = new SqlCommand(strcmd, con88);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlDataAdapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        txtBrand.Text = dt.Rows[0]["Brand"].ToString();
                        txtmodel.Text = dt.Rows[0]["Model"].ToString();
                        txt_Board_Sr_No.Text = dt.Rows[0]["brdsrno"].ToString();
                        txt_PanelSr_No.Text = dt.Rows[0]["pnlsrno"].ToString();
                        //txt_Mf_Date.Text = dt.Rows[0]["Line_Out_Date"].ToString();
                        txt_Mf_Date.Text = (Convert.ToDateTime(dt.Rows[0]["Line_Out_Date"]).ToString("yyyy-MM-dd"));
                        if (DateTime.Now.AddYears(-1) < DateTime.Parse(dt.Rows[0]["Line_Out_Date"].ToString()))
                        {
                            checkbox3.Checked = true;
                            check();
                            //checkbox4.Checked = false;
                            //PanelWarranty.Visible = checkbox3.Checked;
                        }
                        else
                        {
                            checkbox3.Checked = false;
                            check();
                            //checkbox4.Checked = true;
                            //PanelWarranty.Visible = checkbox3.Checked;
                        }

                        grd_RMA.Visible = true;
                        grd_RMA.DataSource = dt;
                        grd_RMA.DataBind();

                        txtBrand.Enabled = false;
                        txtmodel.Enabled = false;
                        txtOC_No.Enabled = false;
                        txt_Board_Sr_No.Enabled = false;
                        txt_PanelSr_No.Enabled = false;
                        txt_Mf_Date.Enabled = false;

                        lblMsg.Text = "Total " + dt.Rows.Count.ToString() + " record(s) found";
                        lblMsg.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                    }

                    else
                    {
                        grd_RMA.Visible = false;
                        txtBrand.Text = "";
                        txtmodel.Text = "";
                        txtOC_No.Text = "";
                        txt_Board_Sr_No.Text = "";
                        txt_PanelSr_No.Text = "";
                        txt_Mf_Date.Text = "";
                        checkbox3.Checked = false;
                        check();

                        lblMsg.Text = "Total " + dt.Rows.Count.ToString() + " record found";
                        lblMsg.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                    }

                    //rptTat.DataSource = dt;
                    //rptTat.DataBind();
                    //lblcount.Text = "Total Rows : " + dt.Rows.Count.ToString();


                }
                else
                {
                    Response.Write("<script>alert(' Please enter serial number')</script>");
                }
                //strcmd = @"select * from vwall where sr_no like '%"+txtSrNo.Text+"%'";

            }
            catch (Exception ex)
            {
                //lblAck.Text = ex.Message;
                //lblAck.ForeColor = System.Drawing.Color.Red;

            }
            finally
            {
                con88.Close();
            }


        }

        public string CheckRecord()
        {


            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
                {
                    //using (SqlCommand command = new SqlCommand(query, conn))
                    //{
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("SELECT top 1 id from tbl_RMAReq WHERE SrNo = '" + txt_SrNO.Text + "' order by id  desc", connection);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    //DataSet ds = new DataSet();
                    da.Fill(dt);

                    string RMAId = "";
                    if (((int)dt.Rows.Count) > 0)
                    {
                        RMAId = dt.Rows[0]["Id"].ToString();
                        return RMAId;
                    }
                    else
                    {
                        return RMAId;
                    }

                }
            }
            catch (Exception e1)
            {
                throw e1;
            }
            finally
            {
                con.Close();
            }

        }


        //protected void btnNext_Click(object sender, EventArgs e)
        //{
        //    Panel2.Visible = false;
        //    Panel1.Visible = true;
        //}


        public void jobid()
        {
            string var;
            string value = "vtx";
            string year = DateTime.Now.Year.ToString();
           int serialNumber = Convert.ToInt32(Request.QueryString["rid"].ToString());
            
            if (serialNumber < 9)
            {
             
                var = "00" + serialNumber.ToString();
            }
            else if (serialNumber < 100)
            {
                var = "0" + serialNumber.ToString();
            }
            
            else
            {
                var = serialNumber.ToString(); 
            }
        
            string jobID = value+"/"+year+"/"+var;

             txtjobid.Text = jobID; ;
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (btn_save.Text == "Save")
            {
                chkStatus();

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_rmareq_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);
                cmd.Parameters.AddWithValue("@Brand", txtBrand.Text);
                cmd.Parameters.AddWithValue("@Model", txtmodel.Text);
                cmd.Parameters.AddWithValue("@PanelSrNo", txt_PanelSr_No.Text);
                cmd.Parameters.AddWithValue("@OCNo", txtOC_No.Text);
                cmd.Parameters.AddWithValue("@BoardSrNo", txt_Board_Sr_No.Text);
                //cmd.Parameters.AddWithValue("@MfgDate", txt_Mf_Date.Text);
                if ((txt_Mf_Date.Text == null) || (txt_Mf_Date.Text == ""))
                {
                    cmd.Parameters.AddWithValue("@MfgDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@MfgDate", txt_Mf_Date.Text);
                }

                string strstatus = checkbox3.Checked ? "Yes" : "No";
                cmd.Parameters.AddWithValue("@Warranty", strstatus);
                cmd.Parameters.AddWithValue("@Problem", txt_Problem.Text);
                cmd.Parameters.AddWithValue("@status", ddl_Status.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                cmd.Parameters.AddWithValue("@EntryBy", Session["UserName"].ToString());
                cmd.Parameters.AddWithValue("@EntryDate", DateTime.Now);


                cmd.ExecuteNonQuery();
                con.Close();
                txt_SrNO.Enabled = false;

                txtBrand.Enabled = false;
                txtmodel.Enabled = false;
                txtOC_No.Enabled = false;
                txt_Board_Sr_No.Enabled = false;
                txt_PanelSr_No.Enabled = false;
                txt_Mf_Date.Enabled = false;

                checkbox3.Enabled = false;
                //checkbox4.Enabled = false;
                txt_Problem.Enabled = false;

                pnlImage.Visible = true;
                //Panel2.Visible = false;


                lblMsg.Text = "Record saved";
                lblMsg.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                LableSave.Text = "Upload related images.";
                LableSave.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel5();", true);

                grd_RMA.Visible = false;
                logdata();

            }




            else
            {
                con.Open();
                SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("Sp_RMAReq_Update", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id", Request.QueryString["rid"]);
                cmd1.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);
                cmd1.Parameters.AddWithValue("@Brand", txtBrand.Text);
                cmd1.Parameters.AddWithValue("@Model", txtmodel.Text);
                cmd1.Parameters.AddWithValue("@PanelSrNo", txt_PanelSr_No.Text);
                cmd1.Parameters.AddWithValue("@OCNo", txtOC_No.Text);
                cmd1.Parameters.AddWithValue("@BoardSrNo", txt_Board_Sr_No.Text);
                //cmd1.Parameters.AddWithValue("@MfgDate", txt_Mf_Date.Text);
                if ((txt_Mf_Date.Text == null) || (txt_Mf_Date.Text == ""))
                {
                    cmd1.Parameters.AddWithValue("@MfgDate", DBNull.Value);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@MfgDate", txt_Mf_Date.Text);
                }
                string strstatus = checkbox3.Checked ? "Yes" : "No";
                cmd1.Parameters.AddWithValue("@Warranty", strstatus);
                cmd1.Parameters.AddWithValue("@Problem", txt_Problem.Text);
                cmd1.Parameters.AddWithValue("@status", ddl_Status.SelectedItem.Text);
                cmd1.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());

                int result = cmd1.ExecuteNonQuery();
                con.Close();

                logdata();

                lblMsg.Text = "Record updated";
                lblMsg.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                if (result > 0)
                {
                    //btnResponse.Redirect("RMASearch.aspx");
                }
                statusPanel.Visible = false;
                btneditrecord.Visible = true;
                btnimage.Visible = true;
                btnstatus.Visible = true;
                btn_save.Visible = false;
                pnlBrand.Enabled = false;
                Panel1.Enabled = false;
                PanelWarranty.Enabled = false;


            }
        }


        public void logdata()
        {
            string Rid = CheckRecord();

            con.Open();
            string query = "INSERT INTO tbl_status_log (FileRefNo, Status, " +
                "Remarks,UserName,userDate,DocketNumber) VALUES (@FileRefNo, @Status, @Remarks,@UserName,@UserDate,@DocketNumber)";

            // Create SQL command object
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameters to the command object
                cmd.Parameters.AddWithValue("@FileRefNo", Rid.ToString());
                cmd.Parameters.AddWithValue("@Status", ddl_Status.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
                cmd.Parameters.AddWithValue("@UserName", Session["UserName"].ToString());
                cmd.Parameters.AddWithValue("@UserDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@DocketNumber", txtdocketnum.Text);

                // Execute SQL command
                cmd.ExecuteNonQuery();
                con.Close();


                // Display success message
                //lblMessage.Text = "Data saved successfully!";
            }

        }

        protected void Upload1(object sender, EventArgs e)
        {
            if (flupImage.HasFile)
            {
                string extension = System.IO.Path.GetExtension(flupImage.FileName);
                int fileSize = flupImage.PostedFile.ContentLength;
                if (fileSize > (2097152))
                {

                    LabelMessage1.Text = "Size of image is too large. Maximum file size permitted is " + 2 + " MB";
                    LabelMessage1.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel1();", true);
                }
                else
                {

                    if ((extension == ".jpg") || (extension == ".png") || (extension == ".jpeg") || (extension == ".JPG") || (extension == ".PNG") || (extension == ".JPEG"))
                    {

                        try
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                            con.Open();
                            string strID = "SELECT id,SrNo FROM tbl_RMAReq WHERE ID = (SELECT IDENT_CURRENT('tbl_RMAReq')) and SrNo = '" + txt_SrNO.Text + "'";
                            SqlCommand sqlcmd = new SqlCommand(strID, con);
                            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            string RMAid = dt.Rows[0]["id"].ToString();
                            con.Close();


                            string filename1 = Path.GetFileName(flupImage.PostedFile.FileName);
                            string rename1 = RMAid.ToString() + " pfi " + filename1;
                            flupImage.SaveAs(Server.MapPath("~/Uploads/" + rename1));
                            Session["image1"] = rename1;
                            con.Open();
                            string query = "Update tbl_RMAReq SET  Pf_Image = @Pf_Image where id = '" + RMAid + "' and SrNo = '" + txt_SrNO.Text + "'";
                            SqlCommand updateCommand = new SqlCommand(query);
                            updateCommand.Connection = con;
                            updateCommand.Parameters.AddWithValue("@Pf_Image", "Uploads/" + rename1);
                            //cmd = new SqlCommand("insert into tbl_RMAReq1 (SrNo,TVSrNo_Image) values(@SrNo,@TVSrNo_Image)", con);
                            //cmd.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);
                            //cmd.Parameters.AddWithValue("@TVSrNo_Image", "Uploads/" + filename);
                            updateCommand.ExecuteNonQuery();
                            LabelMessage1.Text = "Image Uploaded";
                            LabelMessage1.ForeColor = System.Drawing.Color.ForestGreen;

                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowImagePreview1()", true);

                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }


                    else
                    {
                        LabelMessage1.Text = "Only .jpg or .png or .jpeg allowed";
                        LabelMessage1.ForeColor = System.Drawing.Color.Red;
                    }

                }
            }
            else
            {
                LabelMessage1.Text = "Select a file to upload";
                LabelMessage1.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel1();", true);
            }

            lblMsg.Text = "";
            LableSave.Text = "";

        }

        protected void Upload2(object sender, EventArgs e)
        {
            if (flupImage2.HasFile)
            {
                string extension = System.IO.Path.GetExtension(flupImage2.FileName);
                int fileSize = flupImage2.PostedFile.ContentLength;
                if (fileSize > (2097152))
                {

                    LabelMessage2.Text = "Size of image is too large. Maximum file size permitted is " + 2 + " MB";
                    LabelMessage2.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel2();", true);
                }
                else
                {

                    if ((extension == ".jpg") || (extension == ".png") || (extension == ".jpeg") || (extension == ".JPG") || (extension == ".PNG") || (extension == ".JPEG"))
                    {

                    try
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                        con.Open();
                        string strID = "SELECT id,SrNo FROM tbl_RMAReq WHERE ID = (SELECT IDENT_CURRENT('tbl_RMAReq')) and SrNo = '" + txt_SrNO.Text + "'";
                        SqlCommand sqlcmd = new SqlCommand(strID, con);
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        string RMAid = dt.Rows[0]["id"].ToString();
                        con.Close();

                        string filename2 = Path.GetFileName(flupImage2.PostedFile.FileName);
                        string rename2 = RMAid.ToString() + " pbi " + filename2;
                        flupImage2.SaveAs(Server.MapPath("~/Uploads/" + rename2));

                        con.Open();
                        string query = "Update tbl_RMAReq SET  Pb_Image = @Pb_Image where id = '" + RMAid + "' and SrNo = '" + txt_SrNO.Text + "'";
                        SqlCommand updateCommand = new SqlCommand(query);
                        updateCommand.Connection = con;
                        updateCommand.Parameters.AddWithValue("@Pb_Image", "Uploads/" + rename2);
                        //cmd = new SqlCommand("insert into tbl_RMAReq1 (SrNo,TVSrNo_Image) values(@SrNo,@TVSrNo_Image)", con);
                        //cmd.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);
                        //cmd.Parameters.AddWithValue("@TVSrNo_Image", "Uploads/" + filename);
                        updateCommand.ExecuteNonQuery();
                        LabelMessage2.Text = "Image Uploaded";
                        LabelMessage2.ForeColor = System.Drawing.Color.ForestGreen;

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowImagePreview2()", true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    }
                    else
                    {
                        LabelMessage2.Text = "Only .jpg or .png or .jpeg allowed";
                       LabelMessage2.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }

            else
            {
                LabelMessage2.Text = "Select a file to upload";
                LabelMessage2.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel2();", true);
            }

            lblMsg.Text = "";
            LableSave.Text = "";
        }

        protected void Upload3(object sender, EventArgs e)
        {
            if (flupImage3.HasFile)
            {
                string extension = System.IO.Path.GetExtension(flupImage3.FileName);
                int fileSize = flupImage3.PostedFile.ContentLength;
                if (fileSize > (2097152))
                {

                    LabelMessage3.Text = "Size of image is too large. Maximum file size permitted is " + 2 + " MB";
                    LabelMessage3.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel3();", true);
                }
                else
                {

                    if ((extension == ".jpg") || (extension == ".png") || (extension == ".jpeg") || (extension == ".JPG") || (extension == ".PNG") || (extension == ".JPEG"))
                    {
                    try
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                        con.Open();
                        string strID = "SELECT id,SrNo FROM tbl_RMAReq WHERE ID = (SELECT IDENT_CURRENT('tbl_RMAReq')) and SrNo = '" + txt_SrNO.Text + "'";
                        SqlCommand sqlcmd = new SqlCommand(strID, con);
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        string RMAid = dt.Rows[0]["id"].ToString();
                        con.Close();

                        string filename3 = Path.GetFileName(flupImage3.PostedFile.FileName);
                        string rename3 = RMAid.ToString() + " di " + filename3;
                        flupImage3.SaveAs(Server.MapPath("~/Uploads/" + rename3));

                        con.Open();
                        string query = "Update tbl_RMAReq SET  Def_Image = @Def_Image where id = '" + RMAid + "' and SrNo = '" + txt_SrNO.Text + "'";
                        SqlCommand updateCommand = new SqlCommand(query);
                        updateCommand.Connection = con;
                        updateCommand.Parameters.AddWithValue("@Def_Image", "Uploads/" + rename3);
                        //cmd = new SqlCommand("insert into tbl_RMAReq1 (SrNo,TVSrNo_Image) values(@SrNo,@TVSrNo_Image)", con);
                        //cmd.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);
                        //cmd.Parameters.AddWithValue("@TVSrNo_Image", "Uploads/" + filename);
                        updateCommand.ExecuteNonQuery();
                        LabelMessage3.Text = "Image Uploaded";
                        LabelMessage3.ForeColor = System.Drawing.Color.ForestGreen;

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowImagePreview3()", true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    }
                   else
                    {
                       LabelMessage3.Text = "Only .jpg or .png or .jpeg allowed";
                        LabelMessage3.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            else
            {
                LabelMessage3.Text = "Select a file to upload";
                LabelMessage3.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel3();", true);
            }

            lblMsg.Text = "";
            LableSave.Text = "";
        }

        protected void Upload4(object sender, EventArgs e)

        {
            if (flupImage4.HasFile)
            {
                string extension = System.IO.Path.GetExtension(flupImage4.FileName);
                int fileSize = flupImage4.PostedFile.ContentLength;
                if (fileSize > (2097152))
                {

                    LabelMessage4.Text = "Size of image is too large. Maximum file size permitted is " + 2 + " MB";
                    LabelMessage4.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel4();", true);
                }
                else
                {
                    if ((extension == ".jpg") || (extension == ".png") || (extension == ".jpeg") || (extension == ".JPG") || (extension == ".PNG") || (extension == ".JPEG"))
                    {

                    try
                    {
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
                        con.Open();
                        string strID = "SELECT id,SrNo FROM tbl_RMAReq WHERE ID = (SELECT IDENT_CURRENT('tbl_RMAReq')) and SrNo = '" + txt_SrNO.Text + "'";
                        SqlCommand sqlcmd = new SqlCommand(strID, con);
                        SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        string RMAid = dt.Rows[0]["id"].ToString();
                        con.Close();

                        string filename4 = Path.GetFileName(flupImage4.PostedFile.FileName);
                        string rename4 = RMAid.ToString() + " tsi " + filename4;
                        flupImage4.SaveAs(Server.MapPath("~/Uploads/" + rename4));

                        con.Open();
                        string query = "Update tbl_RMAReq SET  TVSrNo_Image = @TVSrNo_Image where id = '" + RMAid + "' and SrNo = '" + txt_SrNO.Text + "'";
                        SqlCommand updateCommand = new SqlCommand(query);
                        updateCommand.Connection = con;
                        updateCommand.Parameters.AddWithValue("@TVSrNo_Image", "Uploads/" + rename4);
                        //cmd = new SqlCommand("insert into tbl_RMAReq1 (SrNo,TVSrNo_Image) values(@SrNo,@TVSrNo_Image)", con);
                        //cmd.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);
                        //cmd.Parameters.AddWithValue("@TVSrNo_Image", "Uploads/" + filename);
                        updateCommand.ExecuteNonQuery();
                        LabelMessage4.Text = "Image Uploaded";
                        LabelMessage4.ForeColor = System.Drawing.Color.ForestGreen;

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowImagePreview4()", true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    }
                    else
                    {
                        LabelMessage4.Text = "Only .jpg or .png or .jpeg allowed";
                        LabelMessage4.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            else
            {
                LabelMessage4.Text = "Select a file to upload";
                LabelMessage4.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel4();", true);
            }

            lblMsg.Text = "";
            LableSave.Text = "";
        }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Edit.aspx");



        }

        //
        public void Edit()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("mst_rma_edit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Request.QueryString["rid"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            txt_SrNO.Text = dt.Rows[0]["SrNO"].ToString();
            txtBrand.Text = dt.Rows[0]["Brand"].ToString();
            txtmodel.Text = dt.Rows[0]["Model"].ToString();
            txt_PanelSr_No.Text = dt.Rows[0]["PanelSrNo"].ToString();
            txtOC_No.Text = dt.Rows[0]["OCNo"].ToString();
            txt_Board_Sr_No.Text = dt.Rows[0]["BoardSrNo"].ToString();
            if (((dt.Rows[0]["MfgDate"]).ToString() == null) || ((dt.Rows[0]["MfgDate"]).ToString() == ""))
            {
                txt_Mf_Date.Text = "";
            }
            else
            {
                txt_Mf_Date.Text = (Convert.ToDateTime(dt.Rows[0]["MfgDate"]).ToString("yyyy-MM-dd"));
            }
            //txt_Mf_Date.Text = (Convert.ToDateTime(dt.Rows[0]["MfgDate"]).ToString("yyyy-MM-dd"));
            txt_Problem.Text = dt.Rows[0]["Problem"].ToString();
            string wr = dt.Rows[0]["warranty"].ToString();
            ddl_Status.SelectedItem.Text = dt.Rows[0]["Status"].ToString();
            if (wr == "Yes")
            {
                checkbox3.Checked = true;
            }
            else
            {
                checkbox3.Checked = false;
            }
            check();
            //checkbox3.Checked = (Convert.ToBoolean(dt.Rows[0]["Warranty"].ToString()));
            ImgPrv.ImageUrl = dt.Rows[0]["Pf_Image"].ToString();
            ImgPrv2.ImageUrl = dt.Rows[0]["Pb_Image"].ToString();
            ImgPrv3.ImageUrl = dt.Rows[0]["Def_Image"].ToString();
            ImgPrv4.ImageUrl = dt.Rows[0]["TvSrNo_Image"].ToString();

            pnlBrand.Visible = true;
            pnlImage.Visible = true;
            PanelWarranty.Visible = true;
            btn_save.Visible = true;

            btn_save.Text = "Update";
            UploadButton1.Text = "Update";
            UploadButton2.Text = "Update";
            UploadButton3.Text = "Update";
            UploadButton4.Text = "Update";

        }

        protected void checkbox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox3.Checked)
            {
                checkbox3.Checked = false;
            }
            else
            {

            }


        }

        protected void checkbox3_CheckedChanged(object sender, EventArgs e)
        {

            check();

        }

        public void check()
        {
            if (checkbox3.Checked)
            {
                checkbox3.Text = "&nbsp;" + " Yes";
            }
            else
            {
                checkbox3.Text = "&nbsp;" + " No";
            }
        }

        public void saveStatus()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_status_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SrNo", txt_SrNO.Text);

            cmd.Parameters.AddWithValue("@updateDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Status", ddl_Status.SelectedItem.Text);

            cmd.ExecuteNonQuery();
            con.Close();
        }

        //NO USE
        public void showData(string id)
        {
            try
            {
                con88.Open();
                SqlCommand cmd = new SqlCommand("select id,Sr_No, Brand,Model,brdsrno,pnlsrno,Line_Out_Date from Newreporttable where id='" + id.ToString() + "' ", con88);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                txt_SrNO.Text = dt.Rows[0]["Sr_No"].ToString();
                txtBrand.Text = dt.Rows[0]["Brand"].ToString();
                txtmodel.Text = dt.Rows[0]["Model"].ToString();
                txt_Board_Sr_No.Text = dt.Rows[0]["brdsrno"].ToString();
                txt_PanelSr_No.Text = dt.Rows[0]["pnlsrno"].ToString();
                //txt_Mf_Date.Text = dt.Rows[0]["Line_Out_Date"].ToString();
                txt_Mf_Date.Text = (Convert.ToDateTime(dt.Rows[0]["Line_Out_Date"]).ToString("yyyy-MM-dd"));
                //txtOC_No.Text = dt.Rows[0][""].ToString();
                if (DateTime.Now.AddYears(-1) < DateTime.Parse(dt.Rows[0]["Line_Out_Date"].ToString()))
                {
                    checkbox3.Checked = true;
                    check();

                }
                else
                {
                    checkbox3.Checked = false;
                    check();

                }

                ViewState["id"] = id.ToString();

                lblMsg.Text = "";
                lblMsg.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con88.Close();
            }

        }
        protected void grd_RMA_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "cmd_Select")
            {

                showData(e.CommandArgument.ToString());

            }
            else
            { }
        }
        public void bindGrid()
        {
            try
            {
                con88.Open();
                SqlCommand cmd = new SqlCommand("usp_Newreporttable", con88);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Sr_No", txt_SrNO.Text);
                cmd.Parameters.AddWithValue("@brdsrno", txt_SrNO.Text);
                cmd.Parameters.AddWithValue("@pnlsrno", txt_SrNO.Text);
                cmd.Parameters.AddWithValue("@Macid", txt_SrNO.Text);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                grd_RMA.DataSource = dt;
                grd_RMA.DataBind();

            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con88.Close();
            }

        }

        //click event of page indexing of grid view
        protected void grd_RMA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_RMA.PageIndex = e.NewPageIndex;
            this.bindGrid();

            lblMsg.Text = "";
            lblMsg.Visible = false;
        }

        public void sendEmail()
        {
            string getmail;
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT EmailId FROM tbl_User WHERE UserName='" + Session["UserName"].ToString() + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            getmail = dt.Rows[0]["EmailId"].ToString();
            con.Close();
            MailMessage message = new MailMessage();
            message.From = new MailAddress("mes@videotexindia.com");
            message.To.Add(getmail);
            message.Subject = "Subject of your email";
            message.Body = "Body of your email";

            // create a SmtpClient object

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.office365.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("mes@videotexindia.com", "BXzbs666");
            smtpClient.EnableSsl = true;

            // send the email
            try
            {
                smtpClient.Send(message);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }

        }
        public void chkStatus()
        {

            string strStatus;
            con.Open();

            // Create SQL command to retrieve data from database
            SqlCommand cmd = new SqlCommand("SELECT status FROM tbl_RMAReq WHERE SrNo='" + txt_SrNO.Text.ToString() + "'", con);
            //SqlCommand cmd = new SqlCommand(strcmd, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);



            // Check if any matching data was found in database
            if (dt.Rows.Count > 0)
            {
                strStatus = dt.Rows[0]["status"].ToString();
                if (strStatus != ddl_Status.SelectedItem.Text)
                {
                    sendEmail();
                }
                else
                { }
            }

            con.Close();



        }


        protected void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)

        {
            sendEmail();



            if ((ddl_Status.SelectedItem.Text == "Dispatch For VTX") || (ddl_Status.SelectedItem.Text == "Dispatch By VTX"))
            {
                txtdocketnum.Text = "";
                rfvField.Validate();
                Paneldocket.Visible = true;
            }
            else
            {
                Paneldocket.Visible = false;
                txtdocketnum.Text = "";
            }
        }
         //-------- status  of entry page----------
         public void  getstatus()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Sp_status_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddl_Status.DataSource = dt;
            

           ddl_Status.DataTextField = "status";
           ddl_Status.DataValueField = "id";
           ddl_Status.ClearSelection();
           ddl_Status.DataBind();
            con.Close();
        }

        public void getlogdata()
        {
            //DataTable ld = new DataTable();
            //SqlCommand command = new SqlCommand("SELECT * FROM tbl_tatus_log", con);
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //adapter.Fill(ld);
            //return ld;
        }




        protected void Grid12_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        protected void btnimage_Click(object sender, EventArgs e)
        {

            if (btnimage.Text == "Show Image")
            {
                pnlImage.Visible = true;
                //Grid12.Visible = true;
                btnimage.Text = "Hide Image";
                btneditrecord.Visible = false;
                btnstatus.Visible = false;

            }
            else
            {
                pnlImage.Visible = false;
                //Grid12.Visible = true;
                btnimage.Text = "Show Image";
                btneditrecord.Visible = true;
                btnstatus.Visible = true;
            }
            statusPanel.Visible = false;
            //statusPanel.Visible = true;

            //btn_save.Visible = false;
            //statusPanel.Visible = false;
            //pnlImage.Visible = true;
            //btnstatus.Visible = false;
            //btneditrecord.Visible = false;



        }

        protected void btnstatus_Click(object sender, EventArgs e)


        {
          
            showgrid();
            //con.Open();


            //// Create a SqlCommand object to retrieve data from the logdata table
            //SqlCommand cmd = new SqlCommand("SELECT Status,Remarks, CONVERT(varchar, Remark_date, 103) as Remark_date ,DocketNumber FROM tbl_status_log", con);

            //// Create a SqlDataAdapter object to fill a DataTable with the data
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            //con.Close();

            //// Bind the DataTable to the GridView control
            //GridView1.DataSource = dt;
            //GridView1.DataBind();




            //btnstatus.Visible = true;



            if (btnstatus.Text == "Show Status")
            {
                statusPanel.Visible = true;
                //Grid12.Visible = true;
                btnstatus.Text = "Hide Status";
                btneditrecord.Visible = false;
                btnimage.Visible = false;

            }
            else
            {
                btnstatus.Text = "Show Status";

                statusPanel.Visible = false;
                //Grid12.Visible = false;

                btneditrecord.Visible = true;
                btnimage.Visible = true;
            }
            pnlImage.Visible = false;
            //statusPanel.Visible = true;


        }

        //-------------------- show grid data ---------------
        public void showgrid()
        {
            string Rid = CheckRecord();


            con.Open();



            SqlCommand cmd = new SqlCommand("SELECT Status,Remarks,DocketNumber,Remark_Date FROM tbl_status_log where FileRefNo=@FileRefNo", con);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@FileRefNo", Rid.ToString());

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            con.Close();

            // Bind the DataTable to the GridView control
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btneditrecord_Click1(object sender, EventArgs e)
        {

            btnstatus.Visible = false;
            btnimage.Visible = false;
            btnimage.Visible = false;
            statusPanel.Visible = false;
            btneditrecord.Visible = false;
            pnlImage.Visible = false;
            btn_save.Visible = true;
            pnlBrand.Enabled = true;
            Panel1.Enabled = false;
            PanelWarranty.Enabled = true;



        }

        protected void btnstatusSave_Click(object sender, EventArgs e)
        {
            showgrid();

            logdata();
            sendEmail();



            btnstatus.Text = "Show Status";
            statusPanel.Visible = false;


            btneditrecord.Visible = true;
            btnimage.Visible = true;
        }
    }
}













