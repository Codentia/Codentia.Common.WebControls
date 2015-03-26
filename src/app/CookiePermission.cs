using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.Helper;
using Codentia.Common.Logging;
using Codentia.Common.Logging.BL;
using Codentia.Common.Net;
using Codentia.Common.WebControls.HtmlElements;
using Codentia.Common.WebControls.Validators;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// CookiePermission control
    /// </summary>
    [ToolboxData("<{0}:CookiePermission runat=server></{0}:CookiePermission>")]
    public class CookiePermission : CECompositeControl
    {
        private ITemplate _messageTemplate;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [TemplateContainer(typeof(RegionTemplate))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        public virtual ITemplate Message
        {
            get
            {
                return _messageTemplate;
            }

            set
            {
                _messageTemplate = value;
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // check if cookies have already been accepted
            string cookieName = string.Format("AcceptCookies_{0}", SiteHelper.SiteEnvironment);

            HttpCookie cookie = Context.Request.Cookies[cookieName];

            if (cookie == null)
            {
                if (_messageTemplate != null)
                {
                    RegionTemplate messageTemplate = new RegionTemplate();
                    messageTemplate.ID = "messageTemplate";
                    _messageTemplate.InstantiateIn(messageTemplate);
                    this.Controls.Add(messageTemplate);
                }

                P controlBar = new P();

                CheckBox cb = new CheckBox();
                cb.ID = "permissionBox";
                cb.Text = "I understand and agree.";
                cb.TextAlign = TextAlign.Right;
                controlBar.Controls.Add(cb);

                Button btn = new Button();
                btn.Text = "Continue";
                btn.OnClientClick = "return CookiePermission_AcceptCookies();";
                controlBar.Controls.Add(btn);

                this.Controls.Add(controlBar);

                this.Page.ClientScript.RegisterClientScriptBlock(typeof(CookiePermission), "vars", string.Format("var _cookiePermissionId = '{0}'; var _cookiePermissionName = '{1}'; var _cookiePermissionCheckBoxId='{2}';", this.ClientID, cookieName, cb.ClientID), true);
                this.Page.ClientScript.RegisterClientScriptResource(typeof(CookiePermission), "Codentia.Common.WebControls.CookiePermission.js");
            }
            else
            {
                this.Visible = false;
            }
        }
    }
}
