using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.Test
{
    public partial class UnorderedCheckBoxListTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict["Choice1"] = "Choice1Value";
            dict["Choice2"] = "Choice2Value";
            dict["Choice3"] = "Choice3Value";


            IEnumerator<string> ie = dict.Keys.GetEnumerator();

            while (ie.MoveNext())
            {

                ListItem li = new ListItem(ie.Current, dict[ie.Current]);
                UCBL.Items.Add(li);
            }

            UCBL.SelectedIndex = 0;

            Dictionary<string, string> dict2 = new Dictionary<string, string>();

            dict2["Choice4"] = "Choice4Value";
            dict2["Choice5"] = "Choice5Value";
            dict2["Choice6"] = "Choice6Value";

            IEnumerator<string> ie2 = dict2.Keys.GetEnumerator();
            while (ie2.MoveNext())
            {

                ListItem li = new ListItem(ie2.Current, dict2[ie2.Current]);
                UCBL2.UnorderedCheckBoxList.Items.Add(li);
            }

            UCBL2.UnorderedCheckBoxList.SelectedIndex = 0;

            Dictionary<string, string> dict3 = new Dictionary<string, string>();

            dict3["Choice4"] = "Choice4Value";
            dict3["Choice5"] = "Choice5Value";
            dict3["Choice6"] = "Choice6Value";

            IEnumerator<string> ie3 = dict3.Keys.GetEnumerator();
            while (ie3.MoveNext())
            {

                ListItem li = new ListItem(ie3.Current, dict3[ie3.Current]);
                UCBL3.UnorderedCheckBoxList.Items.Add(li);
            }

            UCBL3.UnorderedCheckBoxList.SelectedIndex = 0;
        }
    }
}
