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
    public partial class CEValidationSummaryTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RequiredFieldValidator1.IsValid = false;
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Label1.Text = "Valid";
            }
        }
    }
}
