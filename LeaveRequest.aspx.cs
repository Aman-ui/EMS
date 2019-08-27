using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Timesheetmanagement.DL;
namespace Timesheetmanagement
{
    public partial class LeaveRequest : System.Web.UI.Page
    {
        DBprovider DB =new DBprovider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string role = Convert.ToString(Session["Role"]);

                if ((Session["resourceid"] == null || Session["Username"] == null || Session["Role"] == null || !role.ToLower().Equals("admin")))
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    //string role = Convert.ToString(Session["Role"]);
                    lblUserName.Text = Convert.ToString(Session["Username"]);
                    BindLeaveType();
                    //if (!role.ToLower().Equals("admin"))
                    //{
                    //    Response.Redirect("~/Calender.aspx");
                    //}
                }
                lblRequeststatus.Text = "";
            }
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            DateTime dtfromdate = Convert.ToDateTime(txtFromDate.Text).Date;// Cdate((txtFromDate.Text)//  ("dd-MM-YYYY"));
            DateTime dttodate = Convert.ToDateTime(txtToDate.Text).Date;// Cdate(txtToDate.Text);



            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMS"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "sp_LeveInsert";
            // cmd.CommandTimeout = 240;
            // cmd.CommandText = "EMS.[MAIA_DWH_PRESTAGING].[dbo].[sp_fr_pool_details_new]";
            cmd.Parameters.AddWithValue("@ResourceName", ddlResourceName.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@fromdate", dtfromdate);
            cmd.Parameters.AddWithValue("@todate", dttodate);
            cmd.Parameters.AddWithValue("@LeaveType", "leave");
            cmd.Parameters.AddWithValue("@Comment", txtComment.Text);
            cmd.Parameters.AddWithValue("@User", Convert.ToString(Session["Username"]));
            cmd.Connection = con;

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {
                lblRequeststatus.Text = "Succesfully Applied for Leave...";
                clearecontrol();
            }
        }

        private DateTime Cdate(string Date)
        {

            DateTime Dt = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
            //DateTime.ParseExact(Date, "MM/dd/yy", null);
            return Dt;
        }
        private void BindLeaveType()
        {
            //var resuorce = new List<string> { "Select", "RajneeshY", "PrashantA", "NarayanC", "RaviBh", "ManojP", "RajeshPA", "ShivaliS", "AsishkN", "Jamal", "SamadhanS", "Shwetag", "SandeepB", "HirenS", "DevendraS", "ViddheshS", "MohitA1", "AjayC", "pshivas", "PrasannaGK", "SaurabhBo", "Dhanajij", "Chandan", "ajayk", "RajendraC", "MahendraS" };
            //resuorce.Sort();
            DataTable dt = DB.GetResourceList();

            ddlResourceName.DataSource = dt;
            ddlResourceName.DataTextField = "RFullName";
            ddlResourceName.DataValueField = "RNAME";
            ddlResourceName.DataBind();
            ddlResourceName.Items.Add("Select");
            ddlResourceName.SelectedValue = "Select";
        }
        private void clearecontrol()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtComment.Text = "";
            //ddlLeaveType.SelectedIndex = 2;

        }



        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Calender.aspx");
        }

    }
}