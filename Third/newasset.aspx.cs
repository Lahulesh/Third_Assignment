using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Third
{
    public partial class newasset : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            newassetpurchase_CalendarExtender.EndDate = DateTime.Now;
            VendorName();
        }
        void reset()
        {
            newassetid.Text = "";
            newassetname.Text = "";
            newassetpurchase.Text = "";
            newassetcost.Text = "";
        }
        void VendorName()
        {
            DropDownList1.Items.Clear();
            using (SqlConnection Connect = new SqlConnection(con))
            {
                Connect.Open();
                SqlCommand command2 = new SqlCommand("select VendorName from Vendor", Connect);
                SqlDataReader reader1 = command2.ExecuteReader();
                while (reader1.Read())
                {
                    this.DropDownList1.Items.Add(reader1["VendorName"].ToString());
                }
                /*reader1.Close();*/
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection Connect = new SqlConnection(con))
                {
                    Connect.Open();
                    SqlCommand cmd = new SqlCommand("insert into Asset values('"+newassetname.Text+ "','"+DropDownList1.Text+"','"+Convert.ToDateTime(newassetpurchase.Text)+"','"+Convert.ToInt32(newassetcost.Text)+"')",Connect);
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Data Inserted')</script>");
                    reset();
                }
            }
            catch(Exception)
            {
                Response.Write("<script>alert('Please Fill All The Details... Thanks')</script>");
            }
        }
    }
}