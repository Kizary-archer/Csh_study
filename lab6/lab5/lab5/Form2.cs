using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;
using System.Data;

namespace lab5
{
    public partial class Form2 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form2()
        {
            InitializeComponent();
        }
        private string sclients = "";
        public string Selectedcon
        {
            get { return this.sclients; }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adapter.Fill(ds, "clients");
            dataGridView1.DataSource = ds.Tables["clients"];
            this.sclients = "";
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.sclients = ds.Tables["clients"].Rows[this.BindingContext[ds.Tables["clients"]].Position]["id_client"].ToString();
        }
    }
}
