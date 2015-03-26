using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl ordered list
    /// </summary>
    public class Ol : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ol"/> class.
        /// </summary>
        public Ol()
            : base("ol")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ol"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Ol(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ol"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public Ol(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
