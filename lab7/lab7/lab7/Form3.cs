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
        SqlDataAdapter adapterclients, adaptercontracts, adaptertariffs;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            SqlConnection connectWarehousebd = new SqlConnection(connctSt);

            adapterclients = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adaptercontracts = new SqlDataAdapter("SELECT contracts.* FROM contracts", connectWarehousebd);
            adaptertariffs = new SqlDataAdapter("SELECT tariffs.* FROM tariffs", connectWarehousebd);

            adapterclients.Fill(ds.clients);
            adaptercontracts.Fill(ds.contracts);
            adaptertariffs.Fill(ds.tariffs);

            bindingSource1.DataSource = ds.tariffs;
            comboBox1.DataSource = bindingSource1;
            comboBox1.DisplayMember = "name_tariffs";
            comboBox1.ValueMember = "id_tariffs";
            ds.Relations.Add("rel", ds.Tables["clients"].Columns["id_client"], ds.Tables["contracts"].Columns["id_client"]);//связь таблиц
            bild = new SqlCommandBuilder(adaptercontracts);
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "rel";
            dataGridView1.DataSource = bindingSource2;
            bindingSource3.DataSource = bindingSource2;
            dataGridView2.DataSource = bindingSource3;
        }
    }
}
