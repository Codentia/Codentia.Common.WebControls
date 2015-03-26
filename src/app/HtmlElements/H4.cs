using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl Div
    /// </summary>
    public class H4 : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="H4"/> class.
        /// </summary>
        public H4()
            : base("h4")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H4"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public H4(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H4"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public H4(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
