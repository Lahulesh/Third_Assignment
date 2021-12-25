using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Third
{
    public partial class vendor : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            VendorGrid();
        }
        void VendorGrid()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand sqlcom = new SqlCommand("select v.VendorId , v.VendorName,v.VendorEmail,v.VendorContact,c.CityName  from Vendor as v ,City as c where v.CityId=c.CityId", connect);
                SqlDataReader dr = sqlcom.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GridView1.DataSource = dr;
                    GridView1.DataBind();
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        connect.Open();
                        DropDownList DropDownList1 = (e.Row.FindControl("DropDownList1") as DropDownList);
                        SqlCommand cmd = new SqlCommand("select CityName from City", connect);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        sda.Fill(ds);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DropDownList1.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                        string selectedCity = DataBinder.Eval(e.Row.DataItem, "CityName").ToString();
                        DropDownList1.Items.FindByValue(selectedCity).Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            VendorGrid();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            VendorGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Response.Write("<script>alert('Update Button was not working... Thanks')</script>");
        }
    }
}