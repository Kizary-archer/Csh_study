using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;

namespace lab7
{
    public partial class Form2 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapterclients, adaptercontracts;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form2()
        {
            InitializeComponent();
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapterclients = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adaptercontracts = new SqlDataAdapter("SELECT contracts.* FROM contracts", connectWarehousebd);
            adapterclients.Fill(ds, "clients");
            adaptercontracts.Fill(ds, "contracts");
            ds.Relations.Add("rel", ds.Tables["clients"].Columns["id_client"], ds.Tables["contracts"].Columns["id_client"]);//связь таблиц
            bild = new SqlCommandBuilder(adaptercontracts);
            bindingSource1.DataSource = ds.clients;
            dataGridView1.DataSource = bindingSource1;
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "rel";
            dataGridView2.DataSource = bindingSource2;
        }
    }
}
