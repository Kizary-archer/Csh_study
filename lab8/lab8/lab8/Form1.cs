using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;
using System.Data;

namespace lab8
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
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adapter.Fill(ds, "clients");
            //dataGridView1.DataSource = ds.clients;
            


            foreach(DataColumn col in ds.clients.Columns)
            {
                comboBox1.Items.Add(col.ColumnName);
            }

            comboBox1.SelectedItem = "id_client";
            ds.clients.DefaultView.Sort = "id_client";
            //dv.Sort = "id_client";

            dataGridView1.DataSource = ds.clients.DefaultView;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text !="")
            {
                string filter = String.Format("{0}> = '{1}'", comboBox1.SelectedItem.ToString(), textBox1.Text);
                ds.clients.DefaultView.RowFilter = filter;
            }

            string sort = comboBox1.SelectedItem.ToString();
            if (checkBox1.Checked) sort += " DESC";
            ds.clients.DefaultView.Sort = sort;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.clients.DefaultView.RowFilter = "";
            ds.clients.DefaultView.Sort = "";
            // dataGridView1.DataSource = ds.clients.DefaultView;
            //dataGridView1.Update();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
