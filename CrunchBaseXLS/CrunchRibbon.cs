using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Interop.Excel;

namespace CrunchBaseXLS
{
    public partial class CrunchRibbon
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void CompaniesButon_Click(object sender, RibbonControlEventArgs e)
        {
            // insert companies into spreadsheet
            CrunchBaseConnect cbc=new CrunchBaseConnect();
            InsertCompaniesAtCell(cbc.Companies, "company", Globals.ThisAddIn.Application.ActiveCell);
        }
        /// <summary>
        /// Inserts all the contents of a supplied cbCompanyObject list in the active cell, formating the permalink to be www.crunchbase.com/#space#/#permalink#
        /// </summary>
        /// <param name="cbcompanylist">A list of cbCompanyObject</param>
        /// <param name="space">string used to specify part of crunchbase namespace</param>
        /// <param name="activecell">An excel range where the list should be inserted</param>
        private static void InsertCompaniesAtCell(List<cbCompanyObject> cbcompanylist,string space, Microsoft.Office.Interop.Excel.Range activecell)
        {
            int countcompany = cbcompanylist.Count + 1;
            string[,] companies = new string[countcompany, 1];
            string[,] permalink = new string[countcompany, 1];  // actual hyperlink to crunchbase entry
            string[,] shortperma = new string[countcompany,1]; // just short part of permalink
            // put headers in the 0 row
            companies[0, 0] = "Company Name";
            shortperma[0, 0] = "Short Permalink";
            permalink[0, 0] = "Company Permalink";
            int row = 1;
            foreach (cbCompanyObject c in cbcompanylist)
            {
                companies[row, 0] = c.name;
                shortperma[row,0]=c.permalink;
                if (c.permalink.Length < 200)
                {
                    permalink[row++, 0] = "=HYPERLINK(\"" + "http://www.crunchbase.com/"+space+"/" + c.permalink + "\")";
                }
                else
                {
                    permalink[row++, 0] = "ERROR: TOO LONG FOR EXCEL";
                }
                
            }
            var curr = activecell.Worksheet;
            var startCell = activecell;
            var endCell = curr.Cells[startCell.Row + row - 1, startCell.Column];
            var writeRange = curr.Range[startCell, endCell];
            writeRange.Value2 = companies;
            //format the permalink column as a hyperlink (for slow way)
            startCell = curr.Cells[startCell.Row, startCell.Column + 1]; // set just to the 2nd column in the range
            endCell = curr.Cells[startCell.Row, startCell.Column];
            var writePermalinkHeader = Globals.ThisAddIn.Application.ActiveCell.get_Offset(0, 1);
            writePermalinkHeader.Value2 = permalink[0, 0];
            //

            startCell = curr.Cells[startCell.Row + 1, startCell.Column]; // set just to the 2nd column in the range
            endCell = curr.Cells[startCell.Row + row - 1, startCell.Column];

            var hyperlinkcol = curr.Range[startCell, endCell];
            //the fast way that doesn't work
            //hyperlinkcol.FormulaLocal = permalink;
            // THE SLOW WAY
            System.DateTime slowstart = System.DateTime.Now;
            for (int i = 1; i < countcompany; i++)
            {
                hyperlinkcol.Item(i).Formula = permalink[i, 0];
            }
            Console.Error.WriteLine("time to format hyperlinks the slow way {0}", System.DateTime.Now - slowstart);
            //hack hack add the shortperma column
            
            startCell=curr.Cells[activecell.Row,activecell.Column+2];  //2 over
            ThisAddIn.OutputArrayToExcel(startCell,shortperma);
            // add stuff here-->endCell=curr.
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            string company = FreeSearch.Text;
            CrunchBaseConnect cbc = new CrunchBaseConnect();
            CrunchBase cbentry=cbc.GetCrunchCompany(company);
            if (cbentry != null)
            {
                int num_cols = 18;
                Object[,] companyrow = new Object[1, num_cols];
                //headerstring if needed
                /*  string[] cpny ={"Company Logo URL","Company Name","Homepage URL","Crunchbase URL",
                                         "Short Description","Category","Number of Employees","City","State",
                                         "Country","Year Founded","Total Funding",,"keyword tags"}; */
                companyrow[0, 0] = cbentry.GetImageURL();
                companyrow[0, 1] = cbentry.name;
                companyrow[0, 2] = cbentry.homepage_url;
                companyrow[0, 3] = cbentry.crunchbase_url;
                companyrow[0, 4] = cbentry.description;
                companyrow[0, 5] = cbentry.category_code;
                companyrow[0, 6] = cbentry.number_of_employees;
                companyrow[0, 7] = cbentry.offices[0].city;
                companyrow[0, 8] = cbentry.offices[0].state_code;
                companyrow[0, 9] = cbentry.offices[0].country_code;
                companyrow[0, 10] = cbentry.founded_year;
                companyrow[0, 11] = cbentry.GetAggregateFunding();
                companyrow[0, 12] = cbentry.tag_list;

                var curr = Globals.ThisAddIn.Application.ActiveSheet;
                var startCell = Globals.ThisAddIn.Application.ActiveCell;
                var endCell = curr.Cells[startCell.Row, startCell.Column + num_cols];
                var writeRange = curr.Range[startCell, endCell];
                writeRange.Value2 = companyrow;
            }
            else // it's not a company
            {
               CrunchFinancial cfc= cbc.GetCrunchFinanceOrg(company);
            }


        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            CrunchBaseConnect cbc = new CrunchBaseConnect();
            List<cbCompanyObject> financeorgs = cbc.FinanceOrgs;
            InsertCompaniesAtCell(financeorgs, "financial-organization", Globals.ThisAddIn.Application.ActiveCell);

        }

        private void SearchButton_Click(object sender, RibbonControlEventArgs e)
        {
            CrunchSearchForm f = new CrunchSearchForm();
            f.Show();

        }
       
        private void CompanyDetailsButton_Click_1(object sender, RibbonControlEventArgs e)
        {
            Range permalinkrange = Globals.ThisAddIn.Application.Selection;
            CrunchBaseConnect cbc = new CrunchBaseConnect();
            foreach (Range c in permalinkrange.Cells)
            {
                if (c != null)
                {
                    CrunchBase company = cbc.GetCrunchCompany(c.Value2);
                    if (company != null)
                    {
                        object[,] detailrow=new object[1,company.aggregateheaders.Count()];
                        for (int i = 0; i < company.aggregaterow.Count(); i++)
                        {
                            detailrow[0, i] = company.aggregaterow[i];
                        }
                        Range startCell = c.Worksheet.Cells[c.Row, c.Column + 1];
                        Range details = ThisAddIn.OutputArrayToExcel(startCell, detailrow);
                    }
                }
            }

        }
        
    }
}
