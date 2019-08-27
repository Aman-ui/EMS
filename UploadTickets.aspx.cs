using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace Timesheetmanagement
{
    public partial class UploadTickets : System.Web.UI.Page
    {
        ExcelHelper objExcelHelper = new ExcelHelper();
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

            }
        }

        protected void BtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (FuexcelFile.HasFile)
            {

                string FilePath = Server.MapPath("~/App_Data/Tickets/" + DateTime.Now.ToString("ddMMyyyyhhssmm") + Path.GetExtension(FuexcelFile.PostedFile.FileName));

                FuexcelFile.SaveAs(FilePath);
                FuexcelFile.Dispose();
                string[] ExcelTables = objExcelHelper.GetSheetName(FilePath);
                 DataTable Dt = objExcelHelper.GetExcelTableData(FilePath, ExcelTables[0].ToString());

                //DataTable Dt = objExcelHelper.GetExcelTableData(FilePath, "Sheet1$");
                //DataTable Dt = objExcelHelper.GetExcelTableData(FilePath,"Payment");

                if (Dt != null && Dt.Rows.Count > 0)
                {
                    objExcelHelper.CleanUpTable("TmpTicketsUpload");
                    //  Validations(Dt);
                    if (Dt != null && Dt.Rows.Count > 0)
                    {

                        int res = objExcelHelper.BulkInsertRecords(Dt, "TmpTicketsUpload", true);

                        int result=0;
                        if (res > 0)
                        {
                            objExcelHelper.Validate("PRC_VALIDATE_TMP_TICKETS_DATA");
                            DataTable DtData = objExcelHelper.GetValidatedData("TMPTICKETSUPLOAD");
                            

                            if (DtData != null && DtData.Rows.Count > 0)
                            {
                            result  =  objExcelHelper.ValidateData("PRC_PROCESS_BUILKTICKETS_DATA");
                            }


                            if (result > 0)
                            {
                                GrdUploadedData.DataSource = DtData;
                                //GrdUploadedData.DataSource = DvSort;
                                GrdUploadedData.DataBind();





                               // Session["ValidatedData"] = DtData;
                               

                                ClientScript.RegisterStartupScript(this.GetType(), "SavedMessage", "<script type='text/javascript'>window.onload=function(){alert('" + "Sucessfully Uploaded Tickets!!!!" + "');};</script>", false);


                            }




                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "SavedMessage", "<script type='text/javascript'>window.onload=function(){alert('" + "ERROR OCCURED !!!!" + "');};</script>", false);
                        return;
                    }
                    // GrdData.DataSource = Dt;
                    //GrdData.DataBind();
                }

            }
        }

        protected void ImgDownloadExcel_Click(object sender, ImageClickEventArgs e)
        {
            string Fpath = Server.MapPath("~/DOWNLOAD_EXCEL/TicketTemplate.xlsx");
            FileInfo file = new FileInfo(Fpath);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=" + "TicketTemplate.xlsx");
                Response.AddHeader("Content-Type", "application/Excel");
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.End();

            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }


    }
}
    
