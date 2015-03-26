using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Management control for Lightboxing
    /// </summary>
    public class LightboxManager : CECompositeControl
    {
        private string _lightboxLabel = "Product";
        private string _loadingImagePath = "secure/images/lightbox/loading.gif";
        private string _secureLoadingImagePath = "images/lightbox/loading.gif";
        private string _closeImagePath = "secure/images/lightbox/closelabel.gif";
        private string _secureCloseImagePath = "images/lightbox/closelabel.gif";

        /// <summary>
        /// Initializes a new instance of the <see cref="LightboxManager"/> class.
        /// </summary>
        public LightboxManager()
        {
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label
        {
            get
            {
                return _lightboxLabel;
            }

            set
            {
                _lightboxLabel = value;
            }
        }

        /// <summary>
        /// Sets the loading image.
        /// </summary>
        /// <value>The loading image.</value>
        public string LoadingImage
        {
            set
            {
                _loadingImagePath = value;
            }
        }

        /// <summary>
        /// Sets the secure loading image.
        /// </summary>
        /// <value>The secure loading image.</value>
        public string SecureLoadingImage
        {
            set
            {
                _loadingImagePath = value;
            }
        }

        /// <summary>
        /// Sets the close image.
        /// </summary>
        /// <value>The close image.</value>
        public string CloseImage
        {
            set
            {
                _closeImagePath = value;
            }
        }

        /// <summary>
        /// Sets the secure close image.
        /// </summary>
        /// <value>The secure close image.</value>
        public string SecureCloseImage
        {
            set
            {
                _secureCloseImagePath = value;
            }
        }

        /// <summary>
        /// Add a Lightbox attribute to a given WebControl
        /// </summary>
        /// <param name="target">Control to add attribute to</param>
        public void AddLightboxToControl(WebControl target)
        {
            target.Attributes.Add("rel", "lightbox");
        }

        /// <summary>
        /// Add a Lightbox attribute to a given WebControl
        /// </summary>
        /// <param name="target">Control to add attribute to</param>
        /// <param name="lightboxGroup">Group of Lightboxes this control should belong to</param>
        public void AddLightboxToControl(WebControl target, string lightboxGroup)
        {
            target.Attributes.Add("rel", string.Format("lightbox[{0}]", lightboxGroup));
        }

        /// <summary>
        /// Gets the lightbox link.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="target">The target.</param>
        /// <param name="text">The text.</param>
        /// <returns>The HtmlAnchor</returns>
        public HtmlAnchor GetLightboxLink(string id, string target, string text)
        {
            HtmlAnchor link = new HtmlAnchor();
            link.ID = id;
            link.HRef = target;
            link.Attributes.Add("rel", "lightbox");
            link.InnerText = text;

            return link;
        }

        /// <summary>
        /// Gets the lightbox link.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="target">The target.</param>
        /// <param name="text">The text.</param>
        /// <param name="title">The title.</param>
        /// <returns>The HtmlAnchor</returns>
        public HtmlAnchor GetLightboxLink(string id, string target, string text, string title)
        {
            HtmlAnchor link = GetLightboxLink(id, target, text);
            link.Attributes.Add("title", title);

            return link;
        }

        /// <summary>
        /// Get a new LightBox link
        /// </summary>
        /// <param name="id">Id of link</param>
        /// <param name="target">Href Link should point to</param>
        /// <param name="text">Text (inner) of link</param>
        /// <param name="title">Caption text of link</param>
        /// <param name="lightboxGroup">Group to which link should belong</param>
        /// <returns>The HtmlAnchor</returns>
        public HtmlAnchor GetLightboxLink(string id, string target, string text, string title, string lightboxGroup)
        {
            HtmlAnchor link = GetLightboxLink(id, target, text, title);
            link.Attributes.Add("rel", string.Format("lightbox[{0}]", lightboxGroup));

            return link;
        }

        /// <summary>
        /// Register scripts and populate any child controls
        /// </summary>
        /// <param name="e">The Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.Page != null)
            {
                Page.ClientScript.RegisterClientScriptResource(typeof(LightboxManager), "Codentia.Common.WebControls.LightboxManager_prototype.js");
                Page.ClientScript.RegisterClientScriptResource(typeof(LightboxManager), "Codentia.Common.WebControls.LightboxManager_scriptaculous.js");
                Page.ClientScript.RegisterClientScriptResource(typeof(LightboxManager), "Codentia.Common.WebControls.LightboxManager_builder.js");
                Page.ClientScript.RegisterClientScriptResource(typeof(LightboxManager), "Codentia.Common.WebControls.LightboxManager_effects.js");

                // write out parameters
                string parameters = string.Format(
                    "LightboxOptions = Object.extend({{" +
                        "fileLoadingImage:        (window.location.toString().indexOf('https') == 0 ? '{2}' : '{1}'),  " +
                        "fileBottomNavCloseImage: (window.location.toString().indexOf('https') == 0 ? '{4}' : '{3}')," +
                        "overlayOpacity: 0.8," +   // controls transparency of shadow overlay
                        "animate: true," +         // toggles resizing animations
                        "resizeSpeed: 7," +       // controls the speed of the image resizing animations (1=slowest and 10=fastest)
                        "borderSize: 10," +        // if you adjust the padding in the CSS, you will need to update this variable
                        "labelImage: '{0}'," + // When grouping images this is used to write: Image # of #.
                        "labelOf: 'of'" +
                        " }}, window.LightboxOptions || {{}});",
                        _lightboxLabel,
                        _loadingImagePath,
                        _secureLoadingImagePath,
                        _closeImagePath,
                        _secureCloseImagePath);

                Page.ClientScript.RegisterClientScriptBlock(typeof(LightboxManager), "LightboxParameters", parameters, true);

                Page.ClientScript.RegisterClientScriptResource(typeof(LightboxManager), "Codentia.Common.WebControls.LightboxManager_lightbox.js");
            }
        }
    }
}
