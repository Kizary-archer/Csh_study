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
        SqlDataAdapter adapterprod, adaptercontracts, adaptertariffs;
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

            adapterprod = new SqlDataAdapter("SELECT status_contracts.* FROM status_contracts", connectWarehousebd);
            adaptercontracts = new SqlDataAdapter("SELECT contracts.* FROM contracts", connectWarehousebd);
            adaptertariffs = new SqlDataAdapter("SELECT tariffs.* FROM tariffs", connectWarehousebd);

            adapterprod.Fill(ds.status_contracts);
            adaptercontracts.Fill(ds.contracts);
            adaptertariffs.Fill(ds.tariffs);

            bindingSource1.DataSource = ds.tariffs;
            comboBox1.DataSource = bindingSource1;
            comboBox1.DisplayMember = "name_tariffs";
            comboBox1.ValueMember = "id_tariffs";
            ds.Relations.Add("relContract", ds.Tables["tariffs"].Columns["id_tariffs"] , ds.Tables["contracts"].Columns["id_tariffs"]);//связь таблиц
            ds.Relations.Add("relprod", ds.Tables["contracts"].Columns["id_contracts"], ds.Tables["status_contracts"].Columns["id_contracts"]);//связь таблиц
           // bild = new SqlCommandBuilder(adapterclients);
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "relContract";
            dataGridView1.DataSource = bindingSource2;

            bindingSource3.DataSource = bindingSource2;
            bindingSource3.DataMember = "relprod";
            dataGridView2.DataSource = bindingSource3;
        }
    }
}
