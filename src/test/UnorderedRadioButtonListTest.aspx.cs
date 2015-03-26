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

namespace Codentia.Common.WebControls.Test
{
    public partial class UnorderedRadioButtonListTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            URBL.RepeatDirection = RepeatDirection.Horizontal;
            URBL.RepeatLayout = RepeatLayout.Table;

            Dictionary <string, string> dict=new Dictionary<string,string>();

            dict["Choice1"]="Choice1Value";
            dict["Choice2"]="Choice2Value";
            dict["Choice3"]="Choice3Value";


            IEnumerator<string> ie=dict.Keys.GetEnumerator();

            while (ie.MoveNext())
            {

                ListItem li = new ListItem(ie.Current, dict[ie.Current]);
                URBL.Items.Add(li);
            }

            URBL.SelectedIndex=0;           
        }
    }
}
