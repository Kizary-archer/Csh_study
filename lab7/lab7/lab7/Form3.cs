using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;

namespace lab7
{
    public partial class Form3 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapterclients, adaptercontracts, adapterpassport;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);

            adapterclients = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adaptercontracts = new SqlDataAdapter("SELECT contracts.* FROM contracts", connectWarehousebd);
            adapterpassport = new SqlDataAdapter("SELECT passort.* FROM passort", connectWarehousebd);

            adapterclients.Fill(ds, "clients");
            adaptercontracts.Fill(ds, "contracts");
            adapterpassport.Fill(ds, "passport");

            ds.Relations.Add("rel", ds.Tables["clients"].Columns["id_client"], ds.Tables["contracts"].Columns["id_contracts"]);//связь таблиц
            bild = new SqlCommandBuilder(adaptercontracts);
            bindingSource1.DataSource = ds.clients;
            dataGridView1.DataSource = bindingSource1;
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "rel";
            dataGridView2.DataSource = bindingSource2;
        }
    }
}
