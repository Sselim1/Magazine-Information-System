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
    public partial class Print : Form
    {
        string usern;
        int useri,orderid;
        public Print(string us,int i,int o)
        {

            InitializeComponent();
            usern = us;
            useri = i;
            orderid = o;
        }
        CrystalReport3 cr;
        private void Print_Load(object sender, EventArgs e)
        {
            cr = new CrystalReport3();
            cr.SetParameterValue(0, usern);
            cr.SetParameterValue(1, orderid);
            cr.SetParameterValue(2, useri);
            crystalReportViewer1.ReportSource = cr;
        }
    }
}
