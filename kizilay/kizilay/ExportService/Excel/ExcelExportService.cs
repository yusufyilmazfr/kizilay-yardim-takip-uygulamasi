using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelApplication = Microsoft.Office.Interop.Excel;


namespace kizilay.Services.ExportService.Excel
{
    public class ExcelExportService : IExportService
    {
        public void Export(string path, DataTable dataTable)
        {
            try
            {
                ExcelApplication.Application xlApp;
                ExcelApplication.Workbook xlWorkBook;
                ExcelApplication.Worksheet xlWorkSheet;
                ExcelApplication.Sheets excelSheet;
                ExcelApplication.Workbooks excelWorkBooks;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new ExcelApplication.Application();
                excelWorkBooks = xlApp.Workbooks;
                xlWorkBook = excelWorkBooks.Add(misValue);
                excelSheet = xlWorkBook.Worksheets;
                xlWorkSheet = (ExcelApplication.Worksheet)excelSheet.get_Item(1);

                var data = new object[dataTable.Rows.Count + 1, dataTable.Columns.Count];

                //basliklari ekliyelim 
                for (int x = 0; x < dataTable.Columns.Count; x++)
                {
                    data[0, x] = dataTable.Columns[x].ColumnName;
                }


                //basliklari kalin ve sutünun ortasina pozisyonliyalim
                var boldformat = xlWorkSheet.get_Range("A1", "W1");
                var m_objfont = boldformat.Font;
                m_objfont.Bold = true;

                var verformat = xlWorkSheet.get_Range("A1", "W1");
                verformat.VerticalAlignment = ExcelApplication.XlVAlign.xlVAlignCenter;

                //satirlari objemize ekliyelim
                for (var row = 1; row < dataTable.Rows.Count + 1; row++)
                {
                    for (var column = 1; column <= dataTable.Columns.Count; column++)
                    {
                        data[row, column - 1] = dataTable.Rows[row - 1][column - 1].ToString();
                    }
                }

                //K sütünün Text olarak formatliyalim 
                string endcelltelephone = "K" + (dataTable.Rows.Count + 1);
                var writeFormat = xlWorkSheet.get_Range("K1", endcelltelephone);
                writeFormat.NumberFormat = "@";

                //Objeyi ekliyecegimiz Range i belirliyelim
                string endcell = "W" + (dataTable.Rows.Count + 1);
                var writeR = xlWorkSheet.get_Range("A1", endcell);
                //objeyi hazirladigimiz range e ekliyelim
                writeR.Value2 = data;
                //kayit edelim

                DateTime now = DateTime.Now;

                string fileName = String.Format("Kızılay-kisi-listesi-{0}-{1}-{2}---saat-{3}-{4}",
                    now.Day,
                    now.Month,
                    now.Year,
                    now.Hour,
                    now.Minute);


                xlWorkBook.SaveAs(path + "/" + fileName, ExcelApplication.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, ExcelApplication.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

                data = null;

                xlWorkBook.Close(false, misValue, misValue);
                excelWorkBooks.Close();
                xlApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(verformat);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(boldformat);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objfont);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(writeFormat);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(writeR);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                verformat = null;
                boldformat = null;
                m_objfont = null;
                writeR = null;
                writeFormat = null;
                xlWorkSheet = null;
                excelSheet = null;
                excelWorkBooks = null;
                xlWorkBook = null;
                xlApp = null;

                GC.GetTotalMemory(false);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.GetTotalMemory(true);
            }
            catch (Exception ex)
            {

            }
        }

        public Task ExportAsync(string path, DataTable dataTable)
        {
            return Task.Run(() =>
            {
                Export(path, dataTable);
            });
        }
    }
}
