using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Business.Concrete.Services.ImportService
{
    public interface IImportService
    {
        Task<DataTable> ImportAsync(DataTable dataTable);
        DataTable Import(DataTable dataTable);
    }
}
