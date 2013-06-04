using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;

namespace CrunchBaseXLS
{
    public partial class DetailsForm : Form
    {
        public DetailsForm(List<cbSearchResults> entities, string searchscope)
        {
            InitializeComponent();
            EntityList = entities;
            TotalLabel.Text = entities.Count.ToString("#,#");
            ScopeDesc.Text = searchscope;
        }

        private void DetailsForm_Load(object sender, EventArgs e)
        {

        }

        public List<cbSearchResults> EntityList;
        
        private void AggregateButton_Click(object sender, EventArgs e)
        {
            var reportsheet = Globals.ThisAddIn.Application.ActiveSheet;
            if (NewSheetCheck.Checked) 
            {
                reportsheet = Globals.ThisAddIn.Application.ActiveWorkbook.Worksheets.Add();
                reportsheet.Activate();
            }
            var startCell = Globals.ThisAddIn.Application.ActiveCell;
            // normalize into entity lists companies and finance orgs; ignore others for now
            SearchResultsList financecompanies=new SearchResultsList();
            SearchResultsList nonfincompanies=new SearchResultsList();
            SearchResultsList entitiesnotfound = new SearchResultsList();  // add companies that we can't find in crunchbase to this list

            foreach (cbSearchResults entry in EntityList)
            {
                switch (entry.Namespace)
                {
                    case "financial-organization" : {
                        financecompanies.Add(entry);
                        break;
                    }
                    case "company":
                        {
                            nonfincompanies.Add(entry);
                            break;
                        }
                    default:
                        break;
                }
            }
            // output the finance org table
            startCell.Value2 = "Crunchbase Search on " + DateTime.Now.ToLongTimeString() + " Scope: " + ScopeDesc.Text;
            // move startcell one cell lower
            startCell = reportsheet.Cells[startCell.Row + 1, startCell.Column];
            if (financecompanies.Count > 0)
            {
                // output a finance orgs table, first the headers
                CrunchFinancial cfh = new CrunchFinancial();
                object[,] excelout = new object[financecompanies.Count+1, cfh.headersaggregate.Count()];
                // put the header in row 0
                for (int i = 0; i < cfh.headersaggregate.Count(); i++)
                {
                    excelout[0, i] = cfh.headersaggregate[i];
                }
                int currrow = 1;
                CrunchBaseConnect cbc=new CrunchBaseConnect();
                foreach (cbSearchResults f in financecompanies) 
                {
                    CrunchFinancial fc = cbc.GetCrunchFinanceOrg(f.permalink);

                    if (fc!=null)
                    {
                        for (int i = 0; i < fc.headersaggregate.Count(); i++)
                        {
                            excelout[currrow, i] = fc.aggregateitems[i];
                        }
                        currrow++; 
                    } else
                    { // add to not found list
                        entitiesnotfound.Add(f);
                    }
                }
                //now output to excel
                Range  financerange = ThisAddIn.OutputArrayToExcel(startCell, excelout);
                // now move startCell below the financerange
                startCell = reportsheet.Cells[startCell.Row + financerange.Rows.Count+1, startCell.Column];
            }

            // now do companies entity
            if (nonfincompanies.Count>0)
            {
                CrunchBaseConnect cbc = new CrunchBaseConnect();
                CrunchBase cb = new CrunchBase();
                int currow = 1;  // 0 has the header
                object[,] excelout = new object[nonfincompanies.Count + 1, cb.aggregateheaders.Count()];
                // output headers to row 0
                for (int i = 0; i < cb.aggregateheaders.Count(); i++)
                {
                    excelout[0, i] = cb.aggregateheaders[i];
                }
                foreach (cbSearchResults c in nonfincompanies)
                {
                    CrunchBase cp = cbc.GetCrunchCompany(c.permalink);
                    if (cp != null)
                    {
                        for (int i = 0; i < cp.aggregateheaders.Count(); i++)
                        {
                            excelout[currow, i] = cp.aggregaterow[i];
                        }
                        currow++;

                    }
                    else
                    {
                        entitiesnotfound.Add(c);
                    }
                }
                Range companyrange = ThisAddIn.OutputArrayToExcel(startCell, excelout);
                this.Close();

            }
          } //aggregate button click method

        



    }  // details form
}
