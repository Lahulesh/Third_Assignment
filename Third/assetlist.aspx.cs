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
    public partial class assetlist : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["AssetRegister"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AssetListGrid();
            }
        }
        void AssetListGrid()
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            using (SqlConnection cons = new SqlConnection(con))
            {
                cons.Open();
                SqlCommand cmd = new SqlCommand("delete from Asset where AssetId='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", cons);
                cmd.ExecuteNonQuery();
                AssetListGrid();
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
                    cmd.CommandText = "select AssetID,AssetName,VendorName,Cost from Asset where AssetName like '%' + @AssetName + '%'";
                    cmd.Connection = connect;
                    cmd.Parameters.AddWithValue("@AssetName", assetsearch.Text.Trim());
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
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            DropDownListEditVendorAsset.Items.Clear(); 
            TextBoxEditAssetId.Text = GridView1.Rows[e.NewSelectedIndex].Cells[0].Text;
            TextBoxEditAssetName.Text = GridView1.Rows[e.NewSelectedIndex].Cells[1].Text;
            DropDownListEditVendorAsset.Items.Insert(0, new ListItem(GridView1.Rows[e.NewSelectedIndex].Cells[2].Text));
            EditDropDown();
            TextBoxEditCost.Text = GridView1.Rows[e.NewSelectedIndex].Cells[3].Text;
        }


        public void EditDropDown()
        {
            using (SqlConnection cons = new SqlConnection(con))
            {
                cons.Open();
                SqlCommand cmd = new SqlCommand("select VendorName from Vendor", cons);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DropDownListEditVendorAsset.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }
        protected void ButtonEditAsset_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cons = new SqlConnection(con))
                {
                    cons.Open();
                    SqlCommand cmd = new SqlCommand("update Asset set AssetName='" + TextBoxEditAssetName.Text + "',VendorName='" + DropDownListEditVendorAsset.Text + "', Cost='" + Convert.ToDecimal(TextBoxEditCost.Text) + "' where AssetId='" + Convert.ToInt32(TextBoxEditAssetId.Text) + "'", cons);
                    cmd.ExecuteNonQuery();
                }
                AssetListGrid();
                Clear();
            }
            catch(Exception)
            {
                Response.Write("<script>alert('Please Select the row Thanks')</script>");
            }
        }
        public void Clear()
        {
            TextBoxEditAssetId.Text = "";
            TextBoxEditAssetName.Text = "";
            DropDownListEditVendorAsset.Items.Clear();
            TextBoxEditCost.Text = "";
        }

        protected void ButtonCancelEdit_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("newasset.aspx");
        }
    }
}