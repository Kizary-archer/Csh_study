using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;
using System.Data;

namespace lab5
{
    public partial class Form1 : Form
    {

        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT list_of_products.* FROM list_of_products", connectWarehousebd);
            adapter.Fill(ds, "list_of_products");
            dataGridView1.DataSource = ds.Tables["list_of_products"];
            bild = new SqlCommandBuilder(adapter);
        }
        private void Form_Cloasing(object sender, EventArgs e)
        {
            adapter.Update(ds, "list_of_products");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables["list_of_products"].NewRow();
            dr[0] = textBox1.Text;
            dr[1] = dateTimePicker1.Value;
            dr[2] = Convert.ToInt32(textBox4.Text);
            dr[3] = Convert.ToInt32(textBox2.Text);
            ds.Tables["list_of_products"].Rows.Add(dr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.Tables["list_of_products"].AcceptChanges();
            ds.Tables["list_of_products"].Rows[Convert.ToInt32(textBox3.Text)].Delete();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ds.Tables["list_of_products"].RejectChanges();
        }
    }
}
