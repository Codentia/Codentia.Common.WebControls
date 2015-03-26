using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl Span
    /// </summary>
    public class Span : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Span"/> class.
        /// </summary>
        public Span()
            : base("span")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Span"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Span(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Span"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public Span(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
