using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// This class represents an Idempotent override of the asp.net LinkButton
    /// </summary>
    [ToolboxData("<{0}:IdempotentLinkButton runat=server></{0}:IdempotentLinkButton>")]
    public class IdempotentLinkButton : LinkButton
    {
        private string _onClientClick = string.Empty;

        /// <summary>
        /// Gets or sets the Javascript which should be run client-side when this link is clicked
        /// </summary>
        public override string OnClientClick
        {
            get
            {
                return base.OnClientClick;
            }

            set
            {
                _onClientClick = value;
            }
        }
        
        /// <summary>
        /// Handle control pre-render (before html generation, after all events)
        /// </summary>
        /// <param name="e">Event Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.Page != null)
            {
                this.Page.ClientScript.RegisterClientScriptResource(typeof(IdempotentButton), "Codentia.Common.WebControls.Idempotency.js");
                base.OnClientClick = "ExecuteJavascriptWithIdempotency(this, \"" + _onClientClick + (!string.IsNullOrEmpty(_onClientClick) && !_onClientClick.EndsWith(";") ? ";" : string.Empty) + this.Page.ClientScript.GetPostBackEventReference(this, this.ID) + "\");";
            }

            base.OnPreRender(e);
        }
    }
}
