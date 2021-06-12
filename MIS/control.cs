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
namespace MIS
{
    public partial class control : Form
    {
        OracleDataAdapter Adapter;
        DataSet ds, ds1;
        OracleCommandBuilder builder;
        string ordb = "Data source=orcl;User Id=scott; Password=scott;";
        OracleConnection conn;

        string usern, usera; int useri;
        public control(string msg, int id, string admin)
        {
            usern = msg; usera = admin; useri = id;
            InitializeComponent();
            conn = new OracleConnection(ordb);
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Main l = new Main(usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                Adapter.Update(ds.Tables[0]);
                MessageBox.Show("Update");
            }catch(Exception ee)
            {
                MessageBox.Show("Cant delete category");
            }
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Dash l = new Dash(usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem=="user")
            {
                string q = "select * from USERR ";
                ds = new DataSet();
                Adapter = new OracleDataAdapter(q, ordb);
                builder = new OracleCommandBuilder(Adapter);
                Adapter.Fill(ds);
                ds1 = ds;
                dataGridView1.DataSource = ds.Tables[0];
            }
            else if(comboBox1.SelectedItem == "magazine")
            {
                string q = "select * from MAGAZINE ";
                ds = new DataSet();
                Adapter = new OracleDataAdapter(q, ordb);
                builder = new OracleCommandBuilder(Adapter);
                Adapter.Fill(ds);
                ds1 = ds;
                dataGridView1.DataSource = ds.Tables[0];
            }

        }
    }
}
