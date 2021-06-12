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
    public partial class order : Form
    {
        string usern;
        string usera;
        int useri;
        public order(int uiid, string uname, string uadmin)
        {
            InitializeComponent();
            usern = uname;
            usera = uadmin;
            useri = uiid;

        }
        OracleDataAdapter Adapter,adapter1;
        DataSet ds,ds1;
        OracleCommandBuilder builder,builder1;
        string ordb = "Data source=orcl;User Id=scott; Password=scott;";
        OracleConnection conn;
        private void cart_Load(object sender, EventArgs e)
        {
            label2.Text = usern;
            conn = new OracleConnection(ordb);
           // booklist();
            list();
        }
        void displayorder()
        {
            try { 
            string q = "select * from ORDERR where :oid = O_ID ";
            ds1 = new DataSet();
            adapter1 = new OracleDataAdapter(q, ordb);
            adapter1.SelectCommand.Parameters.Add(":oid", Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            builder1 = new OracleCommandBuilder(adapter1);
            adapter1.Fill(ds1);
            bunifuDataGridView2.DataSource = ds1.Tables[0];
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
        }
        void displayitem()
        {
            try { 
           string q = "select CC.CA_ID, M.NAMEE ,CC.QUANTITY,CC.PRICE from CART CC,MAGAZINE M where :oid = CC.OR_ID and CC.MA_ID = M.M_NO";
            ds = new DataSet();
            Adapter = new OracleDataAdapter(q, ordb);
            Adapter.SelectCommand.Parameters.Add(":oid", Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            builder = new OracleCommandBuilder(Adapter);
            Adapter.Fill(ds);

            bunifuDataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
        }
        void deleteorder()
        {
            try { 
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "Delete from ORDERR  where O_ID=:id";
            cmd.Parameters.Add("id", Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            int r1 = cmd.ExecuteNonQuery();
            if (r1 != -1)
            {
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);

            }
            conn.Close();
            Adapter.Update(ds.Tables[0]);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
        }
        void deleteallitem()
        {
            try { 
            OracleCommand c = new OracleCommand();
            conn.Open();
            c.Connection = conn;
            c.CommandText = "Delete from CART where OR_ID=:id";
            c.Parameters.Add("id", Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            int r = c.ExecuteNonQuery();
            conn.Close();
            Adapter.Update(ds.Tables[0]);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
        }
        void deleteoneitem(int i)
        {
            upqty(i);
            upprice(i);
            try { 
            OracleCommand c = new OracleCommand();
            conn.Open();
            c.Connection = conn;
            c.CommandText = "Delete from CART  where CA_ID=:id";
            c.Parameters.Add("id", i);
            int r = c.ExecuteNonQuery();
            
            MessageBox.Show("Item deleted");
            bunifuDataGridView1.Refresh();
            conn.Close();
            Adapter.Update(ds.Tables[0]);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
        }
        int countofitem()
        {
            int r = 0;
            try
            {
                conn.Open();
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select COUNT(CA_ID) from CART where  OR_ID=:id";
            c.Parameters.Add("id", Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {

                r= Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            conn.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
            return r;
        }
        int[] quantity(int i)
        {
            int [] qty=new int[3];
            OracleCommand c = new OracleCommand();
            conn.Open();
            c.Connection = conn;
            c.CommandText = " select CC.MA_ID ,M.QUANTITY ,CC. QUANTITY from MAGAZINE M ,CART CC where CC.CA_ID = :id and M.M_NO=CC.MA_ID";
            c.Parameters.Add("id", i);
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                qty[0] =Convert.ToInt32( dr[0].ToString());
                qty[1] = Convert.ToInt32(dr[1].ToString());
                qty[2] = Convert.ToInt32(dr[2].ToString());
            }
            dr.Close();
            conn.Close();
            return qty;
        }
        void upqty(int i)
        {
            int[] qty = quantity(i);
            OracleCommand cm = new OracleCommand();
            conn.Open();
            cm.Connection = conn;
            cm.CommandText = "update MAGAZINE set QUANTITY=:newqq where M_NO =:id";

            cm.Parameters.Add("newqq", (qty[1] + qty[2]));
            cm.Parameters.Add("id", qty[0]);
            cm.ExecuteNonQuery();

                conn.Close();
        }
        void upprice(int i)
        {
            int price = 0, total = 0, ido = 0 ;
            OracleCommand c = new OracleCommand();
            conn.Open();
            c.Connection = conn;
            c.CommandText = "select CC.PRICE ,CC.OR_ID ,OO.TOTAL from ORDERR OO, CART CC where CC.CA_ID = :id and OO.O_ID = CC.OR_ID"; 
                //"select CC.PRICE ,OO.TOTAL,CC.OR_ID  from CART CC,ORDERR OO  where CC.CA_ID = :id,CC.OR_ID= OO.O_ID ";
            c.Parameters.Add("id", i);
            
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                price = Convert.ToInt32(dr[0].ToString());
                total = Convert.ToInt32(dr[2].ToString());
                ido = Convert.ToInt32(dr[1].ToString());
            }
            dr.Close();

            OracleCommand cm = new OracleCommand();
            //conn.Open();
            cm.Connection = conn;
            cm.CommandText = "update ORDERR set TOTAL=:newqq where O_ID =:id";

            cm.Parameters.Add("newqq", (total-price));
            cm.Parameters.Add("id", ido);
            cm.ExecuteNonQuery();

            conn.Close();
        }
        int retcid()
        {
            int r = 0;
            
                conn.Open();
                OracleCommand c = new OracleCommand();
                c.Connection = conn;
                c.CommandText = "select CA_ID from CART where  OR_ID=:id";
                c.Parameters.Add("id", Convert.ToInt32(comboBox1.SelectedItem.ToString()));
                OracleDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {

                    r = Convert.ToInt32(dr[0].ToString());
                break;
                }
                dr.Close();
                conn.Close();
            return r;
            }

        private void list()
        {


            OracleCommand cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "order_list";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("userid", useri);
            cmd.Parameters.Add("oid", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                comboBox1.Items.Add(dr[0]);
            }
            dr.Close();
            conn.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayorder();
            displayitem();
        }
       
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            BookList l = new BookList(useri, usern, usera);
            l.Show();
            this.Hide();
        }
        int selectedc = 0;
        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = bunifuDataGridView1.Rows[e.RowIndex];
            selectedc = Convert.ToInt32(row.Cells[0].Value.ToString());
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Print l = new Print(usern,useri, Convert.ToInt32(comboBox1.SelectedItem.ToString()));
            l.Show();
            
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            magazinelist l = new magazinelist(useri, usern, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            DataRow dr2 = ds1.Tables[0].Rows[0];
            try
            {
                if (DateTime.Now.Day - Convert.ToDateTime(dr2[2].ToString()).Day>=3&& DateTime.Now.Month == Convert.ToDateTime(dr2[2].ToString()).Month)
                {
                    MessageBox.Show("not allow");
                }
                else
                {
                int index = countofitem(), ca = retcid() ;
                for (int i = 0; i < index; i++)
                {
                    deleteoneitem(ca+i);
                }
                    deleteorder();
                    
                   
                    MessageBox.Show("order deleted");
                    bunifuDataGridView1.DataSource = null;
                    bunifuDataGridView2.DataSource = null;
                    conn.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Main l = new Main(usern, useri , usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        
      {

            try
            {

                DataRow dr2 = ds1.Tables[0].Rows[0];
                if (DateTime.Now.Day - Convert.ToDateTime(dr2[2].ToString()).Day >= 3 && DateTime.Now.Month == Convert.ToDateTime(dr2[2].ToString()).Month)
                {
                    MessageBox.Show("not allow");
                }
                else
                {
                int r = countofitem();
                    if (r > 1)
                    {
                        
                        deleteoneitem(selectedc);
                    displayitem();
                    }
                    else if(r == 1)
                    {
                        
                        deleteoneitem(selectedc);
                        deleteorder();
                    bunifuDataGridView1.DataSource = null;
                    bunifuDataGridView2.DataSource = null;
                }
                }
        }catch(Exception ee)
            {
                MessageBox.Show("Invalid data");
            }
}
    }
}
