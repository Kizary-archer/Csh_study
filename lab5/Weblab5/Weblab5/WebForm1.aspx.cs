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
            string connctSt = WebConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT * FROM [passport]", connectWarehousebd);
            adapter.Fill(ds, "passport");
            GridView2.DataSource = ds.Tables["passport"];
            bild = new SqlCommandBuilder(adapter);
        }

        protected void SqlDataSource_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}