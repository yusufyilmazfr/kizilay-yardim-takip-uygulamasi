using kizilay.DependencyResolver.Ninject;
using Kizilay.Business.Abstract;
using Kizilay.Entities.Concrete;
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
    public partial class frmAccountLogin : Form
    {
        public IAdminManager _adminManager { get; set; }

        public frmAccountLogin(IAdminManager adminManager)
        {
            _adminManager = adminManager;

            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool isExists = _adminManager.AdminIsExistsByUsernameAndPassword(username, password);

            if (isExists)
            {
                MessageBox.Show("Giriş Başarılı:)", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var frmDashboard = FormDependencyResolver.Resolve<frmDashboard>();
                frmDashboard.Show();

                this.Hide();
            }
            else
                MessageBox.Show("Giriş Başarısız!", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void frmAccountLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
