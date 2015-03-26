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
    public partial class TimeDropDownTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void postbackButton_Click(object sender, EventArgs e)
        {
            Label1.Text = string.Format("hours = {0}, minutes = {1}", TimeDropDown1.Time.Hours, TimeDropDown1.Time.Minutes);
        }
    }
}
