using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Runtime.Serialization;
using System.Configuration;
using System.Net;
using System.Data.SqlClient;
using System.Data;


namespace Timesheetmanagement
{

    public partial class ActivityReport1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if ((Session["resourceid"] == null || Session["Username"] == null || Session["Role"] == null))
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    string role = Convert.ToString(Session["Role"]);
                    if (!role.ToLower().Equals("admin"))
                    {
                        Response.Redirect("~/Calender.aspx");
                    }
                }

                //try
                //{
                //    RVEffort.ProcessingMode = ProcessingMode.Remote;
                //    IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["ReportServerUserId"], ConfigurationManager.AppSettings["ReportServerPassword"], ConfigurationManager.AppSettings["RelianceCapital"]);
                //    ServerReport serverReport = RVEffort.ServerReport;
                //    Microsoft.Reporting.WebForms.ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                //    Param[0] = new Microsoft.Reporting.WebForms.ReportParameter("fromdate", DateTime.Now.AddMonths(-4).ToShortDateString());
                //    Param[1] = new Microsoft.Reporting.WebForms.ReportParameter("todate", DateTime.Now.ToShortDateString());
                //    RVEffort.ServerReport.ReportServerCredentials = irsc;
                //    // Set the report server URL and report path
                //    serverReport.ReportServerUrl =
                //       new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
                //     serverReport.ReportPath = @"/Efforts_Management/"+DrpReports.SelectedItem.Value;
                //    // RVEffort.ServerReport.SetParameters(Param);
                //    RVEffort.ServerReport.Refresh();

                //}
                //catch (Exception M)
                //{

                //}

            }
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;

            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user,
             out string password, out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }
        }

        protected void DrpReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try {



            //    RVEffort.ProcessingMode = ProcessingMode.Remote;
            //    IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["ReportServerUserId"], ConfigurationManager.AppSettings["ReportServerPassword"], ConfigurationManager.AppSettings["RelianceCapital"]);
            //    ServerReport serverReport = RVEffort.ServerReport;
            //    //Microsoft.Reporting.WebForms.ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
            //    //Param[0] = new Microsoft.Reporting.WebForms.ReportParameter("fromdate", DateTime.Now.AddMonths(-4).ToShortDateString());
            //    //Param[1] = new Microsoft.Reporting.WebForms.ReportParameter("todate", DateTime.Now.ToShortDateString());
            //    RVEffort.ServerReport.ReportServerCredentials = irsc;
            //    // Set the report server URL and report path
            //    serverReport.ReportServerUrl =
            //       new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
            //    serverReport.ReportPath = @"/Efforts_Management/" + DrpReports.SelectedItem.Value;
            //    // RVEffort.ServerReport.SetParameters(Param);
            //    RVEffort.ServerReport.Refresh();


            //}
            //catch { 

            //}
        }

        protected void BtnViewReport_Click(object sender, EventArgs e)
        {
            try
            {

                Bindgrid();


                //RVEffort.ProcessingMode = ProcessingMode.Remote;
                //IReportServerCredentials irsc = new CustomReportCredentials(ConfigurationManager.AppSettings["ReportServerUserId"], ConfigurationManager.AppSettings["ReportServerPassword"], ConfigurationManager.AppSettings["ReportServerDomain"]);
                //ServerReport serverReport = RVEffort.ServerReport;
                //Microsoft.Reporting.WebForms.ReportParameter[] Param = new Microsoft.Reporting.WebForms.ReportParameter[2];
                //Param[0] = new Microsoft.Reporting.WebForms.ReportParameter("fromdate", dtfromdate.ToShortDateString());
                //Param[1] = new Microsoft.Reporting.WebForms.ReportParameter("todate", dttodate.ToShortDateString());
                //RVEffort.ServerReport.ReportServerCredentials = irsc;
                //RVEffort.ShowParameterPrompts = false;
                //// Set the report server URL and report path
                //serverReport.ReportServerUrl =
                //   new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
                //serverReport.ReportPath = @"/Efforts_Management/" + DrpReports.SelectedItem.Value;
                //RVEffort.ServerReport.SetParameters(Param);
                //RVEffort.ServerReport.Refresh();


            }
            catch (Exception M)
            {

            }
        }

        private DateTime Cdate(string Date)
        {

            DateTime Dt = DateTime.ParseExact(Date, "dd/MM/yyyy", null);
            //DateTime.ParseExact(Date, "MM/dd/yy", null);
            return Dt;
        }
        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        private void Bindgrid()
        {
            try
            {
                DateTime dtfromdate = Cdate(TxtFromDate.Text);
                DateTime dttodate = Cdate(TxtToDate.Text);


                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TMS"].ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "[dbo].[prc_effortsreport] ";
                // cmd.CommandTimeout = 240;
                // cmd.CommandText = "EMS.[MAIA_DWH_PRESTAGING].[dbo].[sp_fr_pool_details_new]";
                cmd.Parameters.AddWithValue("@fromdate", dtfromdate);
                cmd.Parameters.AddWithValue("@todate", dttodate);
                cmd.Connection = con;

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                gvReport.DataSource = ds.Tables[0];

                gvReport.DataBind();
            }
            catch (Exception ex)
            {

            }


        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Effort" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvReport.GridLines = GridLines.Both;
            gvReport.HeaderStyle.Font.Bold = true;
            gvReport.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
    }
}