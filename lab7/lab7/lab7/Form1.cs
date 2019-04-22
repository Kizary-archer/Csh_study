using System;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.
using System.Configuration;



namespace lab7
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
            bindingSource1.DataSource = ds.Tables[0];
            textBox1.DataBindings.Add("Text", bindingSource1, "name");
            textBox2.DataBindings.Add("Text", bindingSource1, "surname");
            textBox3.DataBindings.Add("Text", bindingSource1, "patronymic");
            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "phone";
            listBox1.DataBindings.Add("Text", bindingSource1, "phone");
            bindingNavigator1.BindingSource = bindingSource1;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingSource1.Position = listBox1.SelectedIndex;
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource1.Position = bindingSource1.Find("phone", textBox4.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }
    }
}
