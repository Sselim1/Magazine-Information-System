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
    public partial class BookList : Form
    {
         
        string usern;
        string usera;
        int useri;
        public BookList(int uiid, string uname, string uadmin)
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
        private void BookList_Load(object sender, EventArgs e)
        {
            label2.Text = usern;
            conn = new OracleConnection(ordb);
            list();
        }
        private void booklist()
        {

            
            string q = "select * from MAGAZINE where QUANTITY>0";
            Adapter = new OracleDataAdapter(q, ordb);
            DataSet ds = new DataSet();
            Adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
           // dataGridView1.d


        }
        private void list()
        {


            OracleCommand cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "magazin_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader(); 
            while (dr.Read())
            {
               
                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
            conn.Close();
        }
        int [] ret_info_MAg()
        {
            int[] inf = new int[3];
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "Getp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("name", comboBox1.SelectedItem);
            cmd.Parameters.Add("iid", OracleDbType.Int32, ParameterDirection.Output);
            cmd.Parameters.Add("pri", OracleDbType.Int32, ParameterDirection.Output);
            cmd.Parameters.Add("quan", OracleDbType.Int32, ParameterDirection.Output);
            cmd.ExecuteNonQuery();
            inf[0] = Convert.ToInt32(cmd.Parameters["iid"].Value.ToString());
            inf[1]= Convert.ToInt32(cmd.Parameters["pri"].Value.ToString());
            inf[2] = Convert.ToInt32(cmd.Parameters["quan"].Value.ToString());
            conn.Close();
            return inf;
        }
        int [] info = new int[3];
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           info = ret_info_MAg();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (info[2] - numericUpDown1.Value >= 0)
            {
                bunifuMaterialTextbox2.Text = Convert.ToString(info[1] * numericUpDown1.Value);
            }
            else
            {
                MessageBox.Show("From this magazine there are" + info[2]);
                numericUpDown1.Value = info[2];
                    }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        void makeorder()
        {
            if (comboBox1.Text == "")
                MessageBox.Show("Invalid data");
            else {
                        OracleCommand cmd1 = new OracleCommand();
                        conn.Open();
                        cmd1.Connection = conn;
                        cmd1.CommandText = "GetorderID";
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
                        cmd1.ExecuteNonQuery();
                        try
                        {
                            max = Convert.ToInt32(cmd1.Parameters["id"].Value.ToString());
                            newid = max + 1;
                        }
                        catch { newid = 0; }
                        try
                        {


                            OracleCommand cmd = new OracleCommand();

                            cmd.Connection = conn;
                            cmd.CommandText = "insert into orderr Values(:od,:Cost,:dorder,:useri)";
                            cmd.Parameters.Add("od", newid);
                            cmd.Parameters.Add("Cost", 1.0);
                            cmd.Parameters.Add("dorder", DateTime.Now.Date);
                            cmd.Parameters.Add("useri", useri);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            additem(newid);
                            /*comboBox1.Text = "";
                            numericUpDown1.Value = 0;
                            bunifuMaterialTextbox2.Text = "";*/
                        } catch (Exception e)
                        {
                            MessageBox.Show("Error in data");
                            conn.Close();
                        }
                    }
        }
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {


            //bunifuMaterialTextbox1.Text = "";

            makeorder();
            if (info[2] - numericUpDown1.Value != 0)
            {
                DialogResult dialogResult = MessageBox.Show("You Want to Add New item ?", "Make Order", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bunifuThinButton26.Visible = true;
                    bunifuThinButton24.Visible = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    savee();
                }
            }
            else { savee(); }


            // else {MessageBox.Show("From this magazine there are" + info[2]);
            // numericUpDown1.Value = info[2]; }
            comboBox1.Text = "";
            comboBox1.Items.Clear();
            numericUpDown1.Value = 0;
            bunifuMaterialTextbox2.Text = "";
            list();
        }



        void savee()
        {
            bunifuThinButton26.Visible = false;
            bunifuThinButton24.Visible = true;

            conn.Open();
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "update ORDERR set TOTAL=:newq where O_ID =:id";

            c.Parameters.Add("newq", Convert.ToInt32(bunifuMaterialTextbox1.Text).ToString());
            c.Parameters.Add("id", newid);
            c.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("order succsses");
            
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }
        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Main l = new Main( usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

            order l = new order(useri,  usern,  usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {

        }
        void additem(int newidd)
        {
            int maxc = 0,idc=0;
            if (comboBox1.Text == "")
                MessageBox.Show("Invalid data");
            else
            {
                conn.Open();
                OracleCommand cmd1 = new OracleCommand();

                cmd1.Connection = conn;
                cmd1.CommandText = "GetCRTID";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
                cmd1.ExecuteNonQuery();
                try
                {
                    maxc = Convert.ToInt32(cmd1.Parameters["id"].Value.ToString());
                    idc = maxc + 1;
                }
                catch { idc = 0; }
                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "insert into CART Values(:mid,:itemqty,:itemprice,:usero ,:cid)";
                cmd2.Parameters.Add("mid", info[0]);
                cmd2.Parameters.Add("itiemqty", numericUpDown1.Value);
                cmd2.Parameters.Add("itemprice", Convert.ToInt32(bunifuMaterialTextbox2.Text));
                cmd2.Parameters.Add("usero", newidd);
                cmd2.Parameters.Add("usero", idc);
                cmd2.ExecuteNonQuery();
                // conn.Close();
                string q = "select CC.CA_ID,M.NAMEE,CC.QUANTITY ,CC.PRICE from CART CC , MAGAZINE M where :oid = CC.OR_ID and CC.MA_ID = M.M_NO";
                DataSet ds1 = new DataSet();
                Adapter = new OracleDataAdapter(q, ordb);
                Adapter.SelectCommand.Parameters.Add(":oid", newidd);
                builder = new OracleCommandBuilder(Adapter);
                Adapter.Fill(ds1);
                dataGridView1.DataSource = ds1.Tables[0];
                // conn.Open();
                OracleCommand c = new OracleCommand();
                c.Connection = conn;
                c.CommandText = "select SUM(PRICE) from CART where  OR_ID=:id";
                c.Parameters.Add("id", newidd);
                OracleDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {

                    bunifuMaterialTextbox1.Text = dr[0].ToString();
                }
                dr.Close();
                OracleCommand cm = new OracleCommand();
                cm.Connection = conn;
                cm.CommandText = "update MAGAZINE set QUANTITY=:newqq where M_NO =:id";

                cm.Parameters.Add("newqq", (info[2] - numericUpDown1.Value));
                cm.Parameters.Add("id", info[0]);
                cm.ExecuteNonQuery();

                conn.Close();
            }
        }
                int max = 0, newid = 0;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            
                additem(newid);
            if (info[2] - numericUpDown1.Value != 0)
            {
                DialogResult dialogResult = MessageBox.Show("You Want to Add New item ?", "Make Order", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bunifuThinButton26.Visible = true;
                    bunifuThinButton24.Visible = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    savee();
                }
            }
            else
            {
                savee();
            }
                comboBox1.Text="";
            comboBox1.Items.Clear();
            numericUpDown1.Value = 0;
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox1.Text = "";
            list();
            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            magazinelist l = new magazinelist(useri, usern, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }
    }
}
