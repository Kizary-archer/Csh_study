using System;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using System.Data.Interop.OcOfficeLib;
//using Excel = OcOfficeLib.FormRegionContext;

namespace lab_10
{
    public partial class Form1 : Form
    {
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter;
        warehouseDataSet ds = new warehouseDataSet();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked) ds.Relations["full_client"].Nested = true;
            else ds.Relations["full_client"].Nested = false;

            saveFileDialog1.Title = "Запись файл в формате xml";
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                ds.WriteXml(saveFileDialog1.FileName + ".xml");
            }
            if (checkBox1.Checked)
            {
                saveFileDialog1.Title = "Запись схемы отношений xsd";
                if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    ds.WriteXmlSchema(saveFileDialog1.FileName + ".xsd");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;

            string connctSt = ConfigurationManager.ConnectionStrings["warehouseConnectionString"].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapter = new SqlDataAdapter("SELECT clients.* FROM clients", connectWarehousebd);
            adapter.Fill(ds, "clients");
            adapter = new SqlDataAdapter("SELECT passport.* FROM passport", connectWarehousebd);
            adapter.Fill(ds, "passport");
            ds.Relations.Add("full_client", ds.Tables["clients"].Columns["id_client"], ds.Tables["passport"].Columns["id_passport"]);

            bindingSource1.DataSource = ds.clients;
            dataGridView1.DataSource = bindingSource1;
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "full_client";
            dataGridView2.DataSource = bindingSource2;
            ds.Relations["full_client"].Nested = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            SqlCommand cmd = new SqlCommand("select * from clients for xml auto, elements", (SqlConnection)connectWarehousebd);
            XmlReader reader;
            StringBuilder str = new StringBuilder();
            connectWarehousebd.Open();
            //выполнение sql команды select c предложение  for XML
            reader = cmd.ExecuteXmlReader();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        str.Append("<" + reader.Name + ">");
                        break;
                    case XmlNodeType.EndElement:
                        str.Append("</" + reader.Name + ">" + "\n\r");
                        break;
                    case XmlNodeType.Text:
                        str.Append(reader.Value);
                        break;
                    default:
                        break;
                }
            }
            connectWarehousebd.Close();
            richTextBox1.Text = str.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Excel.Application ExcelApp = new Excel.Application(); //открываем новое приложение excel
            Excel.Workbook ExcelWorkbook; //создаем новую книгу
            Excel.Worksheet ExcelWorksheet; //создаем новый лист
            ExcelWorkbook = ExcelApp.Workbooks.Add(); //добавляем книгу в приложение
            ExcelWorksheet = (Excel.Worksheet)ExcelWorkbook.Worksheets.get_Item(1); //используем первый лист в книге
            for (int i = 1; i < dataGridView2.Columns.Count + 1; i++) //идем по столбцам первой строки
            {
                ExcelWorksheet.Cells[1, i] = dataGridView2.Columns[i - 1].HeaderText; //добавляем названия стобцов
            }
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++) //идем по строкам таблицы
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++) //идем по столбцам
                {
                    ExcelWorksheet.Cells[i + 2, j + 1] = dataGridView2.Rows[i].Cells[j].Value.ToString(); //заносим знаение в ячейки
                }
            }
            ExcelApp.Visible = true; //показываем приложение excel*/

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            ds.Clear();
            richTextBox1.Clear();
            bindingSource1.DataSource = ds.clients;
            bindingSource2.DataSource = bindingSource1;
            bindingSource2.DataMember = "full_client";
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            warehouseDataSet ds2 = new warehouseDataSet();
            openFileDialog1.Title = "Выберите XML файлы";
            openFileDialog1.Filter = "XML(*.xml)|*.xml|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                ds2.ReadXml(openFileDialog1.FileName);

                ds2.Relations.Add("full_client2", ds2.clients.Columns["id_client"], ds2.passport.Columns["id_passport"]);

                bindingSource1.DataSource = ds2.clients;
                dataGridView1.DataSource = bindingSource1;
                bindingSource2.DataSource = bindingSource1;
                if (checkBox2.Checked) bindingSource2.DataMember = "full_client2";
                dataGridView2.DataSource = bindingSource2;
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
