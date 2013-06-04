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
        public const integer CRUNCH_SLEEP = 100; // Amount of ticks to sleep between crunchbase calls

    }
  
    /// <summary>
    /// CrunchBaseConnect connects to Crunchbase to manage the various Json objects
    /// </summary>
    public class CrunchBaseConnect
    {
        private companies List<cbComapanyObject>();

        public List<cbCompanyObject> Companies {
            get {
                if (companies != null)
                {
                    return companies;
                }
                else
                {
                    return getallcompanies();
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
                    return List<string>(cpny);
                    break;
                default:
                    return null;
                    break;
            }

        }
        private List<cbCompanyObject> getallcompanies()
        {
            CrunchJsonStream cjStream = new CrunchJsonStream();
            CompanyGenerator companyGenerator = new CompanyGenerator();
            JavaScriptSerializer ser = new JavaScriptSerializer();

            string jsonStream;


            Console.Error.WriteLine("Getting ready to get the company names");

            //go hit the crunchbase api to get a list of all the companies in their database
            List<cbCompanyObject> companyNames = companyGenerator.GetCompanyNames();
            Console.Error.WriteLine("Back from getting the company names");
        }
        /// <summary>
        /// Gets the info about a company in Crunchbase
        /// </summary>
        /// <param name="crunchcompany">string that matches the shortname in crunchbase, for example as in companies collection name</param>
        /// <returns>Data in a CrunchBase class structure</returns>
        public CrunchBase GetCrunchCompany(string crunchcompany) 
        {
            string jsonStream;
            jsonStream = cjStream.GetJsonStream(company.name);
                 if (jsonStream != null)
                {
                    try
                    {
                        string jsonline;
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
                            jsonCrunchBase.offices[0].city + "\t" +
                            jsonCrunchBase.offices[0].state_code + "\t" +
                            jsonCrunchBase.offices[0].country_code + "\t" +
                            jsonCrunchBase.founded_year + "\t" +
                            jsonCrunchBase.GetAggregateFunding().ToString() + "\t" +
                            jsonCrunchBase.GetKeywordList(); /* + "\t\"" +  //there are max of 5 keywords I will dump out
                            jsonCrunchBase.overview + "\"";*/
                            //I really wanted to put the "overview" field into the file, but the XLS import was
                            //blowing up on the HTML...will need to revisit and fix
                        // tw note:  see http://www.blackbeltcoder.com/Articles/strings/convert-html-to-text

                        jsonCrunchBase.tabdelimited=jsonline;
                        Console.Error.WriteLine("Retrieved info for {0}", jsonCrunchBase.name);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oops, the exception {0} happened with {1}",e.ToString(), company.name);
                    }
                 }  //
        } //member get crunch 
        public class CompanyJsonObject
        {
            public List<cbCompanyObject> cbcompanyObj;
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

    public class CompanyGenerator
    {
        //this is how we call out to crunchbase to get their full list of companies
        public List<cbCompanyObject> GetCompanyNames()
        {
            string jsonStream;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = 12582912; // tons of company data; so maek it bigger just for this read

            WebRequest wrGetURL;
            wrGetURL = WebRequest.Create("http://api.crunchbase.com/v/1/companies.js" + "?" + Constants.API_KEY);

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



            return jsonCompanies;
        }
    }
    //
    // the following are derrived from crunchbase.cs inthe public grabber project
    //
    public class CrunchBase
    {
        //this is the class definition to enum all of the data items to be retrieved from the
        //deserialized JSON object

        public string name;
        public string permaLink;
        public string homepage_url;
        public string crunchbase_url;
        public string category_code;
        public string description; // = "";
        public int? number_of_employees; // = 0;
        public string overview;
        public bool deadpool;
        public int? deadpool_year; //= "";
        public imgStructure image;
        public List<locStructure> offices;
        public string tag_list;
        public int? founded_year;
        public List<fndStructure> funding_rounds;
        public Dictionary<string, fndStructure> aggFundStructure = new Dictionary<string, fndStructure>();
        public List<string> keyword_tags;
        // tw add property to contain a tab delimited version of the stuff
        public string tabdelimited;

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

            foreach (fndStructure fundingRound in this.funding_rounds)
            {
                totalFunding += (double)fundingRound.raised_amount;
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
            string tmpString = "";
            if (keyword_tags != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i < keyword_tags.Count())
                    {
                        tmpString += keyword_tags[i].ToString() + "\t";
                    }
                    else
                    {
                        tmpString += " " + "\t";
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



        public string GetJsonStream(string companyName)
        {
            string jsonStream;
            string apiUrlBase = "http://api.crunchbase.com/v/1/company/";
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
                Console.WriteLine("Company: {0} - URL bad", companyName);
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
