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
using Codentia.Common.WebControls.HtmlElements;

namespace Codentia.Common.WebControls.Test
{
    public partial class LightboxManagerTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestPanel1.Controls.Add(LightboxManager1.GetLightboxLink("TestLink1", "images/lb1.jpg", "test1"));
            TestPanel1.Controls.Add(new Br());
            TestPanel1.Controls.Add(LightboxManager1.GetLightboxLink("TestLink2", "images/lb2.jpg", "test2", "this is a caption"));
            TestPanel1.Controls.Add(new Br());
            TestPanel1.Controls.Add(LightboxManager1.GetLightboxLink("TestLink3", "images/lb3.jpg", "test3", "this is a caption", "grp1"));
        }
    }
}
