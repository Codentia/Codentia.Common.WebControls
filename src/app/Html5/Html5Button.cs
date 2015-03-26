using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls.Html5
{
    /// <summary>
    /// Derivative WebControl Button which generates html5 markup
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class Html5Button : Button
    {
        /// <summary>
        /// Gets or sets the text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button"/> control.
        /// </summary>
        /// <returns>The text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button"/> control. The default value is <see cref="F:System.String.Empty"/>.</returns>
        public new string Text
        {
            get
            {
                return ViewState["NewText"] as string;
            }

            set
            {
                ViewState["NewText"] = HttpUtility.HtmlDecode(value);
            }
        }

        /// <summary>
        /// Gets the name of the control tag. This property is used primarily by control developers.
        /// </summary>
        /// <returns>The name of the control tag.</returns>
        protected override string TagName
        {
            get
            {
                return "button";
            }
        }

        /// <summary>
        /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> value that corresponds to this Web server control. This property is used primarily by control developers.
        /// </summary>
        /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag"/> enumeration values.</returns>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Button;
            }
        }

        /// <summary>
        /// Determines whether the button has been clicked prior to rendering on the client.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            LiteralControl lc = new LiteralControl(this.Text);
            Controls.Add(lc);

            base.Text = UniqueID;
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> object that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            RenderChildren(writer);
        }
    }
}
