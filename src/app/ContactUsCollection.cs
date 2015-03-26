using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.Helper;
using Codentia.Common.Logging;
using Codentia.Common.Logging.BL;
using Codentia.Common.Net;
using Codentia.Common.WebControls.HtmlElements;
using Codentia.Common.WebControls.Validators;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// ContactUsCollection class
    /// </summary>
    public class ContactUsCollection : List<ContactUsField>
    {
    }
}