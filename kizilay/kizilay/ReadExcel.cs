using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kizilay
{
    public class ReadExcel
    {
        public string fileName { get; set; }

        public ReadExcel(string fileName)
        {
            this.fileName = fileName;
        }

        public DataTable ReadXml()
        {
            OleDbConnection connection = new OleDbConnection($"provider=Microsoft.ACE.OLEDB.12.0;Data Source='{this.fileName}';Extended Properties=Excel 8.0;");

            OleDbCommand command = connection.CreateCommand();

            connection.Open();

            command.CommandText = "SELECT * FROM [Sayfa1$]";

            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            connection.Close();
            connection.Dispose();

            return dataTable;
        }

    }
}
