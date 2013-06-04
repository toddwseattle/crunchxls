using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Threading;
namespace CrunchBaseXLS
{
    /// <summary>
    /// Constants contains API_KEY and other global constants
    /// </summary>
    public static class Constants
    {
        public const string API_KEY = "api_key=86v48ptgenmy6b3bagaj3jfx";
        public const int CRUNCH_SLEEP = 100; // Amount of ticks to sleep between crunchbase calls
        public const int HUGEJSON= 22582912;

    }

    public class cpylist : List<cbCompanyObject> { }

    /// <summary>
    /// CrunchBaseConnect connects to Crunchbase to manage the various Json objects
    /// </summary>
    public class CrunchBaseConnect
    {
        // static properties for main tech crunch entities
        private static List<cbCompanyObject> companies=null;
        private static List<cbCompanyObject> financeorgs = null;       // finance orgs is structured just like companies so we reuse alot of stuff.

        public List<cbCompanyObject> Companies
        {
            get
            {
                if (companies != null)
                {
                    return companies;
                }
                else
                {
                    companies=getallcompanies("companies");
                    return companies;

                }
            }
            set { }
        }
        
        public List<cbCompanyObject> FinanceOrgs
        {
            get
            {
                if (financeorgs != null)
                {
                    return financeorgs;
                }
                else
                {
                    financeorgs = getallcompanies("financial-organizations");
                    return financeorgs;

                }
            }
            set { }
        }
        
        public CrunchBaseConnect()
        {
        }
        /// <summary>
        /// GetHeaders returns a String List corresponding to the datatype to use as header rows
        /// </summary>
        /// <param name="headertype">is 'company'</param>
        /// <returns>String List</returns>
        public List<string> GetHeaders(string headertype)
        {
            switch (headertype)
            {
                case "company":
                    string[] cpny ={"Company Logo URL","Company Name","Homepage URL","Crunchbase URL",
                                     "Short Description","Category","Number of Employees","City","State",
                                     "Country","Year Founded","Total Funding","Keyword1","Keyword2","Keyword3",
                                     "Keyword4","Keyword5","Overview"};
                    return cpny.ToList<string>();
                default:
                    return null;
            }

        }
        //public List<cbCompanyObject> GetAllFinanceOrganizations()
        //{
        //    string jsonStream;
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    ser.MaxJsonLength = 12582912; // tons of company data; so maek it bigger just for this read

        //    WebRequest wrGetURL;
        //    //wrGetURL = WebRequest.Create("http://api.crunchbase.com/v/1/search.js" + "?" + "query=venture&entity=financial-organization&"+Constants.API_KEY);  search for the future
        //    wrGetURL = WebRequest.Create("http://api.crunchbase.com/v/1/financial-organizations.js" + "?" + Constants.API_KEY);

        //    jsonStream = new StreamReader(wrGetURL.GetResponse().GetResponseStream()).ReadToEnd();
        //    return jsonStream;
        //}
        private List<cbCompanyObject> getallcompanies(string entityname)
        {
           // CrunchJsonStream cjStream = new CrunchJsonStream();
            CompanyGenerator companyGenerator = new CompanyGenerator();
            JavaScriptSerializer ser = new JavaScriptSerializer();



            Console.Error.WriteLine("Getting ready to get the company names");

            //go hit the crunchbase api to get a list of all the companies in their database
            List<cbCompanyObject> companyNames = companyGenerator.GetCompanyNames(entityname);
            Console.Error.WriteLine("Back from getting the company names");
            return companyNames;
        }

        

        /// <summary>
        /// GetCrunchFinanceOrg retrieves the crunchbase info based on the permalink passed or null if not found.
        /// </summary>
        /// <param name="financeorg">permalink to a firm in the finance-organization namespace on crunchbase</param>
        /// <returns></returns>
        public CrunchFinancial GetCrunchFinanceOrg(string financeorg)
                {
                    CrunchJsonStream cjStream = new CrunchJsonStream();
                    string jsonStream = cjStream.GetJsonStream(financeorg, "financial-organization");
            
                    if (jsonStream != null)
                    {
                        try
                        {
                    
                            JavaScriptSerializer ser = new JavaScriptSerializer();
                            //with the stream, now deserialize into the Crunchbase object
                            CrunchFinancial jsonCrunchBase = ser.Deserialize<CrunchFinancial>(jsonStream);
                            Console.Error.WriteLine("Retrieved info for financial-organization {0}", jsonCrunchBase.name);
                            return jsonCrunchBase;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops, the exception {0} happened with {1}", e.ToString(), financeorg);
                            return null;
                        }
                    }
                    else { return null; } 


                }
        /// <summary>
        /// Gets the info about a company in Crunchbase
        /// </summary>
        /// <param name="crunchcompany">string that matches the shortname in crunchbase, for example as in companies collection name</param>
        /// <returns>Data in a CrunchBase class structure</returns>
        public CrunchBase GetCrunchCompany(string crunchcompany)
        {
            CrunchJsonStream cjStream = new CrunchJsonStream();
            string jsonStream;
            jsonStream = cjStream.GetJsonStream(crunchcompany,"company");
            if (jsonStream != null)
            {
                try
                {
                    string jsonLine;
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    //with the stream, now deserialize into the Crunchbase object
                    CrunchBase jsonCrunchBase = ser.Deserialize<CrunchBase>(jsonStream);

                    //assuming that worked, we need to clean up and create some additional meta data
                    jsonCrunchBase.FixCrunchBaseURL();
                    jsonCrunchBase.AggregateFunding();
                    jsonCrunchBase.SplitTagString();

                    //and now we build the CSV string and write to file
                    jsonLine = "\t" + jsonCrunchBase.GetImageURL() + "\t" +
                        jsonCrunchBase.name + "\t" +
                        jsonCrunchBase.homepage_url + "\t" +
                        jsonCrunchBase.crunchbase_url + "\t" +
                        jsonCrunchBase.description + "\t" +
                        jsonCrunchBase.category_code + "\t" +
                        jsonCrunchBase.number_of_employees + "\t" +
                        jsonCrunchBase.hqcity+ "\t" +
                        jsonCrunchBase.hqstate + "\t" +
                        jsonCrunchBase.hqcountry + "\t" +
                        jsonCrunchBase.founded_year + "\t" +
                        jsonCrunchBase.GetAggregateFunding().ToString() + "\t" +
                        jsonCrunchBase.GetKeywordList(); /* + "\t\"" +  //there are max of 5 keywords I will dump out
                            jsonCrunchBase.overview + "\"";*/
                    //I really wanted to put the "overview" field into the file, but the XLS import was
                    //blowing up on the HTML...will need to revisit and fix
                    // tw note:  see http://www.blackbeltcoder.com/Articles/strings/convert-html-to-text

                    jsonCrunchBase.tabdelimited = jsonLine;
                    Console.Error.WriteLine("Retrieved info for {0}", jsonCrunchBase.name);
                    return jsonCrunchBase;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Oops, the exception {0} happened with {1}", e.ToString(), crunchcompany);
                    return null;
                }
            }
            else { return null; }  //if
        } //member get crunch 
        public class CompanyJsonObject
        {
            public List<cbCompanyObject> cbcompanyObj;
        }

        public List<cbSearchResults> SearchResults(string querystring)
        {
            CrunchJsonStream sr = new CrunchJsonStream();
            string jsonStream = sr.GetJsonSearch(querystring);

            if (jsonStream != null)
            {
                try
                {
                    DiagnosticTimer.StartTimer("firstpage");
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    //with the stream, now deserialize into the Crunchbase object
                    ResultsPage Results = ser.Deserialize<ResultsPage>(jsonStream);
                    DiagnosticTimer.StopTimer("firstpage");
                    // we got the first page create a big list of all the pages 
                    if (Results.total < 11)
                    { // no extra pages
                        return Results.results;
                    }
                    else //get the other pages
                    {
                        bool ShowProgress = Results.total>60 ? true : false;
                        SearchingCrunchBaseProgressForm progressdialog = new SearchingCrunchBaseProgressForm();
                        if (ShowProgress) // put up the dialog
                        {
                            progressdialog.TotalFound = Results.total;
                            progressdialog.Retireved = 10;
                            progressdialog.EstimatedTime = new TimeSpan(DiagnosticTimer.GetTimerTimeSpan("firstpage").Ticks*(Results.total));
                            progressdialog.Show();
                            
                        }
                        
                        List<cbSearchResults> allresults = Results.results;
                        for (int i = 2; i < (Results.total / 10) +1; i++)
                        {
                            jsonStream = sr.GetJsonSearch(querystring + "&page=" + i.ToString());
                            if (jsonStream!=null)
                            {

                                try
                                {
                                    ser = new JavaScriptSerializer();
                                    //with the stream, now deserialize into the Crunchbase object
                                    Results = ser.Deserialize<ResultsPage>(jsonStream);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Oops, the exception {0} happened with {1}", e.ToString(), querystring);
                                    return null;
                                }
                                allresults.AddRange(Results.results);
                                if (ShowProgress)
                                {
                                    progressdialog.Retireved = i * 10;
                                    progressdialog.EstimatedTime = new TimeSpan(DiagnosticTimer.GetTimerTimeSpan("firstpage").Ticks * (Results.total-i));
                                    progressdialog.Update();
                                    if (progressdialog.Canceled)
                                    {
                                        progressdialog.Close();
                                        return allresults;
                                    }
                                }
                            }
                        }
                        if (ShowProgress)
                        {
                            progressdialog.Close();
                        }
                        return allresults;
                    }
                }
                 catch (Exception e)
                        {
                            Console.WriteLine("Oops, the exception {0} happened with {1}", e.ToString(), querystring);
                            return null;
                        }
                    }
                    else { return null; } 

            }
    } // class crunchbaseconnect
    //----------------------------------------------------------------------------------------------
    // the following are classes copied from the crunchbase grabber project on VS code public depot
    // the first two from program.cs
    //
    /// <summary>
    /// cbCompanyObject is the simple company description used to retrieve more detailed company info
    /// </summary>
    public class cbCompanyObject
    {
        public string name;
        public string permalink;
    }



    public class cbSearchResults
    {
        public string name { get; set; }
        public string permalink { get; set; }
        public string Namespace { get; set; }
        public string overview { get; set; }
        public imgStructure image;
    }

    public class SearchResultsList : List<cbSearchResults> { }

    public class ResultsPage
    {
        public int total { get; set; }
        public int page { get; set; }
        public List<cbSearchResults> results;
    }
    
    public class CompanyGenerator
    {
        //this is how we call out to crunchbase to get their full list of companies for an entity like 'companies' or 'financial-organizations'
        // see http://developer.crunchbase.com/docs
        public List<cbCompanyObject> GetCompanyNames(string entity)
        {
            string jsonStream;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = Constants.HUGEJSON; // tons of company data; so maek it bigger just for this read

            WebRequest wrGetURL;
            wrGetURL = WebRequest.Create("http://api.crunchbase.com/v/1/"+entity+".js" + "?" + Constants.API_KEY);

            jsonStream = new StreamReader(wrGetURL.GetResponse().GetResponseStream()).ReadToEnd();
            List<cbCompanyObject> jsonCompanies = null; // don't have any yet!
            //as opposed to the single company calls, this returns a list of companies, so we have to
            //stick it into a list
            if (jsonStream.Length < ser.MaxJsonLength)  // if crunchbase returns a gi-normous string punt
            {
                jsonCompanies = ser.Deserialize<List<cbCompanyObject>>(jsonStream);

            }
            else
            {
                Console.Error.WriteLine("Company Data exceeds Json Max.   max is {0:0,0} and data is {1:0,0}", ser.MaxJsonLength, jsonStream.Length);

            }



            return jsonCompanies ;
        }
    }
    //
    // the following are derrived from crunchbase.cs inthe public grabber project
    //
    /// <summary>
    /// CrunchFinancial describes the json for an entity in the financial-organization namespace of www.crunchbase.com 
    /// </summary>
    /// created by tw 5/16/2013
    public class CrunchFinancial
    {
        public string name{get; set;}
        public string permalink{get; set;}
        public string crunchbase_url{get; set;}
        public string homepage_url{get; set;}
        public string blog_url{get; set;}
        public string blog_feed_url{get; set;}
        public string twitter_username{get; set;}
        public string phone_number{get; set;}
        public string description{get; set;}
        public string email_address{get; set;}
        public int? number_of_employees{get; set;}
        public int? founded_year{get; set;}
        public int? founded_month{get; set;}
        public int? founded_day{get; set;}
        public string tag_list{get; set;}
        public string alias_list{get; set;}
        public string created_at{get; set;}
        public string updated_at{get; set;}
        public string overview{get; set;}
        public imgStructure image{get; set;}
        public List<locStructure> offices;
        public List<RelationshipFinance> relationships;
        public List<FundingRounds> investments;
        public List<MilestonesFinance> milestones;
        public List<ProvidershipsFinance> providerships;
        public List<FundsFinance> funds;
        public List<FinanceVideoEmbed> video_embeds;
        public List<FinanceExternalLinks> external_links;
        public string[] headersaggregate
        {
            get {
                string[]headerlist=new string[14];
                headerlist[0] = "Name";
                headerlist[1]="Crunchbase URL";
                headerlist[2] = "Homepage URL";
                headerlist[3] = "Twitter Handle";
                headerlist[4] = "Founded Date";
                headerlist[5] = "Tags";
                headerlist[6] = "HQ City";
                headerlist[7] = "HQ State";
                headerlist[8] = "HQ Country";
                headerlist[9] = "total invested";
                headerlist[10]= "# of funds";
                headerlist[11] = "Created Date";
                headerlist[12] = "Update Date";
                headerlist[13] = "Description";
                return headerlist;

            }
            set { }
        }
        public Object[] aggregateitems
        {
            get
            {
                Object[] aggregate = new Object[14];
                aggregate[0] = this.name; // "Name";
                aggregate[1] = this.crunchbase_url; // "Crunchbase URL";
                aggregate[2] = this.homepage_url; // "Homepage URL";
                aggregate[3] = this.twitter_username; // "Twitter Handle";
                if (founded_year.HasValue)
                {
                    aggregate[4] = new DateTime(founded_year.Value, founded_month.HasValue ? founded_month.Value : 1, founded_day.HasValue ? founded_day.Value: 1); // founded
                }
                else aggregate[4] = "Unknown";
                aggregate[5] = tag_list;//"Tags";
                if (offices.Count > 0)
                {
                    aggregate[6] = offices[0].city; // "HQ City";
                    aggregate[7] = offices[0].state_code; // "HQ State";
                    aggregate[8] = offices[0].country_code; // "HQ Country";
                }
                else
                {
                    aggregate[6] = "";
                    aggregate[7] = "";
                    aggregate[8] = "";
                }
                // total investments
                double total_invest = 0;
                //List<string> invested_companies=new List<string>;
                foreach (FundingRounds f in investments)
                {
                    total_invest += f.funding_round.raised_amount.HasValue ? f.funding_round.raised_amount.Value: 0;
                    // ** I haven't really figured this out yet
                    //if(!invested_companies.Contains(f.funding_round.company.))
                    //{ }
                }
                aggregate[9] = total_invest;
                aggregate[10] = funds.Count; // "# of funds";
                aggregate[11] = created_at; //"Created Date as a string";
                aggregate[12] = updated_at; // "Upadate Date" as a string
                aggregate[13] = description; 
                return aggregate;
            }
            set { }
        }
        
    }
    public class CrunchBase
    {
        //this is the class definition to enum all of the data items to be retrieved from the
        //deserialized JSON object

        public string name;
        public string permaLink;
        public string homepage_url;
        public string crunchbase_url;
        public string blog_url;
        public string blog_feed_url;
        public string twitter_username;
        public string category_code;
        public string description; // = "";
        public int? number_of_employees; // = 0;
        public string overview;
        public bool deadpool;
        public int? deadpool_year; //= "";
        public int? deadpool_month;
        public int? deadpool_day;
        public string deadpooled_url;
        public string alias_list;
        public string email_address;
        public string phone_number;
        public imgStructure image;
        public List<locStructure> offices;
        public string tag_list;
        public int? founded_year;
        public int? founded_month;
        public int? founded_day;

        public List<fndStructure> funding_rounds;
        public Dictionary<string, fndStructure> aggFundStructure = new Dictionary<string, fndStructure>();
        public List<string> keyword_tags;
        public List<CrunchProduct> products;
        public List<RelationshipFinance> relationships;
        // tw add property to contain a tab delimited version of the stuff
        public string tabdelimited;

        
        // tw get aggregate headers
        public string[] aggregateheaders {
            get{
                return new string[]{
                    "Name", "Crunchbase URL","Homepage URL","Twitter Handle","Founded Date","Tags","HQ City","HQ State","HQ Country","total invested","# employees","catagory code"};
                   }
            set { }
        }

        public object[] aggregaterow
        {
            get
            {
                
                // "Name", "Crunchbase URL","Homepage URL","Twitter Handle","Founded Date","Tags","HQ City","HQ State","HQ Country","total invested","# employees","catagory code"
                return new object[] {
                    name, crunchbase_url,homepage_url,twitter_username,founded_year, tag_list, hqcity,hqstate,hqcountry, this.GetAggregateFunding(),number_of_employees,category_code};
            }

            set { }
        }

        //helper function to generate the URL for the JSON object on a per company basis
        public void FixCrunchBaseURL()
        {
            string urlBase = "http://www.crunchbase.com/company/";

            crunchbase_url = urlBase + this.permaLink;
        }

        //helper function to generate the URL for the image to be pulled from the crunchbase API
        public string GetImageURL()
        {
            string baseURL = "http://www.crunchbase.com/";
            if (this.image != null)
            {
                return baseURL + this.image.available_sizes[1][1].ToString();
            }
            else
            {
                return "none";
            }
        }

        //simple function which pulls all the funding data for each company and aggregates the funding across
        //all rounds - the data is dumped into a dictionary that has all funding for each round
        public void AggregateFunding()
        {
            foreach (fndStructure fundingRound in this.funding_rounds)
            {
                if (this.aggFundStructure.ContainsKey(fundingRound.round_code))
                {
                    if (fundingRound.raised_amount == null)
                    {
                        fundingRound.raised_amount = 0;
                    }

                    this.aggFundStructure[fundingRound.round_code].raised_amount += fundingRound.raised_amount;


                    if (this.aggFundStructure[fundingRound.round_code].funded_year < fundingRound.funded_year)
                    {
                        this.aggFundStructure[fundingRound.round_code].funded_year = fundingRound.funded_year;
                    }
                }
                else
                {
                    if (fundingRound.raised_amount == null)
                    {
                        fundingRound.raised_amount = 0;
                    }

                    if (fundingRound.funded_year == null)
                    {
                        //use -1 to denote that the year returned was null
                        fundingRound.funded_year = -1;
                    }

                    this.aggFundStructure.Add(fundingRound.round_code, fundingRound);
                }
            }
        }

        //takes the dictionary and iterates through it to get the total funding.  Using the dictionary allows
        //for a pivot on funding round later should I choose
        public double GetAggregateFunding()
        {
            double totalFunding = 0;
            if (funding_rounds != null)
            {
                foreach (fndStructure fundingRound in this.funding_rounds)
                {
                    totalFunding += (double)fundingRound.raised_amount;
                }
            }
            return totalFunding;
        }

        //helper function to parse the keyword-tag string
        public void SplitTagString()
        {
            if (tag_list != null)
            {
                string[] keywordTags = tag_list.Split(',');

                foreach (string tag in keywordTags)
                {
                    //make sure those pesky trailing and leading spaces are gone
                    tag.Trim();
                }

                this.keyword_tags = keywordTags.ToList();
            }
        }

        //the number of keywords can be [0,n), which makes for challenges with the Pivot data creation.  To simplify
        //this, I simply take 5 keywords if there are 5, and if not, return \t in place of a word
        //the special case is a null, and to avoid a bug in the Pivot CXML, return "none" for the first word, and 4x\t
        public string GetKeywordList()
        {
            return GetKeywordList("\t");
        }
        public string GetKeywordList(string delimit)
        {
            if (delimit=="")
            {
                delimit = ";";
            }
            string tmpString = "";
            if (keyword_tags != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i < keyword_tags.Count())
                    {
                        tmpString += keyword_tags[i].ToString() + delimit;
                    }
                    else
                    {
                        tmpString += " " + delimit;
                    }
                }
            }
            else
            {
                tmpString = "none\t\t\t\t\t";
            }

            return tmpString;
        }

        public CrunchBase()
        {
        }

        public string hqcity
        {
            get
            {
                if (offices == null) { return ""; }
            else {
                return offices[0].city;
            }
            }
            set { }
        }

        public string hqstate 
        {
            get
            {
                if (offices == null) { return ""; }
            else {
                return offices[0].state_code;
            }
            }
            set { }
        }

        public string hqcountry
        {
            get
            {
                if (offices == null) { return ""; }
                else
                {
                    return offices[0].country_code;
                }
            }
            set { }
        }
    }

    // helper json functions for substructures
    // tw 5-16-2013
      public class CrunchProduct
    {
        string name;
        string permalink;
        List<imgStructure> image;
    }
    public class ProvidershipsFinance
    {
        public string title;
        public bool is_past;
        public FinanceProvider provider;
    }

    public class FinanceProvider
    {
        public string name{get; set;}
        public string permalink{get; set;}
        public List<imgStructure> image;
    }

    public class FundsFinance
    {
        public string name{ get; set;}
        public int? funded_year{ get; set;}
        public int? funded_month{ get; set;}
        public int? funded_day{ get; set;}
        public double? raised_amount{ get; set;}
        public string raised_currency_code{ get; set;}
        public string source_url { get; set; }
        public string source_description { get; set; }
    }

    public class FinanceVideoEmbed
    {
        public string embed_code;
        public string description;
    }
    public class FinanceExternalLinks
    {
        public List<Object> stuff;
    }
public class MilestonesFinance
{
    public string description{get; set;}
    public int? stoned_year{get; set;}
    public int? stoned_month{get; set;}
    public int? stoned_day{get; set;}
    public string source_url{get; set;}
    public string source_text{get; set;}
    public string source_description{get; set;}
    public string stoneable_type{get; set;}
    public int? stoned_value{get; set;}
    public string stoned_value_type{get; set;}
    public string stoned_acquirer{get; set;}
    public List<FinanceStonable> stonable;
}

public class FinanceStonable {
    public string name{get; set;}
    public string permalink{get; set;}
 }


  public class PersonFinance // as in the json in a financial-organization
	{
      public string first_name{get; set;}
      public string last_name{get; set;}
      public string permalink{get; set;}
      public imgStructure image;
		
	}

    public class RelationshipFinance // as in the json in a financial-org
    {
        public bool is_past{get; set;}
        public string title{get; set;}
        public PersonFinance person;
    }
    public class FundingRounds
    {
        public InvestmentsFinance funding_round;
    }
    public class InvestmentsFinance // as in the json in a financial-org
    {
        public string round_code{get; set;}
        public string source_url{get; set;}
        public string source_description{get; set;}
        public double? raised_amount{get; set;}
        public string raised_currency_code{get; set;}
        public int? funded_year{get; set;}
        public int? funded_month{get; set;}
        public int? funded_day{get; set;}
        public List<InvestCompany> company;
    }
    public class InvestCompany //part of json of investmentsfinanc in a financial-org
    {
        public string name{get; set;}
        public string permalink{get; set;}
        public imgStructure image;
    }
    //class for the JSON data about logos
    public class imgStructure
    {
        public List<List<object>> available_sizes;
        public string attribution;
    }

    //class for the JSON data about funding
    public class fndStructure
    {
        public string round_code;
        public double? raised_amount;
        public int? funded_year;
    }

    //class for the JSON data about the company office location
    public class locStructure
    {
        public string city;
        public string state_code;
        public string country_code;
    }

    //class to create the JSON stream, fetch it
    public class CrunchJsonStream
    {
        public string cbJsonStream;

        public string GetJsonSearch(string searchstring)
        {
            string jsonStream;
            string apiUrlBase = "http://api.crunchbase.com/v/1/search.js?";
            string urlEnd = "&"+Constants.API_KEY;

            WebRequest wrGetURL;
            wrGetURL = WebRequest.Create(apiUrlBase + searchstring + urlEnd);

            try
            {
                jsonStream = new StreamReader(wrGetURL.GetResponse().GetResponseStream()).ReadToEnd();
                return jsonStream;
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("Bad Query: {0} - URL bad except {1}", searchstring, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Caught: {0}", e.ToString());
            }

            //Console.WriteLine("Done getting JSON");
            return jsonStream = null;
        }

        public string GetJsonStream(string companyName,string entity)
        {
            string jsonStream;
            string apiUrlBase = "http://api.crunchbase.com/v/1/"+entity+"/";
            string urlEnd = ".js" + "?" + Constants.API_KEY;

            WebRequest wrGetURL;
            wrGetURL = WebRequest.Create(apiUrlBase + companyName + urlEnd);

            try
            {
                jsonStream = new StreamReader(wrGetURL.GetResponse().GetResponseStream()).ReadToEnd();
                return jsonStream;
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("Company: {0} - URL bad except {1}", companyName,e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Generic Exception Caught: {0}", e.ToString());
            }

            //Console.WriteLine("Done getting JSON");
            return jsonStream = null;
        }
    }
} //namespace
