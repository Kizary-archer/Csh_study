using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Windows.Forms;


namespace lab8
{
    public partial class Form2 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        DataView dv;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adapter.Fill(ds, "clients");
            dv = new DataView(ds.clients);
            dataGridView1.DataSource = dv;
            foreach (DataColumn col in ds.clients.Columns)
            {
                comboBox1.Items.Add(col.ColumnName);
            }
            comboBox1.SelectedItem = "id_client";
            comboBox2.DataSource = ds.clients;
            comboBox2.DisplayMember = "id_client";
            comboBox2.ValueMember = "id_client";
            comboBox2.SelectedIndex = 0;
           // dv.Sort = "id_client";
           // bild = new SqlCommandBuilder(adapter);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.DisplayMember = comboBox1.SelectedItem.ToString();
            comboBox2.ValueMember = comboBox1.SelectedItem.ToString();
        }


        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dv.RowFilter = String.Format("{0}='{1}'", comboBox1.SelectedItem,comboBox2.SelectedValue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dv.RowFilter = String.Format("{0}='{1}'", comboBox1.SelectedItem.ToString(), comboBox2.SelectedValue);
        }
       

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dv.Sort = comboBox1.SelectedItem.ToString() + " ASC";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dv.Sort = comboBox1.SelectedItem.ToString() + " DESC";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dv.Sort = "";
            dv.RowFilter = "";
            comboBox1.SelectedItem = "id_client";
            comboBox2.DataSource = ds.clients;
            comboBox2.DisplayMember = "id_client";
            comboBox2.ValueMember = "id_client";
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataColumn FIO = new DataColumn("FIO",
            typeof(string), "name + ' ' + surname + ' ' + patronymic");
            ds.clients.Columns.Add(FIO);

        }
    }

}
