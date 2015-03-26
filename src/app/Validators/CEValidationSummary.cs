using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls.Validators
{
    /// <summary>
    /// CEValidationSummary for standard show/hide behaviour
    /// </summary>
    [ToolboxData("<{0}:CEValidationSummary runat=server></{0}:CEValidationSummary>")]
    public class CEValidationSummary : ValidationSummary
    {
        private bool _showOnFirstPostBack = true;

        /// <summary>
        /// Gets or sets a value indicating whether [show on first post back].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show on first post back]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOnFirstPostBack
        {
            get
            {
                return _showOnFirstPostBack;
            }

            set
            {
                _showOnFirstPostBack = value;
            }
        }

        /// <summary>
        /// Create child controls
        /// </summary>
        protected override void CreateChildControls()
        {
            this.ShowSummary = false;

            if (_showOnFirstPostBack)
            {
                if (this.Page.IsPostBack)
                {
                    this.ShowSummary = true;
                }
            }
            else
            {
                if (this.Page.IsPostBack)
                {
                    _showOnFirstPostBack = true;
                }
            }

            base.CreateChildControls();
        }

        /// <summary>
        /// Save the state of this control to page viewstate
        /// </summary>
        /// <returns>The object</returns>
        protected override object SaveViewState()
        {
            object[] state = new object[1];
            state[0] = _showOnFirstPostBack;

            return (object)state;
        }

        /// <summary>
        /// Load the saved state of this control
        /// </summary>
        /// <param name="savedState">state to load</param>
        protected override void LoadViewState(object savedState)
        {
            object[] state = (object[])savedState;

            _showOnFirstPostBack = Convert.ToBoolean(state[0]);
        }
    }
}