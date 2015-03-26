using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Codentia.Common.WebControls.HtmlElements;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// UnorderedCheckBoxListSelectable - combines a Label, TextBox and AJAX Calendar Extendar
    /// </summary>
    public class UnorderedCheckBoxListSelectable : CECompositeControl
    {
        private string _caption = string.Empty;
        private string _callTemplate = "javascript: CheckBoxListSelect('{0}', {1}); return false;";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnorderedCheckBoxListSelectable"/> class.
        /// </summary>
        public UnorderedCheckBoxListSelectable()
        {
        }

        /// <summary>
        /// Gets the unordered check box list.
        /// </summary>
        public UnorderedCheckBoxList UnorderedCheckBoxList
        {
            get
            {
                return (UnorderedCheckBoxList)this.FindChildControl("UCBL");
            }
        }

        /// <summary>
        /// Handle control pre-render (before html generation, after all events)
        /// </summary>
        /// <param name="e">Event Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.Page != null)
            {
                string scriptLocation = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Codentia.Common.WebControls.UnorderedCheckBoxListSelectable.js");
                Page.ClientScript.RegisterClientScriptInclude("Codentia.Common.WebControls.UnorderedCheckBoxListSelectable.js", scriptLocation);

                SetAttributes();
            }
        }

        /// <summary>
        /// Create child controls
        /// </summary>
        protected override void CreateChildControls()
        {
            P p = new P();

            HtmlAnchor has = new HtmlAnchor();
            has.InnerText = "Select All";
            has.ID = "sa";
            has.HRef = "#";

            p.Controls.Add(has);

            Label l = new Label();
            l.Text = " | ";
            p.Controls.Add(l);

            HtmlAnchor hau = new HtmlAnchor();
            hau.InnerText = "Unselect All";
            hau.ID = "ua";
            hau.HRef = "#";
            p.Controls.Add(hau);

            this.Controls.Add(p);

            UnorderedCheckBoxList ucbl = new UnorderedCheckBoxList();
            ucbl.ID = "UCBL";
            ucbl.Items.Clear();
            this.Controls.Add(ucbl);
        }

        /// <summary>
        /// Set Attributes (onclick calls)
        /// </summary>
        private void SetAttributes()
        {
            EnsureChildControls();

            HtmlAnchor has = (HtmlAnchor)this.FindChildControl("sa");
            has.Attributes.Add("onclick", string.Format(_callTemplate, ClientID, "true"));

            HtmlAnchor hau = (HtmlAnchor)this.FindChildControl("ua");
            hau.Attributes.Add("onclick", string.Format(_callTemplate, ClientID, "false"));
        }
    }
}