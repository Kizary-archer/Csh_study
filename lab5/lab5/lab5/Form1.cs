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
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT passport.* FROM passport", connectWarehousebd);
            adapter.Fill(ds, "passport");
            dataGridView1.DataSource = ds.Tables["passport"];
            bild = new SqlCommandBuilder(adapter);
        }
        private void Form_Cloasing(object sender, EventArgs e)
        {
            adapter.Update(ds, "passport");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow dr = ds.Tables["passport"].NewRow();
            dr[0] = Convert.ToInt32(textBox1.Text);
            dr[1] = Convert.ToInt32(textBox1.Text);
            dr[2] = dateTimePicker1.Value;
            dr[3] = dateTimePicker2.Value;
            dr[4] = textBox2.Text;
            ds.Tables["passport"].Rows.Add(dr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ds.Tables["passport"].AcceptChanges();
            ds.Tables["passport"].Rows[Convert.ToInt32(textBox3.Text)].Delete();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ds.Tables["passport"].RejectChanges();
        }
    }
}
