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
    public partial class magazinelist : Form
    {
        string usern;
        string usera;
        int useri;
        public magazinelist(int uiid, string uname, string uadmin)
        {
            InitializeComponent();
            usern = uname;
            usera = uadmin;
            useri = uiid;

        }
        OracleDataAdapter Adapter;
        DataSet ds;
        OracleCommandBuilder builder;
        string ordb = "Data source=orcl;User Id=scott; Password=scott;";
        OracleConnection conn;
        
        private void booklist()
        {


            string q = "select * from MAGAZINE where QUANTITY>0";
            Adapter = new OracleDataAdapter(q, ordb);
            DataSet ds = new DataSet();
            Adapter.Fill(ds);
            bunifuDataGridView1.DataSource = ds.Tables[0];
         
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Main l = new Main(usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            BookList l = new BookList(useri, usern, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            order l = new order(useri, usern, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            booklist();
        }

        private void magazinelist_Load_1(object sender, EventArgs e)
        {
            label2.Text = usern;
            conn = new OracleConnection(ordb);
            booklist();
        }
    }
}
