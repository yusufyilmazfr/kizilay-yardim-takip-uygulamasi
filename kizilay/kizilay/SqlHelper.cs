using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kizilay
{
    class SqlHelper
    {
        public OleDbConnection connection { get; private set; }
        public OleDbCommand command { get;  set; }

        public string connectionString { get; private set; } =
            string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Jet OLEDB:Database Password=db1dbea0", Application.StartupPath + "\\data.accdb");

        public SqlHelper()
        {
            connection = new OleDbConnection(connectionString);
            command = connection.CreateCommand();
        }
    }
}
