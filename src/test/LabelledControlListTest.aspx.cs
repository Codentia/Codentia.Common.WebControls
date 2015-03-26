using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.Test
{
    public partial class LabelledControlListTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox tx1 = new TextBox();
            tx1.ID = "tx1";
            LabelledControlList1.Add("TB1", tx1);

            TextBox tx2 = new TextBox();
            tx2.ID = "tx2";
            LabelledControlList1.Add("TB2", tx2);
            
            TextBox tx3 = new TextBox();
            tx3.ID = "tx3";
            LabelledControlList1.Add("TB3", tx3);

            LabelledControlList1.UpdateChildControls();


            TextBox btx1 = new TextBox();
            btx1.ID = "btx1";
            LabelledControlList2.Add("TB1", btx1);

            TextBox btx2 = new TextBox();
            btx2.ID = "btx2";
            LabelledControlList2.Add("TB2", btx2);

            TextBox btx3 = new TextBox();
            btx3.ID = "btx3";
            LabelledControlList2.Add("TB3", btx3);

            LabelledControlList2.UpdateChildControls();
        }
    }
}
