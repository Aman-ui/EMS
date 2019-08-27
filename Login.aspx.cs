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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                TxtUserId.Focus();
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (validateuser(TxtUserId.Text, TxtPassword.Text))
                {
                    Response.Redirect("~/Calender.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), UniqueID, "Javascript:alert('Invalid User Id or Password !!!!')", true);
                }
            }
            catch (Exception M)
            {
            }
        }


        private bool validateuser(string userid, string password)
        {
            bool result = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["TMS"].ConnectionString;

            try
            {
                IDataReader Dr;
                DataTable DtUserDetails = new DataTable();
                DtUserDetails.TableName = "TblUserDetails";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from TblResource with(nolock) where RName=@RName and RPassword=@password and RStatus=1";
                cmd.Parameters.AddWithValue("@RName", userid);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                Dr = cmd.ExecuteReader();
                DtUserDetails.Load(Dr);
                con.Close();
                string DbUserId = Convert.ToString(DtUserDetails.Rows[0]["RName"]);
                string DbPassword=Convert.ToString(DtUserDetails.Rows[0]["RPassword"]);
                string ResourceId=Convert.ToString(DtUserDetails.Rows[0]["RID"]);
                string RRole = Convert.ToString(DtUserDetails.Rows[0]["RROLE"]);
                

                if (DbUserId.ToLower().Equals(userid.ToLower()) && DbPassword.Equals(password))
                {
                    Session["resourceid"] = ResourceId;
                    Session["Username"] = TxtUserId.Text;
                    Session["Role"] = RRole;
                    result= true;
                    
                }
                else
                {
                    result= false;
                }

            }
            catch (Exception M)
            {
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

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            TxtUserId.Text = "" ;
            TxtPassword.Text="";
        }
    }
}