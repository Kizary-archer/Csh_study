using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weblab5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        SqlConnection connectWarehousebd;
        SqlDataAdapter adapter;
        SqlCommandBuilder bild;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connctSt = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT list_of_products.* FROM [list_of_products]", connectWarehousebd);
            adapter.Fill(ds, "list_of_products");
            GridView2.DataSource = ds.Tables["list_of_products"];
            bild = new SqlCommandBuilder(adapter);
        }

        protected void SqlDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}