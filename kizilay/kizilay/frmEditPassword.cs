using Kizilay.Business.Abstract;
using Kizilay.Core.HashService;
using Kizilay.Core.ViewModels.Password;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay
{
    public partial class frmEditPassword : Form
    {
        private IAdminManager _adminManager { get; set; }

        public frmEditPassword(IAdminManager adminManager)
        {
            _adminManager = adminManager;
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel
            {
                Username = txtUsername.Text,
                LastPassword = txtLastPassword.Text,
                NewRePassword = txtRePassword.Text,
                NewPassword = txtNewPassword.Text
            };

            var result = _adminManager.ChangePasswordByViewModel(model);

            if (result.HasError())
            {
                var firstErrorMessage = result.Errors.FirstOrDefault();
                MessageBox.Show(firstErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            }

            else
            {
                MessageBox.Show("Güncelleme işlemi başarılı... yönlendiriliyorsunuz.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                this.Dispose();
                this.Close();
            }

        }
    }
}
