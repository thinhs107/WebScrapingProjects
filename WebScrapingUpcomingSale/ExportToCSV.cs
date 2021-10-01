using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace WebScrapingUpcomingSale
{
    class ExportToCSV
    {
        public void ExportDtToCsv(DataTable cvsDataTable)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;


            try
            {
                excel = new Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;

                //Creating a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                //Creating Work sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = "Upcoming Sale";

                // loop through each row and add values to our sheet
                int rowcount = 1;

                foreach (System.Data.DataRow datarow in cvsDataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= cvsDataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[1, i] = cvsDataTable.Columns[i - 1].ColumnName;
                        }
                        // Filling the excel file 
                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();
                    }
                }

                excelworkBook.SaveAs(@"C:\Users\"+Environment.UserName+@"\Documents\WebScraping\JeffersonUpcomingSale\FormatUpcomingSale.xlsx");
                excelworkBook.Close();
                excel.Quit();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }
        }
    }
}
