﻿using System;
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
            this.Width = 600;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            clientsDB clients = new clientsDB("warehouseConnectionString");
            clients.Insert(new SecAccess(textBox1.Text,textBox2.Text,textBox3.Text, dateTimePicker1.Value,Convert.ToInt32(maskedTextBox1.Text)));
            dataGridView1.DataSource = clients.ds.standalone_clients;
            cancel();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            dataGridView1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            this.Width = 864;
        }
        private void cancel()
        {
            groupBox1.Visible = false;
            dataGridView1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
           // maskedTextBox1.Text = "2343453212";
            this.Width = 600;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cancel();
        }
    }
}
