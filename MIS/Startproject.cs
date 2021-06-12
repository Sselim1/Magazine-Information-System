using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Startproject : Form
    {
        public Startproject()
        {
            InitializeComponent();
        }
        int start = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            start += 1;
            Star.Value = start;
            prec.Text = "% " + start;
            if (Star.Value == 50)
            {
                Star.Value = 0;
                timer1.Stop();
                Login l = new Login();
                l.Show();
                this.Hide();

            }
        }

        private void Startproject_Load(object sender, EventArgs e)
        {
            timer1.Start();
            
        }
    }
    }

