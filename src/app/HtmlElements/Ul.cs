namespace Codentia.Common.WebControls.HtmlElements
{
    /// <summary>
    /// Basic control encapsulating an HtmlGenericControl unordered list
    /// </summary>
    public class Ul : CEHtmlGenericControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ul"/> class.
        /// </summary>
        public Ul()
            : base("ul")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ul"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public Ul(string id)
            : this()
        {
            this.ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ul"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cssClass">The CSS class.</param>
        public Ul(string id, string cssClass)
            : this(id)
        {
            this.Attributes.Add("class", cssClass);
        }
    }
}
