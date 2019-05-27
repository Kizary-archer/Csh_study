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
        //метод обновления
        public void Update(SecAccess access)
        {
            SqlCommand cmd = new SqlCommand("UpdateClient", connectWarehousebd);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_client", access.Id);
            cmd.Parameters.AddWithValue("@Cname", access.CName);
            cmd.Parameters.AddWithValue("@surname", access.Surname);
            cmd.Parameters.AddWithValue("@Patronymic", access.Patronymic);
            cmd.Parameters.AddWithValue("@Date_of_birth", access.Date_of_Birth);
            cmd.Parameters.AddWithValue("@phone", access.Phone);
            connectWarehousebd.Open();
            cmd.ExecuteNonQuery();
            connectWarehousebd.Close();
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
            SecAccess curClient = new SecAccess();
            curClient.Id = (int)reader["id_client"];
            curClient.CName = (string)reader["Cname"];
            curClient.Surname = (string)reader["surname"];
            curClient.Patronymic = (string)reader["patronymic"];
            curClient.Date_of_Birth = (DateTime)reader["Date_of_birth"];
            curClient.Phone = (int)reader["phone"];
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
                curClient.Id = (int)reader["id_client"];
                curClient.CName = (string)reader["Cname"];
                curClient.Surname = (string)reader["surname"];
                curClient.Patronymic = (string)reader["patronymic"];
                curClient.Date_of_Birth = (DateTime)reader["Date_of_birth"];
                curClient.Phone = (int)reader["phone"];
                curClients.Add(curClient);
            }
            reader.Close();
            connectWarehousebd.Close();
            return curClients;
        }
    }

}
