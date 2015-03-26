using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.Test
{
    public partial class IdempotentLinkButtonTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void idempotentLink_Click(object sender, EventArgs e)
        {
            // sleep for 5s so we can test idempotency
            Thread.Sleep(5000);
        }

        protected void idempotentLink_WithValidation_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                idempotentLink_Click(sender, e);
            }
        }
    }
}
