using System.Web;
using System.Web.Security;

namespace Codentia.Common.WebControls.Handlers
{
    /// <summary>
    /// Generic Handler base class containing utility methods and performing basic functions otherwise repeated.
    /// The AuthenticatedHandler requires the current user session to be authenticated via membership - if not it 
    /// will call FormsAuthentication to forward them to the login page.
    /// </summary>
    public class CEAuthenticatedHandler : CEGenericHandler
    {
        #region IHttpHandler Members

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void ProcessRequest(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        #endregion
    }
}
