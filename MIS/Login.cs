using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MIS;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        string ordb = "Data source=orcl;User Id=scott; Password=scott;";
        OracleConnection con;
       
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
        string name,admin;int id;
        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            try
            {
                // con.Open();
                OracleCommand cmd = new OracleCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select USERNAME ,U_ID ,ADMIN from USERR where username =:id  and pass =:pass ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("id", bunifuMaterialTextbox1.Text);
                cmd.Parameters.Add("pass", bunifuMaterialTextbox2.Text);
                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    name = dr[0].ToString();
                    id = Convert.ToInt32(dr[1].ToString());
                    admin = dr[2].ToString();
                    //msg = bunifuMaterialTextbox1.Text;

                    Main at = new Main(name,id,admin);
                    at.Show();
                    this.Hide();
                }


                /* MessageBox.Show("Welcome to the cinema booking system" +
                 ".We hope we can receive your praise ");*/

                else
                {

                    MessageBox.Show("Not a Registration User Are Invalid User name or passowrd");
                }
                dr.Close();

                con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Invalid Data");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            con = new OracleConnection(ordb);
        }
    }
}
