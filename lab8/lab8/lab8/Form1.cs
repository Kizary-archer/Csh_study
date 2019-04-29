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
        DataView dv;
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

            dv = new DataView(ds.clients, "", "", DataViewRowState.CurrentRows);

            


            foreach(DataColumn col in ds.clients.Columns)
            {
                comboBox1.Items.Add(col.ColumnName);
                comboBox2.Items.Add(col.ColumnName);
            }

            comboBox1.SelectedItem = "id_client";
            comboBox2.SelectedItem = "id_client";
            ds.clients.DefaultView.Sort = "id_client";
            dv.Sort = "id_client";

            dataGridView1.DataSource = ds.clients.DefaultView;
            dataGridView2.DataSource = dv;
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
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                string filter = String.Format("{0}> = '{1}'", comboBox2.SelectedItem.ToString(), textBox2.Text);
                dv.RowFilter = filter;
            }

            string sort = comboBox1.SelectedItem.ToString();
            if (checkBox2.Checked) sort += " DESC";
            dv.Sort = sort;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dv.RowFilter = "";
            dv.Sort = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 f3 = new Form2();
            f3.Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
