using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace CrunchBaseXLS
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private static void WriteArray(int rows, int columns, Worksheet worksheet)
        {
            var data = new object[rows, columns];
            for (var row = 1; row <= rows; row++)
            {
                for (var column = 1; column <= columns; column++)
                {
                    data[row - 1, column - 1] = "Test";
                }
            }

            var startCell = (Excel.Range)worksheet.Cells[1, 1];
            var endCell = (Excel.Range)worksheet.Cells[rows, columns];
            var writeRange = worksheet.Range[startCell, endCell];

            writeRange.Value2 = data;
        }
        public static Excel.Range OutputArrayToExcel(Excel.Range startCell, object[,] excelout)
        {
            var reportsheet = startCell.Worksheet;
            int lastrow = excelout.GetLength(0)-1; // number of rows, i.e. excelout[row,col]
            int lastcol = excelout.GetLength(1)-1; // number of cols 
            // 
            var endCell = reportsheet.Cells[startCell.Row + lastrow, startCell.Column + lastcol];
            var writerange = reportsheet.Range[startCell, endCell];
            writerange.Value2 = excelout;
            return writerange;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
