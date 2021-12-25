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
    public partial class report : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportGrid();
            cost();
        }
        void ReportGrid()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand sqlcom = new SqlCommand("select * from Asset", connect);
                SqlDataReader dr = sqlcom.ExecuteReader();
                if (dr.HasRows == true)
                {
                    GridView1.DataSource = dr;
                    GridView1.DataBind();
                }
            }
        }
        void cost()
        {
            using (SqlConnection connect = new SqlConnection(con))
            {
                connect.Open();
                SqlCommand sqlcom = new SqlCommand("SELECT SUM(Cost) as total FROM Asset", connect);
                SqlDataReader dr = sqlcom.ExecuteReader();
                while (dr.Read())
                {
                    Label2.Text = dr["total"].ToString();
                }
            }
        }
        void SearchAsset()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(con))
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "select AssetID,AssetName,VendorName,PurchaseDate,Cost from Asset where AssetName like '%' + @AssetName + '%'";
                    cmd.Connection = connect;
                    cmd.Parameters.AddWithValue("@AssetName", reportsearch.Text.Trim());
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet);

                    GridView1.DataSource = dataSet;

                    GridView1.DataBind();
                }
            }
            catch (Exception e)
            {
                Response.Write(e);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SearchAsset();
            Label1.Visible = false;
            Label2.Visible = false;
        }
    }
}