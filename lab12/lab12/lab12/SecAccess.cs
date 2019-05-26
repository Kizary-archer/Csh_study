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

    class SecAccess
    {
        private string Cname;
        private string surname;
        private string patronymic;
        private DateTime Date_of_birth;
        private int phone;
        private int id;

        public string CName { get => Cname; set => Cname = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Patronymic { get => patronymic; set => patronymic = value; }
        public DateTime Date_of_Birth { get => Date_of_birth; set => Date_of_birth = value; }
        public int Phone { get => phone; set => phone = value; }
        public int Id { get => id; set => id = value; }

        public SecAccess(string Cname,string surname,string patronymic,DateTime Date_of_birth, int phone)   //insert
        {
            if(Cname !="" && surname!="" && patronymic != "" && phone>0)
            {
                this.CName = Cname;
                this.Surname = surname;
                this.Patronymic = patronymic;
                this.Date_of_Birth = Date_of_birth;
                this.Phone = phone; 
            }
            else MessageBox.Show("введены некоректные данные");
        }
        public SecAccess(int id)    //delete
        {
            this.Id = id;
        }
        public SecAccess(int id, string Cname, string surname, string patronymic, DateTime Date_of_birth, int phone)    //delete
        {
            this.Id = id;
        }
    }
}
