using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl Div
    /// </summary>
    public class CEHtmlGenericControl : HtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CEHtmlGenericControl"/> class.
        /// </summary>
        public CEHtmlGenericControl()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CEHtmlGenericControl"/> class.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public CEHtmlGenericControl(string tagName)
            : base(tagName)
        {
        }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass
        {
            get
            {
                return this.Attributes["class"];
            }

            set
            {
                this.Attributes.Add("class", value);
            }
        }
    }
}
