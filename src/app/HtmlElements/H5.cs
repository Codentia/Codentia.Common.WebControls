using System;
using System.Web.UI.HtmlControls;

namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl Div
    /// </summary>
    public class H5 : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="H5"/> class.
        /// </summary>
        public H5()
            : base("h5")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H5"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public H5(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="H5"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public H5(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
