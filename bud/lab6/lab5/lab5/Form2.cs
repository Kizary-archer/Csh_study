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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["products_name"].DisplayIndex)
            {
                Form3 f3 = new Form3();
                f3.ShowDialog();
                if (f3.DialogResult == DialogResult.OK)
                {
                    dataGridView1[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y].Value = f3.Selected;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT list_of_products.* FROM list_of_products", connectWarehousebd);
            adapter.Fill(ds, "list_of_products");
            dataGridView1.DataSource = ds.Tables["list_of_products"];
            bild = new SqlCommandBuilder(adapter);
        }
    }
}
