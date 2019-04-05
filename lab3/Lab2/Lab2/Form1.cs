using System;
using System.Configuration;// Предоставляет доступ к файлам конфигурации для клиентских приложений
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;// Пространство имен  является поставщиком данных платформы .NET для SQL Server.




namespace Lab2
{
    public partial class Form1 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        private SqlConnection сonnectWarehousebd;
        public Form1()
        {
             InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            сonnectWarehousebd = new SqlConnection(@"Data Source=Max;Initial Catalog=warehouse;Integrated Security=True");
            сonnectWarehousebd.Open();
            richTextBox1.Text = String.Format("Версия сервера:{0} \n", сonnectWarehousebd.ServerVersion);
            richTextBox1.Text += String.Format("Состояние соединения1:{0} \n", сonnectWarehousebd.State.ToString());
            сonnectWarehousebd.Close();
            richTextBox1.Text += String.Format("Состояние соединения1:{0} \n", сonnectWarehousebd.State.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику Bus_Route
            сonnectWarehousebd = new SqlConnection(connctSt );//
            сonnectWarehousebd.Open();//метод открытия подключения
            richTextBox1.Text += String.Format("Версия сервера:{0} \n", сonnectWarehousebd.ServerVersion);
            richTextBox1.Text += String.Format("Состояние соединения2:{0} \n", сonnectWarehousebd.State.ToString());//описание строки подключения и ее вывод в бокс
            сonnectWarehousebd.Close();//метод закрытия подключения
            richTextBox1.Text += String.Format("Состояние соединения2:{0} \n ", сonnectWarehousebd.State.ToString());//описание строки подключения и ее вывод в бокс


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику Bus_Route
            сonnectWarehousebd = new SqlConnection(connctSt);//
            SqlCommand cmd = new SqlCommand("SELECT count(clients.name)FROM clients", сonnectWarehousebd);
            сonnectWarehousebd.Open();//метод открытия подключения
            richTextBox1.Text = String.Format("Название БД:{0} \n", сonnectWarehousebd.Database);
            richTextBox1.Text += String.Format("Получено записей:{0} \n", cmd.ExecuteScalar());
            сonnectWarehousebd.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику Bus_Route
            сonnectWarehousebd = new SqlConnection(connctSt);//
            SqlCommand cmd = new SqlCommand("INSERT INTO clients(id_passport, id_client, name, surname, patronymic,phone) VALUES (6, 6, 5,5,5,2342344)", сonnectWarehousebd);
            сonnectWarehousebd.Open();//метод открытия подключения

            richTextBox1.Text = String.Format("Записей добавлено:{0} \n", cmd.ExecuteNonQuery());
            сonnectWarehousebd.Close();

        }
    }
}
