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
    class clientsDB
    {
        public warehouseDataSet ds = new warehouseDataSet();

        public clientsDB(string ConSts)
        {
            string connctSt = ConfigurationManager.ConnectionStrings[ConSts].ConnectionString;//подключение к источнику 
            SqlConnection connectWarehousebd = new SqlConnection(connctSt);
            SqlDataAdapter adapterclient;
            adapterclient = new SqlDataAdapter("SELECT standalone_clients.* FROM standalone_clients", connectWarehousebd);
            adapterclient.Fill(ds, "standalone_clients");
           // MessageBox.Show("aasdas");
        }
        private void Insert()
        {
           
        }
    }
}
