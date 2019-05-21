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

namespace lab11
{
    public partial class Form1 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapterclient, adapterpass,adapterphone;
        warehouseDataSet1 ds = new warehouseDataSet1();
        DataView dvphone = new DataView();

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindingSource1.Position = comboBox1.SelectedIndex;
            bindingSource2.Position = comboBox1.SelectedIndex;
            dvphone.RowStateFilter = DataViewRowState.CurrentRows;
            dvphone.RowFilter = String.Format("id_client={0}", comboBox1.SelectedValue);
            dataGridView1.DataSource = dvphone;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox1.Enabled = false;
            groupBox3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            groupBox1.Enabled = true;
            groupBox3.Visible = false;
            ds.RejectChanges();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bindingSource1.Position -= 1;
            bindingSource1.EndEdit();
            bindingSource2.Position -= 1;
            bindingSource2.EndEdit();
            adapterpass.Update(ds.passport);
            adapterclient.Update(ds.clients);
            adapterphone.Update(ds.phone);
            bindingSource1.MoveLast();
            bindingSource2.MoveLast();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bindingSource1.RemoveCurrent();
            bindingSource2.RemoveCurrent();
            if (ds.clients.GetChanges(DataRowState.Deleted) != null)
            {
                adapterpass.Update(ds.passport);
                adapterclient.Update(ds.clients);
                adapterphone.Update(ds.phone);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox1.Enabled = false;
            groupBox3.Visible = true;
            DataRow row = ds.clients.NewRow();
            DataRow rowpass = ds.passport.NewRow();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            row[0] = Convert.ToInt32(ds.clients.Rows.Count + 1);
            row[1] = textBox2.Text;
            row[2] = textBox3.Text;
            row[3] = textBox4.Text;
            ds.clients.Rows.Add(row);
            rowpass[0] = Convert.ToInt32(ds.passport.Rows.Count + 1);
            rowpass[1] = dateTimePicker1.Value;
            rowpass[2] = dateTimePicker2.Value;
            rowpass[3] = textBox6.Text;
            ds.passport.Rows.Add(rowpass);
            if (ds.clients.GetChanges(DataRowState.Added) != null)
            {
                adapterclient.Update(ds.clients);
                adapterpass.Update(ds.passport);
                bindingSource1.MoveLast();
                bindingSource2.MoveLast();
            }
            dvphone.RowStateFilter = DataViewRowState.CurrentRows;
            dvphone.RowFilter = String.Format("id_client={0}", textBox1.Text);
            dataGridView1.DataSource = dvphone;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            groupBox1.Enabled = true;
            groupBox3.Visible = false;
            bindingSource1.Position -= 1;
            bindingSource1.EndEdit();
            bindingSource2.Position -= 1;
            bindingSource2.EndEdit();
            bindingSource1.Position += 1;
            bindingSource1.Position += 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataRow row = ds.phone.NewRow();
            row[0] = Convert.ToInt32(ds.phone.Rows.Count + 1);
            row[1] = Convert.ToInt32(textBox1.Text);
            row[2] = textBox7.Text;
            row[3] = dateTimePicker3.Value;
            ds.phone.Rows.Add(row);

            adapterphone.Update(ds.phone);

            textBox7.Text = "";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //connect
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString1"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapterclient = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adapterclient.Fill(ds, "clients");
            adapterpass = new SqlDataAdapter("SELECT passport.* FROM passport", connectWarehousebd);
            adapterpass.Fill(ds, "passport");
            adapterphone = new SqlDataAdapter("SELECT phone.* FROM phone", connectWarehousebd);
            adapterphone.Fill(ds, "phone");
            //вычисляемый столбец
            DataColumn FIO = new DataColumn("FIO",typeof(string), "name + ' ' + surname + ' ' + patronymic");
            ds.clients.Columns.Add(FIO);
            //привязка элементов
            bindingSource1.DataSource = ds.clients;
            bindingSource2.DataSource = ds.passport;
            bindingSource3.DataSource = ds.phone;
            comboBox1.DataSource = ds.clients;
            comboBox1.DisplayMember = "FIO";
            comboBox1.ValueMember = "id_client";
            comboBox1.SelectedItem = "id_client";
            textBox1.DataBindings.Add("text", bindingSource1, "id_client");
            textBox2.DataBindings.Add("text", bindingSource1, "name");
            textBox3.DataBindings.Add("text", bindingSource1, "surname");
            textBox4.DataBindings.Add("text", bindingSource1, "patronymic");
            dateTimePicker1.DataBindings.Add("text", bindingSource2, "Date_of_birth");
            dateTimePicker2.DataBindings.Add("text", bindingSource2, "Date_issues");
            textBox6.DataBindings.Add("text", bindingSource2, "issued_by");
            //datagrid
            dvphone.Table = ds.phone;
            dvphone.RowStateFilter = DataViewRowState.CurrentRows;
            dvphone.RowFilter = String.Format("id_client={0}", comboBox1.SelectedValue);
            dataGridView1.DataSource = dvphone;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            //настройка адаптера на вставку
            adapterclient.InsertCommand = new SqlCommand("insert into clients values (@id_client,@name,@surname,@patronymic)", connectWarehousebd);
            adapterclient.InsertCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterclient.InsertCommand.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            adapterclient.InsertCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.InsertCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");

            adapterpass.InsertCommand = new SqlCommand("insert into passport values (@id_passport,@Date_issues,@Date_of_birth,@issued_by)", connectWarehousebd);
            adapterpass.InsertCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterpass.InsertCommand.Parameters.Add("@Date_issues", SqlDbType.Date, 30, "Date_issues");
            adapterpass.InsertCommand.Parameters.Add("@Date_of_birth", SqlDbType.Date, 30, "Date_of_birth");
            adapterpass.InsertCommand.Parameters.Add("@issued_by", SqlDbType.VarChar, 30, "issued_by");

            adapterphone.InsertCommand = new SqlCommand("insert into phone values (@id_phone,@id_client,@phone,@Date_)", connectWarehousebd);
            adapterphone.InsertCommand.Parameters.Add("@id_phone", SqlDbType.Int, 4, "id_phone");
            adapterphone.InsertCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterphone.InsertCommand.Parameters.Add("@phone", SqlDbType.VarChar, 20, "phone");
            adapterphone.InsertCommand.Parameters.Add("@Date_", SqlDbType.Date, 30, "Date_");

            //удаление строк
            adapterclient.DeleteCommand = new SqlCommand("delete from clients where id_client = @id_client", connectWarehousebd);
            adapterclient.DeleteCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");

            adapterpass.DeleteCommand = new SqlCommand("delete from passport where id_passport = @id_passport", connectWarehousebd);
            adapterpass.DeleteCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");

            adapterphone.DeleteCommand = new SqlCommand("delete from phone where id_phone = @id_phone", connectWarehousebd);
            adapterphone.DeleteCommand.Parameters.Add("@id_phone", SqlDbType.Int, 4, "id_phone");

            //обновление строк
            adapterclient.UpdateCommand = new SqlCommand("update clients set id_client = @id_client,name = @name,surname = @surname,patronymic = @patronymic where id_client = @id_client", connectWarehousebd);
            adapterclient.UpdateCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterclient.UpdateCommand.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            adapterclient.UpdateCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.UpdateCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");

            adapterpass.UpdateCommand = new SqlCommand("update passport set  id_passport = @id_passport,Date_issues =@Date_issues,Date_of_birth = @Date_of_birth,issued_by = @issued_by where id_passport = @id_passport", connectWarehousebd);
            adapterpass.UpdateCommand.Parameters.Add("@id_passport", SqlDbType.Int, 4, "id_passport");
            adapterpass.UpdateCommand.Parameters.Add("@Date_issues", SqlDbType.Date, 30, "Date_issues");
            adapterpass.UpdateCommand.Parameters.Add("@Date_of_birth", SqlDbType.Date, 30, "Date_of_birth");
            adapterpass.UpdateCommand.Parameters.Add("@issued_by", SqlDbType.VarChar, 30, "issued_by");

            adapterphone.UpdateCommand = new SqlCommand("update phone set  id_phone = @id_phone,id_client =@id_client,phone = @phone,Date_ = @Date_ where id_phone = @id_phone", connectWarehousebd);
            adapterphone.UpdateCommand.Parameters.Add("@id_phone", SqlDbType.Int, 4, "id_phone");
            adapterphone.UpdateCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
            adapterphone.UpdateCommand.Parameters.Add("@phone", SqlDbType.VarChar, 20, "phone");
            adapterphone.UpdateCommand.Parameters.Add("@Date_", SqlDbType.Date, 30, "Date_");

            groupBox2.Enabled = false;
            groupBox1.Enabled = true;
            textBox1.Enabled = false;
            groupBox3.Visible = false;
        }
    }
}
