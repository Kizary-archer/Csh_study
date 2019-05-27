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
        DataGridViewButtonColumn deleteButton;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            dataGridView1.Enabled = true;
            clientsDB clients = new clientsDB("warehouseConnectionString");//подключаемся к бд и заполняем датасет
  
            bindingSource1.DataSource = clients.GetAllClient();
            dataGridView1.DataSource = bindingSource1;
            deleteButton = new DataGridViewButtonColumn();
            deleteButton.HeaderText = "Х";
            deleteButton.Text = "Х";
            deleteButton.UseColumnTextForButtonValue = true;
            deleteButton.Width = 30;
            dataGridView1.Columns.Add(deleteButton);
            this.Width = 630;
            dataGridView1.Columns[5].Visible = false;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            clientsDB clients = new clientsDB("warehouseConnectionString");
            clients.Insert(new SecAccess(textBox1.Text,textBox2.Text,textBox3.Text, dateTimePicker1.Value,Convert.ToInt32(maskedTextBox1.Text)));
            bindingSource1.DataSource = clients.GetAllClient();
            dataGridView1.DataSource = bindingSource1;
            cancel();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            dataGridView1.Enabled = false;
            button2.Enabled = false;
            button3.Visible = false;
            button1.Visible = true;
            this.Width = 888;
        }
        private void cancel()
        {
            groupBox1.Visible = false;
            dataGridView1.Enabled = true;
            button2.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            this.Width = 630;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cancel();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            clientsDB clients = new clientsDB("warehouseConnectionString");
            
            if (dataGridView1.Columns[e.ColumnIndex] == deleteButton)
            {
                clients.Delete(new SecAccess(dataGridView1.CurrentRow.Index));
                bindingSource1.DataSource = clients.GetAllClient();
                dataGridView1.DataSource = bindingSource1;

            }
            else
            {
                int i = (int)dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value;
                //MessageBox.Show(i.ToString());
                SecAccess access = clients.GetClient(i);
                textBox1.Text = access.CName;
                textBox2.Text = access.Surname;
                textBox3.Text = access.Patronymic;
                dateTimePicker1.Value = access.Date_of_Birth;
                maskedTextBox1.Text = access.Phone.ToString();
                button1.Visible = false;
                button3.Visible = true;
                groupBox1.Visible = true;
                this.Width = 888;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clientsDB clients = new clientsDB("warehouseConnectionString");
            int i = (int)dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value;
            clients.Update(new SecAccess(i,textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, Convert.ToInt32(maskedTextBox1.Text)));
            bindingSource1.DataSource = clients.GetAllClient();
            dataGridView1.DataSource = bindingSource1;
            cancel();
        }
    }
}
