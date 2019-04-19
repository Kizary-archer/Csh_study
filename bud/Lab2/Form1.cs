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
        SqlConnection connectbd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        private SqlConnection сonnectbd;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            сonnectbd = new SqlConnection(@"Data Source=Max;Initial Catalog=bug_bd;Integrated Security=True");
            сonnectbd.Open();
            richTextBox1.Text = String.Format("Версия сервера:{0} \n", сonnectbd.ServerVersion);
            richTextBox1.Text += String.Format("Состояние соединения1:{0} \n", сonnectbd.State.ToString());
            сonnectbd.Close();
            richTextBox1.Text += String.Format("Состояние соединения1:{0} \n", сonnectbd.State.ToString());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            сonnectbd = new SqlConnection(connctSt);//
            сonnectbd.Open();//метод открытия подключения
            richTextBox1.Text += String.Format("Версия сервера:{0} \n", сonnectbd.ServerVersion);
            richTextBox1.Text += String.Format("Состояние соединения2:{0} \n", сonnectbd.State.ToString());//описание строки подключения и ее вывод в бокс
            сonnectbd.Close();//метод закрытия подключения
            richTextBox1.Text += String.Format("Состояние соединения2:{0} \n ", сonnectbd.State.ToString());//описание строки подключения и ее вывод в бокс


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            сonnectbd = new SqlConnection(connctSt);
            сonnectbd.Open();//метод открытия подключения
            SqlCommand cmd = new SqlCommand("SELECT count(product.products_name)FROM product WHERE product.products_name = @NAME", сonnectbd);
            cmd.Parameters.AddWithValue("@NAME", textBox1.Text);
            richTextBox1.Text = String.Format("Название БД:{0} \n", сonnectbd.Database);
            richTextBox1.Text += String.Format("Получено записей:{0} \n", cmd.ExecuteScalar());
            сonnectbd.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику
            сonnectbd = new SqlConnection(connctSt);//
            SqlCommand cmd = new SqlCommand("INSERT INTO write_off(products_name, date_off_end, total_cost_off, units_count) VALUES (чай, 12.12.2017, 13,8)", сonnectbd);
            сonnectbd.Open();//метод открытия подключения

            richTextBox1.Text = String.Format("Записей добавлено:{0} \n", cmd.ExecuteNonQuery());
            сonnectbd.Close();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику
            сonnectbd = new SqlConnection(connctSt);
            сonnectbd.Open();
            cmd.Connection = сonnectbd;
            cmd.CommandText = "SELECT * FROM write_off ";
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                i++;
                richTextBox1.Text += String.Format("Данные о товаре\n№{0}:\nИмя: {1} \nДата: {2}\nцена: {3} \n", i, reader[0], reader[1], reader[2]);

            }
            reader.Close();
            сonnectbd.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику
            сonnectbd = new SqlConnection(connctSt);
            сonnectbd.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM write_off; SELECT* FROM product", сonnectbd);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            richTextBox1.Text = "Данные о товаре\n";
            while (reader.Read())
            {
                i++;
                richTextBox1.Text += String.Format("№{0}:\nИмя: {1} \nДата: {2}\nцена: {3} \n", i, reader[0], reader[1], reader[2]);
            }
            reader.NextResult();
            i = 0;
            richTextBox1.Text += "Данные о товаре\n";
            while (reader.Read())
            {
                i++;
                richTextBox1.Text += String.Format("№{0}:\nИмя: {1} \narticle: {2}\n", i, reader[0], reader[2]);
            }
            reader.Close();
            сonnectbd.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            сonnectbd = new SqlConnection(connctSt);
            cmd = new SqlCommand("count", сonnectbd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            сonnectbd.Open();//метод открытия подключения
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            int i = 0;
            richTextBox1.Text = "Колличество товаров с названием:" + textBox1.Text;
            while (reader.Read())
            {
                i++;
                richTextBox1.Text += String.Format(": {0}", reader[0]);
            }
            сonnectbd.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string connctSt = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;//подключение к источнику 
            сonnectbd = new SqlConnection(connctSt);
            cmd = new SqlCommand("date", сonnectbd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
            cmd.Parameters.Add("@name", SqlDbType.Int);
            cmd.Parameters["@name"].Direction = ParameterDirection.Output;
            сonnectbd.Open();//метод открытия подключения
            cmd.ExecuteNonQuery();
            richTextBox1.Text = "продукт с заданой датой:";
            richTextBox1.Text += String.Format("{0}", cmd.Parameters["@name"].Value.ToString());
            сonnectbd.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

