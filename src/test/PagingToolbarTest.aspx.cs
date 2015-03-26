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
    public partial class PagingToolbarTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // initialise controls on first load only
            if (!IsPostBack)
            {
                PagingToolbar1.TotalPages = Convert.ToInt32(totalPages.Text);
                PagingToolbar1.TotalItems = Convert.ToInt32(totalItems.Text);

                PagingToolbar2.TotalPages = Convert.ToInt32(totalPages.Text);
                PagingToolbar2.TotalItems = Convert.ToInt32(totalItems.Text);
            }
        }

        protected void updateControl_Clicked(object sender, EventArgs e)
        {
            int options = 0;
            
            if (HideFirstPageControl.Checked)
            {
                options = options | (int)PagingToolbarOption.HideFirstPageControl;
            }
            
            if (HidePreviousPageControl.Checked)
            {
                options = options | (int)PagingToolbarOption.HidePreviousPageControl;
            }
            
            if (HideNextPageControl.Checked)
            {
                options = options | (int)PagingToolbarOption.HideNextPageControl;
            }
            
            if (HideLastPageControl.Checked)
            {
                options = options | (int)PagingToolbarOption.HideLastPageControl;
            }
            
            if (IsItemBased.Checked)
            {
                options = options | (int)PagingToolbarOption.IsItemBased;
            }

            if (HidePageNumberLabel.Checked)
            {
                options = options | (int)PagingToolbarOption.HidePageNumberLabel;
            }
            
            if (UseImages.Checked)
            {
                options = options | (int)PagingToolbarOption.UseImages;
            }

            PagingToolbar1.Options = options;
            PagingToolbar2.Options = options;

            PagingToolbar1.TotalPages = Convert.ToInt32(totalPages.Text);
            PagingToolbar1.TotalItems = Convert.ToInt32(totalItems.Text);

            PagingToolbar2.TotalPages = Convert.ToInt32(totalPages.Text);
            PagingToolbar2.TotalItems = Convert.ToInt32(totalItems.Text);
        }
    }
}
