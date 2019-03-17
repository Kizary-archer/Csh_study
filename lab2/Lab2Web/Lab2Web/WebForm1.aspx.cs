using System;
using System.Web.Configuration;
using System.Data.SqlClient;


namespace Lab2Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;
            SqlConnection сonnectWarehousebd = new SqlConnection(connectionString);

            сonnectWarehousebd.Open();
            TextBox1.Text = String.Format("Версия сервера:{0} \n", сonnectWarehousebd.ServerVersion);
            TextBox1.Text += String.Format("Состояние соединения:{0} \n", сonnectWarehousebd.State.ToString());
            сonnectWarehousebd.Close();
            TextBox1.Text += String.Format("Состояние соединения:{0} \n ", сonnectWarehousebd.State.ToString());

        }
    }
}