using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace lab12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            dataGridView1.Enabled = true;
            clientsDB clients = new clientsDB("warehouseConnectionString");//подключаемся к бд и заполняем датасет
            dataGridView1.DataSource = clients.ds.standalone_clients;
            dataGridView1.Columns[0].Visible = false;

            SecAccess access = new SecAccess("sad","asd","sad",dateTimePicker1.Value,24);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            dataGridView1.Enabled = false;
        }

    }
}
