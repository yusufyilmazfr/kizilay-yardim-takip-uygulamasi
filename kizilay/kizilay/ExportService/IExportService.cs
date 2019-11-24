using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kizilay.Services.ExportService
{
    public interface IExportService
    {
        void Export(string path, DataTable dataTable);
        Task ExportAsync(string path, DataTable dataTable);
    }
}
