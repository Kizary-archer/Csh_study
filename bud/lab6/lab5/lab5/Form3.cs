using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;
using System.Data;

namespace lab5
{
    public partial class Form3 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form3()
        {
            InitializeComponent();
        }
        private string outt = "";
        public string Selected
        {
            get { return this.outt; }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT product.* FROM product", connectWarehousebd);
            adapter.Fill(ds, "product");
            dataGridView1.DataSource = ds.Tables["product"];
            bild = new SqlCommandBuilder(adapter);
            this.outt = "";
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.outt = ds.Tables["product"].Rows[this.BindingContext[ds.Tables["product"]].Position]["products_name"].ToString();
        }
    }
}
