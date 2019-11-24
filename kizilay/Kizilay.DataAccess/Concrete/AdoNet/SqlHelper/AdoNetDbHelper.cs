using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.DataAccess.Concrete.AdoNet.SqlHelper
{
    public class AdoNetDbHelper
    {
        public OleDbConnection connection { get; private set; }
        public OleDbCommand command { get; set; }

        private string connectionString { get; set; }

        //string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Jet OLEDB:Database Password=db1dbea0", @"D:\Program Files\GitHub\Kizilay-yardim-takip-uygulamasi\kizilay\kizilay\bin\Debug\data.accdb");


        public AdoNetDbHelper()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string databaseFileName = Path.Combine(assemblyFolder, "data.accdb");

            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databaseFileName}; Jet OLEDB:Database Password=db1dbea0";

            connection = new OleDbConnection(connectionString);
            command = connection.CreateCommand();
        }

        public DataTable GetDataTable()
        {
            OpenSafeConnection();

            DataTable myTable = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            adapter.Fill(myTable);

            CloseSafeConnection();

            return myTable;
        }

        public OleDbCommand CreateCommandInCurrentConnection()
        {
            return connection.CreateCommand();
        }

        public bool Any(OleDbCommand command)
        {
            OpenSafeConnection();

            OleDbDataReader reader = command.ExecuteReader();

            bool isExists = false;

            if (reader.HasRows)
                isExists = true;

            reader.Close();
            reader.Dispose();

            CloseSafeConnection();

            return isExists;
        }

        public void OpenSafeConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void CloseSafeConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}
