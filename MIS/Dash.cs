using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Shared;

namespace MIS
{
    public partial class Dash : Form
    {
        CrystalReport1 cr;
        CrystalReport2 cr1;

        string usern, usera; int useri;
        public Dash(string msg, int id, string admin)
        {
            usern = msg; usera = admin; useri = id;
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            cr.SetParameterValue(0, comboBox1.Text);
            crystalReportViewer1.ReportSource = cr;
        }

        private void Dash_Load(object sender, EventArgs e)
        {
            cr1 = new CrystalReport2();
            cr = new CrystalReport1();
            foreach (ParameterDiscreteValue v in cr.ParameterFields[0].DefaultValues)
                comboBox1.Items.Add(v.Value);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = cr1;
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Main l = new Main(usern, useri, usera);
            l.Show();
            this.Hide();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            control l = new control(usern, useri, usera);
            l.Show();
            this.Hide();
        }
    }
}
