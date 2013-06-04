using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using System.Collections;

namespace System.Web.Script.Serialization.CS
{
    public class FundingRoundsConverter : JavaScriptConverter
    {

        public override IEnumerable<Type> SupportedTypes
        {
            //Define the CrunchBaseXLS.InvestmentsFinance as a supported type. 
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(CrunchBaseXLS.FundingRounds) })); }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            CrunchBaseXLS.FundingRounds listType = obj as CrunchBaseXLS.FundingRounds;

            /*if (listType != null)
            {
                // Create the representation.
                Dictionary<string, object> result = new Dictionary<string, object>();
                ArrayList itemsList = new ArrayList();
                
                foreach (ListItem item in listType)
                {
                    //Add each entry to the dictionary.
                    Dictionary<string, object> listDict = new Dictionary<string, object>();
                    listDict.Add("Value", item.Value);
                    listDict.Add("Text", item.Text);
                    itemsList.Add(listDict);
                }
                result["List"] = itemsList;

                return result;
            }*/
            return new Dictionary<string, object>();
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(CrunchBaseXLS.FundingRounds))
            {
                // Create the instance to deserialize into.
                CrunchBaseXLS.FundingRounds invest = new CrunchBaseXLS.FundingRounds();
                // Deserialize the CrunchBaseXLS.FundingRounds's items.
                
                //ArrayList itemsList = (ArrayList)dictionary["List"];
                //for (int i = 0; i < itemsList.Count; i++)
                //    list.Add(serializer.ConvertToType<ListItem>(itemsList[i]));

                //return list;
            }
            return null;
        }

    }
}