using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.Test
{
    public partial class SelectionListTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void selectionList2_selectedIndexChanged(SelectionList sender, int[] indexes)
        {
            StringBuilder msg = new StringBuilder();

            msg.Append("Selection: ");

            for (int i = 0; i < indexes.Length; i++)
            {
                msg.AppendFormat("{0}{1} ({2})", (i > 0 ? "," : string.Empty), indexes[i], sender.Items[indexes[i]].Description);
            }

            messageLabel2.Text = msg.ToString();
        }

        protected void selectionList2_selectionCancelled(SelectionList sender, int[] indexes)
        {
            messageLabel2.Text = "Selection was cancelled.";
        }
    }
}
