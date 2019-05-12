using System;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            ds.Relations["full_client"].Nested = true;

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
    }
}
