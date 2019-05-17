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

            //настройка адаптера на вставку
            adapterclient.InsertCommand = new SqlCommand("insert into clients values (@id_passport,@id_client,@name,@surname,@patronymic,@phone)", connectWarehousebd);
            adapterclient.InsertCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterclient.InsertCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterclient.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            adapterclient.InsertCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.InsertCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");
            adapterclient.InsertCommand.Parameters.Add("@phone", SqlDbType.Int, 4, "phone");

            adapterpass.InsertCommand = new SqlCommand("insert into passport values (@id_passport,@id_client,@Date_issues,@Date_of_birth,@issued_by)", connectWarehousebd);
            adapterpass.InsertCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterpass.InsertCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterpass.InsertCommand.Parameters.Add("@Date_issues", SqlDbType.Date, 30, "Date_issues");
            adapterpass.InsertCommand.Parameters.Add("@Date_of_birth", SqlDbType.Date, 30, "Date_of_birth");
            adapterpass.InsertCommand.Parameters.Add("@issued_by", SqlDbType.VarChar, 30, "issued_by");

            //удаление строк
            adapterclient.DeleteCommand = new SqlCommand("delete from clients where id_client = @id_client", connectWarehousebd);
            adapterclient.DeleteCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");

            adapterpass.DeleteCommand = new SqlCommand("delete from passport where id_client = @id_client", connectWarehousebd);
            adapterpass.DeleteCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            //обновление строк
            adapterclient.UpdateCommand = new SqlCommand("update clients set id_passport= @id_passport,id_client = @id_client,name = @name,surname = @surname,patronymic = @patronymic,phone = @phone where id_client = @id_client", connectWarehousebd);
            adapterclient.UpdateCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterclient.UpdateCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterclient.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            adapterclient.UpdateCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.UpdateCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");
            adapterclient.UpdateCommand.Parameters.Add("@phone", SqlDbType.Int, 4, "phone");

            adapterpass.UpdateCommand = new SqlCommand("update passport set  id_passport = @id_passport,id_client=@id_client,Date_issues =@Date_issues,Date_of_birth = @Date_of_birth,issued_by = @issued_by where id_passport = @id_passport", connectWarehousebd);
            adapterpass.UpdateCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterpass.UpdateCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterpass.UpdateCommand.Parameters.Add("@Date_issues", SqlDbType.Date, 30, "Date_issues");
            adapterpass.UpdateCommand.Parameters.Add("@Date_of_birth", SqlDbType.Date, 30, "Date_of_birth");
            adapterpass.UpdateCommand.Parameters.Add("@issued_by", SqlDbType.VarChar, 30, "issued_by");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            label1.Visible = true;
            textBox6.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bindingSource1.RemoveCurrent();
            bindingSource2.RemoveCurrent();
            if (ds.clients.GetChanges(DataRowState.Deleted) != null)
            {
                adapterpass.Update(ds.passport);
                adapterclient.Update(ds.clients);
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataRow row = ds.clients.NewRow();
            DataRow rowpass = ds.passport.NewRow();
            row[0] = Convert.ToInt32(ds.clients.Rows.Count + 1);
            row[1] = Convert.ToInt32(ds.clients.Rows.Count + 1);
            row[2] = textBox2.Text;
            row[3] = textBox3.Text;
            row[4] = textBox4.Text;
            row[5] = Convert.ToInt32(textBox5.Text);

            rowpass[0] = Convert.ToInt32(ds.passport.Rows.Count + 1);
            rowpass[1] = Convert.ToInt32(ds.passport.Rows.Count + 1);
            rowpass[2] = dateTimePicker1.Value;
            rowpass[3] = dateTimePicker2.Value;
            rowpass[4] = textBox1.Text;
            ds.clients.Rows.Add(row);
            ds.passport.Rows.Add(rowpass);
            if (ds.clients.GetChanges(DataRowState.Added) != null)
            {
                adapterclient.Update(ds.clients);
                adapterpass.Update(ds.passport);
                bindingSource1.MoveLast();
                bindingSource2.MoveLast();
            }
            label1.Visible = false;
            textBox6.Visible = false;
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            bindingSource1.Position -= 1;
            bindingSource1.EndEdit();
            bindingSource2.Position -= 1;
            bindingSource2.EndEdit();
                adapterpass.Update(ds.passport);
                adapterclient.Update(ds.clients);
                bindingSource1.MoveLast();
                bindingSource2.MoveLast();
           

        }

    }
}
