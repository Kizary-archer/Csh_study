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


namespace lab9
{
    public partial class Form1 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapterclient,adapterpass;
        warehouseDataSet ds = new warehouseDataSet();
        SqlCommandBuilder bild;
        public Form1()
        {
            InitializeComponent();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            bindingSource1.Position -= 1;
            bindingSource2.Position = bindingSource1.Position;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource1.Position += 1;
            bindingSource2.Position = bindingSource1.Position;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapterclient = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adapterclient.Fill(ds, "clients");
            adapterpass = new SqlDataAdapter("SELECT passport.* FROM passport", connectWarehousebd);
            adapterpass.Fill(ds, "passport");
            bindingSource1.DataSource = ds.clients;
            bindingSource2.DataSource = ds.passport;
            textBox2.DataBindings.Add("text", bindingSource1, "name");
            textBox3.DataBindings.Add("text", bindingSource1, "surname");
            textBox4.DataBindings.Add("text", bindingSource1, "patronymic");
            textBox5.DataBindings.Add("text", bindingSource1, "phone");
            textBox6.DataBindings.Add("text", bindingSource1, "id_client");

            dateTimePicker1.DataBindings.Add("text", bindingSource2, "Date_of_birth");
            dateTimePicker2.DataBindings.Add("text", bindingSource2, "Date_issues");
            textBox1.DataBindings.Add("text", bindingSource2, "issued_by");

            comboBox1.DataSource = ds.passport;
            comboBox1.ValueMember = "id_passport";
            comboBox1.DisplayMember = "id_passport";
            comboBox1.DataBindings.Add("text", bindingSource2, "id_passport");

            //настройка адаптера на вставку
            adapterclient.InsertCommand = new SqlCommand("insert into clients values (@id_passport,@id_client,@name,@surname,@patronymic,@phone)", connectWarehousebd);
            adapterclient.InsertCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterclient.InsertCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterclient.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            adapterclient.InsertCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.InsertCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");
            adapterclient.InsertCommand.Parameters.Add("@phone", SqlDbType.Int, 4, "phone");

            //удаление строк
            adapterclient.DeleteCommand = new SqlCommand("delete from clients where id_client = @id_client", connectWarehousebd);
            adapterclient.DeleteCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            //обновление строк
            adapterclient.UpdateCommand = new SqlCommand("update clients set id_passport= @id_passport,id_client = @id_client,name = @name,surname = @surname,patronymic = @patronymic,phone = @phone where id_client = @id_client", connectWarehousebd);
            adapterclient.UpdateCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterclient.UpdateCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterclient.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            adapterclient.UpdateCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.UpdateCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");
            adapterclient.UpdateCommand.Parameters.Add("@phone", SqlDbType.Int, 4, "phone");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            label1.Visible = true;
            label2.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            textBox1.Visible = true;
            textBox6.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            comboBox1.Visible = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource1.RemoveCurrent();
            if (ds.clients.GetChanges(DataRowState.Deleted) != null) adapterclient.Update(ds.clients);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataRow row = ds.clients.NewRow();
            row[0] = Convert.ToInt32(comboBox1.SelectedValue);
            row[1] = Convert.ToInt32(ds.clients.Rows.Count + 1);
            row[2] = textBox2.Text;
            row[3] = textBox3.Text;
            row[4] = textBox4.Text;
            row[5] = Convert.ToInt32(textBox5.Text);
            ds.clients.Rows.Add(row);
            if (ds.clients.GetChanges(DataRowState.Added) != null)
            {
                adapterclient.Update(ds.clients);
                bindingSource1.MoveLast();
            }
            label1.Visible = false;
            label2.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            textBox1.Visible = false;
            textBox6.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Visible = false;
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            bindingSource1.Position -= 1;
            bindingSource1.EndEdit();
           // if (ds.clients.GetChanges(DataRowState.Added) == null)
              adapterclient.Update(ds.clients);
          
        }

    }
}
