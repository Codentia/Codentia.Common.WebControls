using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl Div
    /// </summary>
    public class P : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="P"/> class.
        /// </summary>
        public P()
            : base("p")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="P"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public P(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="P"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public P(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
