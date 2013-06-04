using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Interop.Excel;

namespace CrunchBaseXLS
{
    public partial class CrunchSearchForm : Form
    {
        public CrunchSearchForm()
        {
            InitializeComponent();
        }
        private List<cbSearchResults> resultslist;

        private void SearchButton_Click(object sender, EventArgs e)
        {
            // Companies
            // Financial Organizations
            String entity=EntityCombo.Text=="Companies"? "company":"financial-organization";
            String querystring="";

            
            
            if (NameTextBox.Text!="")  // then create a  query
            {
                querystring = "query=" + NameTextBox.Text + "&entity=" + entity + (checkBoxTags.Checked ?"&field=tag_list":"&field=name");
            
                CrunchBaseConnect cbc = new CrunchBaseConnect();
                DiagnosticTimer.StartTimer("SearchCrunch");
                List<cbSearchResults> myresults = cbc.SearchResults(querystring);
                resultslist = myresults;
                SearchResultsGrid.DataSource = myresults;
                SearchResultsGrid.Columns[0].Width = 8*20;
                SearchResultsGrid.Columns[1].Width = 8*20;
                SearchResultsGrid.Columns[2].Width = 8*12;
                SearchResultsGrid.Columns[3].Width = 8*60;
                DiagnosticTimer.StopTimer("SearchCrunch");
            }

        }

        private void fundingRoundsConverterBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void InsertResultsButton_Click(object sender, EventArgs e)
        {
            DiagnosticTimer.StartTimer("InsertResults");
            List<cbSearchResults> sgrid = SearchResultsGrid.DataSource as List<cbSearchResults>;
            object[,] spread=new object[sgrid.Count+1,4];
            spread[0, 0] = "name";
            spread[0, 1] = "permalink";
            spread[0, 2] = "namespace";
            spread[0, 3] = "overview";
            for (int i = 0; i < sgrid.Count; i++)
            {
                spread[i + 1,0 ] = sgrid[i].name;
                spread[i + 1, 1] = sgrid[i].permalink;
                spread[i + 1, 2] = sgrid[i].Namespace;
                spread[i + 1, 3] = sgrid[i].overview;
            }
            var curr = Globals.ThisAddIn.Application.ActiveSheet;
            var startCell = Globals.ThisAddIn.Application.ActiveCell;
            var endCell = curr.Cells[startCell.Row + sgrid.Count, startCell.Column+3];
            var writeRange = curr.Range[startCell, endCell];
            DiagnosticTimer.StartTimer("WriteGridToExcel");
            writeRange.Value2 = spread;
            writeRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            DiagnosticTimer.StartTimer("FormatExcelGrid");
            writeRange.RowHeight = 14;
            // set the width of name and permalink columns to 140 pixels
            var startcol=startCell;
            var endcol=curr.Cells[endCell.Row,startCell.Column+1];
            var setcols = curr.Range[startcol, endcol].ColumnWidth = 20;
            // set width of permalink to 10
            startcol = curr.Cells[startCell.Row, startCell.Column + 2];
            endcol = curr.Cells[endCell.Row, startCell.Column + 2];
            setcols = curr.Range[startcol, endcol].ColumnWidth = 10;
            // set overview to 60

            startcol = curr.Cells[startCell.Row, startCell.Column + 3];
            endcol = curr.Cells[endCell.Row, startCell.Column + 3];
            setcols = curr.Range[startcol, endcol].ColumnWidth = 60;
            DiagnosticTimer.StopTimer("FormatExcelGrid");
            DiagnosticTimer.StopTimer("WriteGridToExcel");
            DiagnosticTimer.StopTimer("InsertResults");
            DiagnosticTimer.WriteAllTimersDiag();
        }

        private void SwitchToFieldsButton_Click(object sender, EventArgs e)
        {
            List<cbSearchResults> sl = resultslist;
            DetailsForm df = new DetailsForm(sl, NameTextBox.Text);
            df.Show();

        }
    }
}
