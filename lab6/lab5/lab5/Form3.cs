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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["id_client"].DisplayIndex)
            {
                Form2 f2 = new Form2();
                f2.ShowDialog();
                if (f2.DialogResult == DialogResult.OK)
                {
                    dataGridView1[dataGridView1.CurrentCellAddress.X, dataGridView1.CurrentCellAddress.Y].Value = f2.Selectedcon;
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT contracts.* FROM contracts", connectWarehousebd);
            adapter.Fill(ds, "contracts");
            dataGridView1.DataSource = ds.Tables["contracts"];
        }
    }
}
