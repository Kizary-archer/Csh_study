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
        SqlConnection connectWarehousebd = new SqlConnection();//строка подключения для MSSQL
        public warehouseDataSet ds = new warehouseDataSet();
        private SqlDataAdapter adapterclient;


        public clientsDB(string ConSts)
        {
            string connctSt = ConfigurationManager.ConnectionStrings[ConSts].ConnectionString;//подключение к источнику 
            connectWarehousebd = new SqlConnection(connctSt);
            adapterclient = new SqlDataAdapter("SELECT standalone_clients.* FROM standalone_clients", connectWarehousebd);
            adapterclient.Fill(ds, "standalone_clients");
           // MessageBox.Show("aasdas");
        }
        //методы добавления нового клиента
        private void adapSettInsert()
        {
            adapterclient.InsertCommand = new SqlCommand("Insert_client", (SqlConnection)connectWarehousebd);
            adapterclient.InsertCommand.CommandType = CommandType.StoredProcedure;
            adapterclient.InsertCommand.Parameters.Add("@Cname", SqlDbType.VarChar, 30, "Cname");
            adapterclient.InsertCommand.Parameters.Add("@surname", SqlDbType.VarChar, 30, "surname");
            adapterclient.InsertCommand.Parameters.Add("@patronymic", SqlDbType.VarChar, 30, "patronymic");
            adapterclient.InsertCommand.Parameters.Add("@Date_of_birth", SqlDbType.Date, 30, "Date_of_birth");
            adapterclient.InsertCommand.Parameters.Add("@phone", SqlDbType.Int, 4, "phone");

        }
        public void Insert(SecAccess access)
        {
            if (access.CName != null)
            {
                adapSettInsert();
                DataRow row = ds.standalone_clients.NewRow();
                row["Cname"] = access.CName;
                row["surname"] = access.Surname;
                row["patronymic"] = access.Patronymic;
                row["Date_of_birth"] = access.Date_of_Birth;
                row["phone"] = access.Phone;

                ds.standalone_clients.Rows.Add(row);
                adapterclient.Update(ds.standalone_clients);
                //ds.AcceptChanges();
            }
        }
        //методы удаления
        private void adapSetDel()
        {
            adapterclient.DeleteCommand = new SqlCommand("Delete_client", (SqlConnection)connectWarehousebd);
            adapterclient.DeleteCommand.CommandType = CommandType.StoredProcedure;
            adapterclient.DeleteCommand.Parameters.Add("@id_client", SqlDbType.Int, 4, "id_client");
        }
        public void Delete(SecAccess access)
        {
            adapSetDel();
            ds.standalone_clients.Rows[access.Id].Delete();
            adapterclient.Update(ds.standalone_clients);
        }
        //методы обновления
        private void adapSetUpdate()
        {

        }
        //
        public SecAccess GetClient(int id)
        {
            SqlCommand cmd = new SqlCommand("SelectClient", connectWarehousebd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_client", id);
            connectWarehousebd.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            SecAccess curClient = new SecAccess((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (DateTime)reader[4], (int)reader[5]);
            reader.Close();
            connectWarehousebd.Close();
            return curClient;

        }
        public List<SecAccess> GetAllClient()
        {
            SqlCommand cmd = new SqlCommand("SelectAllClient", connectWarehousebd);
            cmd.CommandType = CommandType.StoredProcedure;
            List<SecAccess> curClients = new List<SecAccess>();
            connectWarehousebd.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SecAccess curClient = new SecAccess();
                curClient.Id = (int)reader[0];
                curClient.CName = (string)reader[1];
                curClient.Surname = (string)reader[2];
                curClient.Patronymic = (string)reader[3];
                curClient.Date_of_Birth = (DateTime)reader[4];
                curClient.Phone = (int)reader[5];
                curClients.Add(curClient);
            }
            reader.Close();
            connectWarehousebd.Close();
            return curClients;
        }
    }

}
