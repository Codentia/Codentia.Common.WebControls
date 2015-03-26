using System;
using System.Web;
using Codentia.Common.Logging;
using Codentia.Common.Logging.BL;
using Codentia.Common.WebControls.HtmlElements;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// BrowserManager Control - responsible for logging HttpRequests and issuing notifications when a browser is potentially incompatible with the
    /// site.
    /// </summary>
    public class BrowserManager : CECompositeControl
    {
        private bool _warnIfIE6 = false;
        private bool _applyIE6PngFix = true;

        private string _ie6WarningMessage = "You are using an old web browser (IE6 or older) and may experience issues with this website. Click here for more information &raquo;";
        private string _unitClearGifPath = string.Empty;
        private string _ie6WarningLink = "http://www.mattchedit.com/Blog.aspx?id=ie6-whats-the-deal&c=technical";

        /// <summary>
        /// Sets a value indicating whether [warn if ie6].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [warn if ie6]; otherwise, <c>false</c>.
        /// </value>
        public bool WarnIfIe6
        {
            set
            {
                _warnIfIE6 = value;
            }
        }

        /// <summary>
        /// Sets the I e6 warning message.
        /// </summary>
        /// <value>
        /// The I e6 warning message.
        /// </value>
        public string IE6WarningMessage
        {
            set
            {
                _ie6WarningMessage = value;
            }
        }

        /// <summary>
        /// Sets the IE6 warning link.
        /// </summary>
        /// <value>The IE6 warning link.</value>
        public string IE6WarningLink
        {
            set
            {
                _ie6WarningLink = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [apply I e6 PNG fix].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [apply I e6 PNG fix]; otherwise, <c>false</c>.
        /// </value>
        public bool ApplyIE6PngFix
        {
            set
            {
                _applyIE6PngFix = value;
            }
        }

        /// <summary>
        /// Sets the I e6 PNG fix clear GIF path.
        /// </summary>
        /// <value>
        /// The I e6 PNG fix clear GIF path.
        /// </value>
        public string IE6PngFixClearGifPath
        {
            set
            {
                _unitClearGifPath = value;
            }
        }

        /// <summary>
        /// EventHandler prior to control rendering
        /// </summary>
        /// <param name="e">The Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (_applyIE6PngFix && HttpContext.Current.Request.Browser.Browser == "IE" && HttpContext.Current.Request.Browser.MajorVersion == 6)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(CECompositeControl), "UintClearGif", string.Format("var clear='{0}';", this.Page.ResolveUrl(_unitClearGifPath)), true);
                this.Page.ClientScript.RegisterClientScriptResource(typeof(CECompositeControl), "Codentia.Common.WebControls.UnitPngFix.js");
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                LogManager.Instance.AddToLog(new UrlAccessMessage(HttpContext.Current.Request));
            }
        }

        /// <summary>
        /// Create child controls
        /// </summary>
        protected override void CreateChildControls()
        {
            if (_warnIfIE6 && HttpContext.Current.Request.Browser.Browser == "IE" && HttpContext.Current.Request.Browser.MajorVersion < 7)
            {
                // user is on IE6, display a warning
                P warning = new P();
                warning.CssClass = "ie6warning";

                if (string.IsNullOrEmpty(_ie6WarningLink))
                {
                    warning.InnerHtml = _ie6WarningMessage;
                }
                else
                {
                    warning.InnerHtml = string.Format("<a href=\"{0}\" target=\"_blank\" title=\"Read more about Internet Explorer 6\">{1}</a>", _ie6WarningLink, _ie6WarningMessage);
                }

                this.Controls.Add(warning);
            }
        }
    }
}
