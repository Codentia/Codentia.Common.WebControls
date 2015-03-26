using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl Div
    /// </summary>
    public class H3 : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="H3"/> class.
        /// </summary>
        public H3()
            : base("h3")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H3"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public H3(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H3"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public H3(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
