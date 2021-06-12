using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using WindowsFormsApp1;

namespace MIS
{
    public partial class Main : Form
    {
        string usern, usera; int useri;
        string ordb = "Data source=orcl;User Id=scott; Password=scott;";
        OracleConnection conn;
        public Main(string msg, int id, string admin)
        {
            usern= msg;usera = admin;useri = id;
            InitializeComponent();
            conn = new OracleConnection(ordb);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            order l = new order( useri,  usern,  usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            BookList l = new BookList(useri,usern,usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Dash l = new Dash(usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            control l = new control(usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            magazinelist l = new magazinelist(useri, usern, usera);
            l.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            label2.Text = usern;
            if(usera== "y")
            {
                bunifuThinButton21.Hide();
                bunifuThinButton22.Hide();
                bunifuThinButton26.Hide();
                bunifuThinButton23.Visible=true;
                bunifuThinButton25.Visible=true;
                panel4.Visible = true;
                panel5.Visible = true;
                panel6.Visible = true;
                conn.Open();
                OracleCommand c = new OracleCommand();
                c.Connection = conn;
                c.CommandText = "select count(M_NO) from MAGAZINE";
                c.CommandType = CommandType.Text;
                OracleDataReader dr = c.ExecuteReader();
                if (dr.Read())
                {
                    label3.Text = dr[0].ToString();
                    
                }
                dr.Close();
                OracleCommand c1 = new OracleCommand();
                c1.Connection = conn;
                c1.CommandText = "select count(U_ID) from USERR";
                c1.CommandType = CommandType.Text;
                OracleDataReader dr1 = c1.ExecuteReader();
                if (dr1.Read())
                {
                    label5.Text = dr1[0].ToString();

                }
                dr1.Close();
                OracleCommand c2 = new OracleCommand();
                c2.Connection = conn;
                c2.CommandText = "select sum(TOTAL) from ORDERR";
                c2.CommandType = CommandType.Text;
                OracleDataReader dr2 = c2.ExecuteReader();
                if (dr2.Read())
                {
                    label7.Text = dr2[0].ToString();

                }
                dr2.Close();

                conn.Close();
            }
            else
            {
                bunifuThinButton23.Hide();
                bunifuThinButton25.Hide();
                bunifuThinButton21.Visible = true;
                bunifuThinButton22.Visible = true;
                bunifuThinButton26.Visible = true;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
            }
        }
    }
}
