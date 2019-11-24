using kizilay.DependencyResolver.Ninject;
using kizilay.Services.ExportService;
using kizilay.Services.ExportService.Excel;
using Kizilay.Business.Abstract;
using Kizilay.Business.Concrete.Services.ImportService;
using Kizilay.Business.Concrete.Services.ImportService.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace kizilay
{
    public partial class frmDashboard : Form
    {
        private IPersonManager _personManager { get; set; }
        private IImportService _importService { get; set; }

        public frmDashboard(IPersonManager personManager, IImportService importService)
        {
            _personManager = personManager;
            _importService = importService;

            InitializeComponent();
        }

        public async void StartImport()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;

                var dataTable = ((IReadableFile)_importService).ReadFile(fileName);

                _importService.ImportAsync(dataTable)
                    .ContinueWith((invalidDataList) =>
                    {
                        if (invalidDataList.Result != null && invalidDataList.Result.Rows.Count > 0)
                        {
                            string message = $"Yüklenemeyen {invalidDataList.Result.Rows.Count} satır kayıt var. Bunları indirmek ister misiniz?";

                            var messageResult = MessageBox.Show(message, "Bilgi?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (messageResult == DialogResult.Yes)
                            {
                                string applicationPath = Application.StartupPath;
                                WriteDataTableToExcelAsync(applicationPath, invalidDataList.Result);
                            }
                        }
                        else
                        {
                            if (invalidDataList.Result.Rows.Count == 0)
                            {
                                MessageBox.Show("Kayıtlar başarıyla aktarıldı!");
                            }
                        }
                        dataTable.Dispose();
                    });
            }
            else
            {
                MessageBox.Show("Lütfen import yapacak dosyayı seçiniz...", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void WriteDataTableToExcelAsync(string path, DataTable dataTable)
        {
            IExportService export = new ExcelExportService();

            export.ExportAsync(path, dataTable)
                .ContinueWith(task =>
                {
                    MessageBox.Show("Kayıtlar başarıyla dışarıya aktarıldı..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataTable.Dispose();
                });

        }

        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnSocialSecurity_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmSocialSecurity>().ShowDialog();
        }

        private void btnEducational_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmEducationalStatus>().ShowDialog();
        }

        private void btnHousing_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmHousing>().ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmEditPassword>().ShowDialog();
        }

        private void btnFamily_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmFamilyDashboard>().Show();
        }

        private void btnDonation_Click(object sender, EventArgs e)
        {
            FormDependencyResolver.Resolve<frmDonateDashboard>().Show();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();

            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;

                var dataTable = _personManager.GetAllPersonInformationForExport();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i]["Gender"] = dataTable.Rows[i]["Gender"].ToString().Trim('-');
                    dataTable.Rows[i]["JobState"] = dataTable.Rows[i]["JobState"].ToString().Trim('-');
                }

                WriteDataTableToExcelAsync(path, dataTable);
            }
            else
                MessageBox.Show("Lütfen bir konum seçiniz...");

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            StartImport();
        }
    }
}
