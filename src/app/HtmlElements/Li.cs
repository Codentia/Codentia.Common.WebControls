using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl listitem
    /// </summary>
    public class Li : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Li"/> class.
        /// </summary>
        public Li()
            : base("li")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Li"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Li(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Li"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public Li(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
