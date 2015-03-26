using System;
using System.Configuration;
using System.Text;
using System.Web.UI;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// This control acts as a passive interface for Google Analytics, permitting us to add the urchin tracker to pages with one line, rather than
    /// copying and pasting a block of code each time.
    /// </summary>
    [ToolboxData("<{0}:GoogleAnalyticsTracker runat=server></{0}:ContactUs>")]
    public class GoogleAnalyticsTracker : CECompositeControl
    {
        /// <summary>
        /// EventHandler prior to control rendering
        /// </summary>
        /// <param name="e">The Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["GoogleAnalyticsId"]))
            {
                StringBuilder analyticsHtml = new StringBuilder();
                analyticsHtml.Append("  var _gaq = _gaq || [];");
                analyticsHtml.AppendFormat("  _gaq.push(['_setAccount', '{0}']);", ConfigurationManager.AppSettings["GoogleAnalyticsId"]);
                analyticsHtml.Append("  _gaq.push(['_trackPageview']);");
                analyticsHtml.Append("  (function() {");
                analyticsHtml.Append("var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
                analyticsHtml.Append("ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
                analyticsHtml.Append("var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
                analyticsHtml.Append("})();");

                // register Google Analytics include
                Page.ClientScript.RegisterClientScriptBlock(typeof(GoogleAnalyticsTracker), "GoogleAnalyticsTrackerExec", analyticsHtml.ToString(), true);
            }
        }
    }
}