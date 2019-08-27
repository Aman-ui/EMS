using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Timesheetmanagement
{
    public partial class Addefforts : System.Web.UI.Page
    {
        int resourceid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["resourceid"] == null || Session["Username"] == null))
            {
                Response.Redirect("~/Login.aspx");
            }



            resourceid = Convert.ToInt32(Session["resourceid"]);
            if (!IsPostBack)
            {
                BindCurrentdayData();
            }
        }
        protected void AddRow(object sender, EventArgs e)
        {
            UpdatepagedData();
            DataTable DtPagedData = ((DataTable)ViewState["PagedData"]);

            DataRow Dr = DtPagedData.NewRow();
            Dr["TId"] = 0;
            Dr["TEDATE"] = DateTime.Now.Date;
            Dr["TResourceId"] = resourceid;
            Dr["TECreatedon"] = DateTime.Now.Date;
            Dr["TEfforts"] = 0;
            Dr["TicketNumber"] = " ";
            Dr["TikcetCreatedOn"] = DateTime.Now.Date;
            Dr["TicketDescription"] = " ";

            Dr["TicketTypeId"] = 0;
            Dr["ApplicationId"] = 0;
            Dr["ApplicationTypeId"] = 0;
            Dr["VendorId"] = 0;
            Dr["ClassificationId"] = 0;
            Dr["TicketStatusId"] = 0;


            Dr["Mode"] = "I";
            //Dr["Mode"] = "U";
            
            DtPagedData.Rows.Add(Dr);
            RptAddefforts.DataSource = DtPagedData;
            RptAddefforts.DataBind();
            
            
        }

        private void UpdatepagedData()
        {
            DataTable DtPagedData = ((DataTable)ViewState["PagedData"]);
            foreach(RepeaterItem RptItems in RptAddefforts.Items)
            {
                Label LblTEID = ((Label)RptItems.FindControl("LblTEID"));
                TextBox TxtTikcetNo = ((TextBox)RptItems.FindControl("TxtTicketNumber"));
                TextBox TxtRemarks = ((TextBox)RptItems.FindControl("TxtRemarks"));
                TextBox TxtEfforts = ((TextBox)RptItems.FindControl("TxtEfforts"));
                Label LblDescription = ((Label)RptItems.FindControl("LblDescription"));
                Label LblTID = ((Label)RptItems.FindControl("LblTID"));

                DropDownList drptickettype = (DropDownList)RptItems.FindControl("DrpTicketType");
                DropDownList DrpApplications = (DropDownList)RptItems.FindControl("DrpApplications");
                DropDownList DrpApplicationType = (DropDownList)RptItems.FindControl("DrpApplicationType");
               // DropDownList DrpVendor = (DropDownList)RptItems.FindControl("DrpVendor");
                DropDownList DrpClassification = (DropDownList)RptItems.FindControl("DrpClassification");
                //DropDownList DrpTicketStatus = (DropDownList)RptItems.FindControl("DrpTicketStatus");

                DataRow[] DR = DtPagedData.Select("TEID ='"+ LblTEID.Text+"'");
                if (DR != null && DR.Count() > 0)
                {
                    DR[0]["TicketNumber"] = TxtTikcetNo.Text;
                    DR[0]["Remarks"] = TxtRemarks.Text;
                    DR[0]["TEfforts"] = TxtEfforts.Text;
                    DR[0]["TicketDescription"] = LblDescription.Text;
                    DR[0]["TID"] = LblTID.Text;


                    DR[0]["TicketTypeId"] = drptickettype.SelectedItem.Value;
                    DR[0]["ApplicationId"] = DrpApplications.SelectedItem.Value;
                    DR[0]["ApplicationTypeId"] = DrpApplicationType.SelectedItem.Value;
                  //  DR[0]["VendorId"] = DrpVendor.SelectedItem.Value;
                    DR[0]["ClassificationId"] = DrpClassification.SelectedItem.Value;
                   // DR[0]["TicketStatusId"] = DrpTicketStatus.SelectedItem.Value;

                }
                Button btnDeleteCurrent = (Button)RptItems.FindControl("Btndelete");
                btnDeleteCurrent.Visible = true;
                DtPagedData.AcceptChanges();
               

                
            }
        }


        private void BindCurrentdayData()
        {
            if (Request.QueryString["date"] != null)
            {
                DateTime DtDate = DateTime.ParseExact(Request.QueryString["date"], "dd/MM/yyyy", null);

                DataTable DtSelectmonthsefforts = new DataTable();


                DtSelectmonthsefforts.TableName = "TblEfforts";
                IDataReader Dr;
                SqlConnection con = new SqlConnection();
                try
                {

                    con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "prc_getcurrentdayefforts";
                    cmd.Parameters.AddWithValue("@currentdate", DtDate);
                    cmd.Parameters.AddWithValue("@resourceid", resourceid);

                    Dr = cmd.ExecuteReader();
                    DtSelectmonthsefforts.Load(Dr);
                    con.Close();

                   
                        ViewState["PagedData"] = DtSelectmonthsefforts;


                        if (DtSelectmonthsefforts != null && DtSelectmonthsefforts.Rows.Count > 0)
                        {
                            LblPagemode.Text = "U";
                        }
                        else
                        {
                            LblPagemode.Text = "I";
                        }


                    RptAddefforts.DataSource = DtSelectmonthsefforts;
                    RptAddefforts.DataBind();

                  


                }



                catch (Exception M)
                {
                    throw;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }


            }
        }

        protected void UpdateData(object sender, EventArgs e)
        {
            try
            {
                UpdatepagedData();
                DataTable DtPagedData = ((DataTable)ViewState["PagedData"]);
                if (DtPagedData != null && DtPagedData.Rows.Count > 0)
                {
                    foreach (DataRow Dr in DtPagedData.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(Dr["TicketDescription"]).Trim()))
                        {       
                            SqlConnection con = new SqlConnection();
                            try
                            {
                                DateTime DtEfforts = DateTime.ParseExact(Request.QueryString["date"], "dd/MM/yyyy", null);
                                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                                con.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.CommandText = "prc_updateefforts";
                                cmd.Parameters.AddWithValue("@TID", Convert.ToInt32(Dr["TID"]));
                                cmd.Parameters.AddWithValue("@TEDate", DtEfforts);
                                cmd.Parameters.AddWithValue("@TResourceId", resourceid);
                                cmd.Parameters.AddWithValue("@TECreatedon", Dr["TECreatedOn"]);
                                cmd.Parameters.AddWithValue("@TEUpdation", Dr["TEUpdatedOn"]);
                                cmd.Parameters.AddWithValue("@TEfforts", Convert.ToDecimal(Dr["TEfforts"]));
                                cmd.Parameters.AddWithValue("@Remarks", Convert.ToString(Dr["Remarks"]));
                                cmd.Parameters.AddWithValue("@Mode", Convert.ToString(Dr["Mode"]));
                                cmd.Parameters.AddWithValue("@TEID", Convert.ToInt32(Dr["TEID"]));

                                cmd.Parameters.AddWithValue("@TicketTypeId", Convert.ToInt32(Dr["TicketTypeId"]));
                                cmd.Parameters.AddWithValue("@ApplicationId", Convert.ToInt32(Dr["ApplicationId"]));
                                cmd.Parameters.AddWithValue("@ApplicationTypeId", Convert.ToInt32(Dr["ApplicationTypeId"]));
                                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Dr["VendorId"]));
                                cmd.Parameters.AddWithValue("@ClassificationId", Convert.ToInt32(Dr["ClassificationId"]));
                                cmd.Parameters.AddWithValue("@TicketStatusId", Convert.ToInt32(Dr["TicketStatusId"]));




                                 int res=  cmd.ExecuteNonQuery();

                                con.Close();


                                if (res > 0)
                                {

                                }
                                
                            }
                            catch (Exception M)
                            {
                                throw;
                            }
                            finally
                            {
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }
                            }
                        }
                    }
                }

                ClientScript.RegisterStartupScript(this.GetType(),UniqueID, "Javascript:alert('Sucessfully updated !!!!')",true);
                Response.Redirect("~/Calender.aspx");
            }
            catch (Exception U)
            {
                throw;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> searchTickets(string prefixText, int count)
        {
            try
            {

                prefixText = prefixText.Trim();

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select TicketNumber from TblTicketDetails with(Nolock) where TicketNumber like '"+prefixText+"%'";
                cmd.Connection = con;
                con.Open();
                List<string> Tikcets = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Tikcets.Add(sdr["TicketNumber"].ToString());
                    }
                }
                con.Close();
                return Tikcets;
            }
            catch (Exception M)
            {
                throw;
            }
        }


        protected void TxtTicketNumber_Changed(object sender, EventArgs e)
        {
            try
            {
                TextBox tbticketnumber = ((TextBox)(sender));


                DataTable DtTicketInfo = SearchTicketbyTicketNumber(tbticketnumber.Text);

                if (DtTicketInfo != null && DtTicketInfo.Rows.Count > 0)
                {
                    RepeaterItem Rptdata = ((RepeaterItem)(tbticketnumber.NamingContainer));
                    Label LblTicketcreatedon = (Label)Rptdata.FindControl("LblTicketcreatedon");
                    Label LblDescription = (Label)Rptdata.FindControl("LblDescription");
                    Label LblTID = (Label)Rptdata.FindControl("LblTID");
                    DropDownList DrpTicketType = (DropDownList)Rptdata.FindControl("DrpTicketType");

                    DropDownList DrpApplication = (DropDownList)Rptdata.FindControl("DrpApplications");

                    DropDownList DrpClassification = (DropDownList)Rptdata.FindControl("DrpClassification");

                 


                   // DropDownList DrpAppVendor = (DropDownList)Rptdata.FindControl("");
                    LblTicketcreatedon.Text = Convert.ToString( DtTicketInfo.Rows[0]["TikcetCreatedOn"]);
                    LblDescription.Text = Convert.ToString(DtTicketInfo.Rows[0]["TicketDescription"]);
                    LblTID.Text = Convert.ToString(DtTicketInfo.Rows[0]["TID"]);
                    DrpTicketType.SelectedIndex = DrpTicketType.Items.IndexOf(DrpTicketType.Items.FindByValue(Convert.ToString(DtTicketInfo.Rows[0]["TicketType"])));
                    DrpApplication.SelectedIndex = DrpApplication.Items.IndexOf(DrpApplication.Items.FindByValue(Convert.ToString(DtTicketInfo.Rows[0]["TicketApplication"])));

                    bindpastdropdowndata(DrpClassification, Convert.ToInt32(DrpTicketType.SelectedItem.Value));
                    DropDownList Drpapptype = (DropDownList)Rptdata.FindControl("DrpApplicationType");
                    //DropDownList Drpvendor = (DropDownList)Rptdata.FindControl("DrpVendor");
                    if (Drpapptype != null)
                    {
                        DataTable dtapplications = BindCascadingDropList("applications", Convert.ToInt32(DrpApplication.SelectedItem.Value));


                        if (dtapplications != null && dtapplications.Rows.Count > 0)
                        {
                            Drpapptype.SelectedIndex = Drpapptype.Items.IndexOf(Drpapptype.Items.FindByValue(Convert.ToString(dtapplications.Rows[0]["AppCat_id"])));
                           // Drpvendor.SelectedIndex = Drpvendor.Items.IndexOf(Drpvendor.Items.FindByValue(Convert.ToString(dtapplications.Rows[0]["AppVendor_Id"])));


                            Drpapptype.Enabled = false;
                           // Drpvendor.Enabled = false;

                            dtapplications.Dispose();
                        }

                        
                    }


                    DtTicketInfo.Dispose();

                }

                

                
            }
            catch(Exception M)
            {
                throw;
            }

        }

        private DataTable SearchTicketbyTicketNumber(string TicketNumber)
        {
            DataTable DtTicketsInfo = new DataTable();


            DtTicketsInfo.TableName = "TblTicketInfo";
            IDataReader Dr;
            SqlConnection con = new SqlConnection();
            try
            {

                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "Select * from  TblTicketDetails with(Nolock) where TicketNumber=@TicketNumber";
                cmd.Parameters.AddWithValue("@TicketNumber", TicketNumber);
               

                Dr = cmd.ExecuteReader();
                DtTicketsInfo.Load(Dr);
                con.Close();
                return DtTicketsInfo;
            }
            catch (Exception M)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        public void BindDropList(string DListType, DropDownList drp)
        {
            DataTable Dtsource = new DataTable();


            Dtsource.TableName = "Tbldropdowndata";
            IDataReader Dr;
            SqlConnection con = new SqlConnection();
            string DataText = string.Empty;
            string Datavalue = string.Empty;
            try
            {

                string Query = string.Empty;
                if (DListType.ToLower().Equals("tickettype"))
                {
                    Query = "Select * from MST_TicketType with(nolock) where isdelete<>0";
                    DataText = "TType_Name";
                    Datavalue = "TType_ID";
                }
                else if (DListType.ToLower().Equals("applications"))
                {
                    Query = "Select * from MST_Applications with(nolock) where application_status= 1";
                    DataText = "Application_Name";
                    Datavalue = "Application_Id";
                }
                else if (DListType.ToLower().Equals("applicationtype"))
                {
                    Query = "Select * from Mst_ApplicationCategory with(nolock)";
                    DataText = "Appcat_Name";
                    Datavalue = "Appcat_Id";
                }
                //else if (DListType.ToLower().Equals("vendor"))
                //{
                //    Query = "Select * from Mst_ApplicationVendor with(nolock)";
                //    DataText = "AppVendor_Name";
                //    Datavalue = "AppVendor_Id";
                //}
                else if (DListType.ToLower().Equals("classification"))
                {
                    Query = "Select * from Mst_Tikcetclassification with(nolock) where TClassification_status = 1";
                    DataText = "TClassification_Name";
                    Datavalue = "TClassification_Id";
                }
                //else if (DListType.ToLower().Equals("ticketstatus"))
                //{
                //    Query = "Select * from Mst_TicketStatus with(nolock) ";
                //    DataText = "TStatus";
                //    Datavalue = "TStatus_Id";
                //}

                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = Query;
                Dr = cmd.ExecuteReader();
                Dtsource.Load(Dr);
                con.Close();

                if (Dtsource != null && Dtsource.Rows.Count > 0)
                {
                    drp.DataSource = Dtsource;
                    drp.DataTextField = DataText;
                    drp.DataValueField = Datavalue;
                    drp.DataBind();

                    ListItem Lstselect = new ListItem("---- Select ----", "0");
                    drp.Items.Insert(0,Lstselect);
                }

            }
            catch (Exception M)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
           

        }

        protected void RptAddefforts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList drptickettype = (DropDownList)e.Item.FindControl("DrpTicketType");
                DropDownList DrpApplications = (DropDownList)e.Item.FindControl("DrpApplications");
                DropDownList DrpApplicationType = (DropDownList)e.Item.FindControl("DrpApplicationType");
               // DropDownList DrpVendor = (DropDownList)e.Item.FindControl("DrpVendor");
                DropDownList DrpClassification = (DropDownList)e.Item.FindControl("DrpClassification");
               // DropDownList DrpTicketStatus = (DropDownList)e.Item.FindControl("DrpTicketStatus");
                Button BtnDelete = (Button)e.Item.FindControl("Btndelete");

                BindDropList("tickettype",drptickettype);
                BindDropList("applications", DrpApplications);

                BindDropList("applicationtype", DrpApplicationType);
                //BindDropList("vendor", DrpVendor);
                BindDropList("classification", DrpClassification);
               // BindDropList("ticketstatus", DrpTicketStatus);


                Label LblTEID = ((Label)e.Item.FindControl("LblTEID"));


                 DataTable DtPagedData = ((DataTable)ViewState["PagedData"]);
                 if (DtPagedData != null && DtPagedData.Rows.Count > 0)
                 {
                     DataRow[] DR = DtPagedData.Select("TEID ='" + LblTEID.Text + "'");
                     if (DR != null && DR.Count() > 0)
                     {
                         drptickettype.SelectedIndex = drptickettype.Items.IndexOf(drptickettype.Items.FindByValue(Convert.ToString(DR[0]["TicketTypeId"])));
                         DrpApplications.SelectedIndex = DrpApplications.Items.IndexOf(DrpApplications.Items.FindByValue(Convert.ToString(DR[0]["ApplicationId"])));
                         DrpClassification.SelectedIndex = DrpClassification.Items.IndexOf(DrpClassification.Items.FindByValue(Convert.ToString(DR[0]["ClassificationId"])));
                         DrpApplicationType.SelectedIndex = DrpApplicationType.Items.IndexOf(DrpApplicationType.Items.FindByValue(Convert.ToString(DR[0]["ApplicationTypeId"])));
                         //DrpVendor.SelectedIndex = DrpVendor.Items.IndexOf(DrpVendor.Items.FindByValue(Convert.ToString(DR[0]["VendorId"])));
                         //DrpTicketStatus.SelectedIndex = DrpTicketStatus.Items.IndexOf(DrpTicketStatus.Items.FindByValue(Convert.ToString(DR[0]["TicketStatusId"])));

                         bindpastdropdowndata(DrpClassification, Convert.ToInt32(drptickettype.SelectedItem.Value));
                         DrpClassification.SelectedIndex = DrpClassification.Items.IndexOf(DrpClassification.Items.FindByValue(Convert.ToString(DR[0]["ClassificationId"])));


                         if (LblPagemode.Text == "U")
                         {
                             BtnDelete.Visible = true;
                         }
                         else
                         {
                             BtnDelete.Visible = true;
                             //BtnDelete.Visible =false;
                         }
                     }
                 }
            }
        }

      



        public DataTable BindCascadingDropList(string DListType,int id)
        {
            DataTable Dtsource = new DataTable();


            Dtsource.TableName = "Tbldropdowndata";
            IDataReader Dr;
            SqlConnection con = new SqlConnection();
            try
            {

                string Query = string.Empty;
               
                 if (DListType.ToLower().Equals("applications"))
                {
                    Query = "Select * from MST_Applications with(nolock) where application_status= 1 and Application_Id="+id;
                }
                else if (DListType.ToLower().Equals("classification"))
                {
                    Query = "Select * from Mst_Tikcetclassification C with(nolock) inner join Tbltickettypeclassificationmapping M on C.TClassification_Id=M.TClassification_ID  where TClassification_status = 1 and TType_ID="+id;
                }
            

                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = Query;
                Dr = cmd.ExecuteReader();
                Dtsource.Load(Dr);
                con.Close();

            }
            catch (Exception M)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return Dtsource;

        }

        private void bindpastdropdowndata(DropDownList drplist, int id)
        {
            if (drplist != null)
            {
                DataTable DtClassfication = BindCascadingDropList("classification", id);


                if (DtClassfication != null && DtClassfication.Rows.Count > 0)
                {
                    drplist.Items.Clear();
                    drplist.DataSource = DtClassfication;
                    drplist.DataTextField = "TClassification_Name";
                    drplist.DataValueField = "TClassification_Id";
                    drplist.DataBind();
                }
            }
        }

        protected void DrpTicketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp = sender as DropDownList;
            if (drp != null)
            {
                int id = Convert.ToInt32(drp.SelectedItem.Value);
                int RepeaterItemIndex = ((RepeaterItem)drp.NamingContainer).ItemIndex;

              
                    DropDownList Drpclass = (DropDownList)RptAddefforts.Items[RepeaterItemIndex].FindControl("DrpClassification");

                    if (Drpclass != null)
                    {
                        DataTable DtClassfication = BindCascadingDropList("classification", id);


                        if (DtClassfication != null && DtClassfication.Rows.Count > 0)
                        {
                            Drpclass.Items.Clear();
                            Drpclass.DataSource = DtClassfication;
                            Drpclass.DataTextField = "TClassification_Name";
                            Drpclass.DataValueField = "TClassification_Id";
                            Drpclass.DataBind();
                        }
                    }
                
            }
        }

        protected void DrpApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp = sender as DropDownList;
            if (drp != null)
            {
                int id = Convert.ToInt32(drp.SelectedItem.Value);
                int RepeaterItemIndex = ((RepeaterItem)drp.NamingContainer).ItemIndex;


                DropDownList Drpapptype = (DropDownList)RptAddefforts.Items[RepeaterItemIndex].FindControl("DrpApplicationType");
                //DropDownList Drpvendor = (DropDownList)RptAddefforts.Items[RepeaterItemIndex].FindControl("DrpVendor");
                if (Drpapptype != null)
                {
                    DataTable dtapplications = BindCascadingDropList("applications", id);


                    if (dtapplications != null && dtapplications.Rows.Count > 0)
                    {
                        Drpapptype.SelectedIndex = Drpapptype.Items.IndexOf(Drpapptype.Items.FindByValue(Convert.ToString(dtapplications.Rows[0]["AppCat_id"])));
                       // Drpvendor.SelectedIndex = Drpvendor.Items.IndexOf(Drpvendor.Items.FindByValue(Convert.ToString(dtapplications.Rows[0]["AppVendor_Id"])));


                        Drpapptype.Enabled = false;
                       // Drpvendor.Enabled = false;
                    }
                }

            }
        }

        protected void RptAddefforts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int LblTEID = Convert.ToInt32(e.CommandArgument.ToString());

                if (LblTEID > 0)
                {
                    Deleteselectedefforts(LblTEID);
                    BindCurrentdayData();
                }

            }
        }


        public int Deleteselectedefforts(int TEID)
        {
            SqlConnection con = new SqlConnection();
            int result=0;
            try
            {

                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "Delete from TblTicketEfforts where TEID=@TEID";
                cmd.Parameters.AddWithValue("@TEID", TEID);


                result = cmd.ExecuteNonQuery();
              
                con.Close();

               
            }
            catch (Exception M)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return result;
        }

    }







}