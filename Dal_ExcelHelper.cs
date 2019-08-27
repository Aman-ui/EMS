using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Entity;




    public class ExcelHelper
    {
        OleDbConnection objOleDbConnection = null;
        private string GetExcelConnection(string ExcelFilePath)
        {

            //For Excel 
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties='Excel 12.0;IMEX=1'";

            /*For CSV ***********************************
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties='text;HDR=Yes;FMT=CSVDelimited';";
            ***********************************************/
            //string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties='text;HDR=Yes;FMT=CSVDelimited';";
            
            return connStr;

        }
        public ExcelHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetExcelTableData(string ExcelFilePath, string TableName)
        {
            DataTable Dt = new DataTable();
            try
            {
                objOleDbConnection = new OleDbConnection();
                objOleDbConnection.ConnectionString = GetExcelConnection(ExcelFilePath);
                objOleDbConnection.Open();
                OleDbDataAdapter objDataAdapater = new OleDbDataAdapter("SELECT * FROM [" + TableName + "]", objOleDbConnection);
                OleDbCommandBuilder objCommandBuilder = new OleDbCommandBuilder(objDataAdapater);
                objDataAdapater.Fill(Dt);
                return Dt;
            }
            catch (Exception m)
            {
                return null;
            }
            finally
            {
                if (objOleDbConnection != null)
                {
                    objOleDbConnection.Close();
                    objOleDbConnection.Dispose();
                }
                if (Dt != null)
                {
                    Dt.Dispose();
                }
            }
        }
        public DataTable DeleteTempRecordsFromExcel(string ExcelFilePath, string TableName)
        {
            DataTable Dt = new DataTable();
            try
            {
                objOleDbConnection = new OleDbConnection();
                objOleDbConnection.ConnectionString = GetExcelConnection(ExcelFilePath);
                objOleDbConnection.Open();
                OleDbDataAdapter objDataAdapater = new OleDbDataAdapter("SELECT * FROM [" + TableName + "]", objOleDbConnection);
                OleDbCommandBuilder objCommandBuilder = new OleDbCommandBuilder(objDataAdapater);
                objDataAdapater.Fill(Dt);
                for (int i = 0; i < 3; i++)
                {
                    Dt.Rows[i].Delete();
                }
                Dt.TableName = "[" + TableName + "]";
                // Dt.Tables[0].TableName = "["+TableName+"]";
                Dt.AcceptChanges();

                //objDataAdapater.Update(Dt);


                for (int i = 0; i < Dt.Columns.Count; i++)
                {
                    Dt.Columns[i].ColumnName = Dt.Rows[0][i].ToString().Replace(" ", string.Empty).Trim();
                }
                Dt.Rows[0].Delete();
                Dt.AcceptChanges();

                return Dt;
            }
            catch (Exception m)
            {
                throw;
            }
            finally
            {
                if (objOleDbConnection != null)
                {
                    objOleDbConnection.Close();
                    objOleDbConnection.Dispose();
                }
                if (Dt != null)
                {
                    Dt.Dispose();
                }
            }
        }
        public void CleanUpTable(string TableName)
        {
            try
            {

                string SqlCon = "TMS";//ConfigurationManager.AppSettings["NPSConn"].ToString();
                SqlCon = ConfigurationManager.ConnectionStrings[SqlCon].ConnectionString;
                SqlConnection con = new SqlConnection(SqlCon);               
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = "truncate table " + TableName;
                con.Open();
                Cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception M)
            {
                throw;
            }
        }
        public string[] GetSheetName(string ExcelFilePath)
        {
            DataTable Dt = new DataTable();
            try
            {

                objOleDbConnection = new OleDbConnection();
                objOleDbConnection.ConnectionString = GetExcelConnection(ExcelFilePath);
                objOleDbConnection.Open();

                Dt = objOleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (Dt == null)
                {
                    return null;
                }
                string[] ExcelName = null;
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    ExcelName = new string[Dt.Rows.Count];
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        ExcelName[i] = Dt.Rows[i]["TABLE_NAME"].ToString();
                    }
                }
                return ExcelName;
            }
            catch (Exception m)
            {
                return null;
            }
            finally
            {
                if (objOleDbConnection != null)
                {
                    objOleDbConnection.Close();
                    objOleDbConnection.Dispose();
                }
                if (Dt != null)
                {
                    Dt.Dispose();
                }
            }
        }
        public DataTable GetCSVData(string strFileName)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(strFileName) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");
            conn.Open();
            string strQuery = "SELECT * FROM [" + System.IO.Path.GetFileName(strFileName) + "]";
            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);
            System.Data.DataSet ds = new System.Data.DataSet("CSV File");
            adapter.Fill(ds);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Columns.Count; i++)
                {
                    if (ds.Tables[0].Columns[i].DataType == typeof(DateTime))
                    {
                        for (int j = 0; j <= ds.Tables[0].Rows.Count; i++)
                        {
                            string shrtDate = ds.Tables[0].Rows[j][i].ToString();//.Substring(0, 7);
                            ds.Tables[0].Rows[j][i] = Cdate(shrtDate);
                        }
                    }
                }
                ds.Tables[0].AcceptChanges();
            }
            return ds.Tables[0];
        }
        private DateTime Cdate(string Date)
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-US", true);
            DateTime Dt = DateTime.ParseExact(Date, "dd/MM/yyyy", theCultureInfo);
               //DateTime.ParseExact(Date, "MM/dd/yy", null);
            return Dt;
        }
        public void ExportDataTable(DataTable dt, string FileName)
        {
            string attachment = "attachment; filename=" + FileName + ".xls";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            string sTab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                HttpContext.Current.Response.Write(sTab + dc.ColumnName);
                sTab = "\t";
            }
            HttpContext.Current.Response.Write("\n");

            int i;
            foreach (DataRow dr in dt.Rows)
            {
                sTab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write(sTab + dr[i].ToString());
                    sTab = "\t";
                }
                HttpContext.Current.Response.Write("\n");
            }
            HttpContext.Current.Response.End();
        }
        public int BulkInsertRecords(DataTable Dt, string TableName, bool ColumnMaping)
        {
            int Resu = -1;// TruancateTable(TableName);
            string SqlCon = "TMS";//ConfigurationManager.AppSettings["NPSConn"].ToString();
            
    
           SqlCon=ConfigurationManager.ConnectionStrings[SqlCon].ConnectionString;
           
            if (Resu == -1)
            {
                using (SqlConnection destinationConnection =
                               new SqlConnection(SqlCon))
                {
                    destinationConnection.Open();

                    using (SqlTransaction transaction =
                               destinationConnection.BeginTransaction())
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(
                                   destinationConnection, SqlBulkCopyOptions.KeepIdentity,
                                   transaction))
                        {
                            bulkCopy.BatchSize = Dt.Rows.Count;
                            bulkCopy.DestinationTableName = TableName;

                            if (ColumnMaping == true)
                            {
                                SqlBulkCopyColumnMapping TicketNumber = new SqlBulkCopyColumnMapping("TicketNumber", "TicketNumber");
                                bulkCopy.ColumnMappings.Add(TicketNumber);
                                SqlBulkCopyColumnMapping TicketDescription = new SqlBulkCopyColumnMapping("TicketDescription", "TicketDescription");
                                bulkCopy.ColumnMappings.Add(TicketDescription);
                                SqlBulkCopyColumnMapping TicketStatus = new SqlBulkCopyColumnMapping("TicketStatus", "TicketStatus");
                                bulkCopy.ColumnMappings.Add(TicketStatus);
                                SqlBulkCopyColumnMapping TicketCreatedon = new SqlBulkCopyColumnMapping("TicketCreatedon", "TicketCreatedon");
                                bulkCopy.ColumnMappings.Add(TicketCreatedon);
                                SqlBulkCopyColumnMapping TicketApplicationName = new SqlBulkCopyColumnMapping("TicketApplicationName", "TicketApplicationName");
                                bulkCopy.ColumnMappings.Add(TicketApplicationName);
                                SqlBulkCopyColumnMapping TicketType = new SqlBulkCopyColumnMapping("TicketType", "TicketType");
                                bulkCopy.ColumnMappings.Add(TicketType);
                            }

                            // Write from the source to the destination.
                            // This should fail with a duplicate key error.
                            try
                            {
                                bulkCopy.WriteToServer(Dt);
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                //Console.WriteLine(ex.Message);
                                transaction.Rollback();
                                //Console.ReadLine();
                                throw;

                            }
                            finally
                            {
                                destinationConnection.Close();
                                destinationConnection.Dispose();
                                Dt.Dispose();
                            }
                        }
                    }
                }
                return 1;


            }
            else
            {
                return 0;
            }
        }
        public DataTable GetTblBulkImportTable(string TableName)
        {
            try
            {
                IDataReader Dr = null;
                DataTable DT = new DataTable();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = 360000;
                Cmd.CommandText = "sp_dsp_npsbulkupload";
                con.Open();
                Dr = Cmd.ExecuteReader();
                DT.Load(Dr);
                con.Close();
                DT.TableName = "TblTmpBulkImportData";
                return DT;
            }
            catch (Exception M)
            {
                throw;
            }
        }
        
        public int ValidateData(string StoreProcedure)
        {
            int res = 0;
             string SqlCon = "TMS";
                SqlCon = ConfigurationManager.ConnectionStrings[SqlCon].ConnectionString;
            SqlConnection con = new SqlConnection(SqlCon);
            try
            {
               

               
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = 36000;
                Cmd.CommandText = StoreProcedure;
                con.Open();
                res = Cmd.ExecuteNonQuery();
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
            return res;
        }
        public DataTable GetTblCorporateBulkImportTable(string TableName)
        {
            try
            {
                IDataReader Dr = null;
                DataTable DT = new DataTable();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = 360000;
                Cmd.CommandText = "sp_dsp_npsCorporateBulkUpload";
                con.Open();
                Dr = Cmd.ExecuteReader();
                DT.Load(Dr);
                con.Close();
                DT.TableName = "TblTmpCorporateBulkImportData";
                return DT;
            }
            catch (Exception M)
            {
                throw;
            }
        }
        public DataTable GetValidatedTable(string TableName)
        {
            try
            {
                string SqlCon = "TMS";//ConfigurationManager.AppSettings["NPSConn"].ToString();
                SqlCon = ConfigurationManager.ConnectionStrings[SqlCon].ConnectionString;
              
                IDataReader Dr = null;
                DataTable DT = new DataTable();
                SqlConnection con = new SqlConnection(SqlCon);
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 360000;
                Cmd.CommandText = "Select * from " + TableName;
                con.Open();
                Dr = Cmd.ExecuteReader();
                DT.Load(Dr);
                con.Close();
                DT.TableName = "TblValidatedData";
                return DT;
            }
            catch (Exception M)
            {
                throw;
            }
        }
        public void ValidateCorporateData()
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString; ;
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = 36000;
                Cmd.CommandText = "nps_Corporate_Bulk_Validate";
                con.Open();
                Cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception M)
            {
                throw;
            }
        }
        public void Validate(string StoreProcedureName)
        {
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString; 
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandTimeout = 36000;
                Cmd.CommandText=StoreProcedureName;
                con.Open();
                Cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception M)
            {
                throw;
            }
 
        }
        public DataTable GetValidatedData(string TableName)
        {
            try
            {
                IDataReader Dr = null;
                DataTable DT = new DataTable();
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;
                SqlCommand Cmd = new SqlCommand();
                Cmd.Connection = con;
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandTimeout = 360000;
                Cmd.CommandText = "Select * from " + TableName;
                con.Open();
                Dr = Cmd.ExecuteReader();
                DT.Load(Dr);
                con.Close();
                DT.TableName = "TblValidatedData";
                return DT;
            }
            catch (Exception M)
            {
                throw;
            }
        }
         
    }

