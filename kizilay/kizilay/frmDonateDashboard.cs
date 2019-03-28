using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay
{
    public partial class frmDonateDashboard : Form
    {
        public frmDonateDashboard()
        {
            InitializeComponent();
        }

        private void btnFamily_Click(object sender, EventArgs e)
        {
            new frmNewDonate().ShowDialog();
        }

        private void btnNewDonate_Click(object sender, EventArgs e)
        {
            new frmDonate().Show();
        }
    }
}
