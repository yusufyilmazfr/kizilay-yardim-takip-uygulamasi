using kizilay.DependencyResolver.Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay
{
    public partial class frmFamilyDashboard : Form
    {

        public frmFamilyDashboard()
        {
            InitializeComponent();
        }

        private void btnNewFamily_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmNewFamily>().Show();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmNewPerson>().Show();
        }

        private void btnShowAllFamily_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmAllFamily>().Show();
        }

        private void btnEditFamily_Click(object sender, EventArgs e)
        {

        }

        private void btnShowAllFamily_Click_1(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmAllFamily>().Show();
        }

        private void btnEditFamily_Click_1(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmEditFamily>().Show();
        }
    }
}
