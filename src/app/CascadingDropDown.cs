using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Codentia.Common.Helper;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// CascadingDropDown control
    /// </summary>
    public class CascadingDropDown : CECompositeControl
    {
        private DropDownCollection _ddls = new DropDownCollection();
        private DropDownItemCollection _ddlItems = new DropDownItemCollection();
        private Xml _xmlArea = new Xml();
        private LabelledControlList _lci = new LabelledControlList();

        /// <summary>
        /// Initializes a new instance of the <see cref="CascadingDropDown"/> class.
        /// </summary>
        public CascadingDropDown()
        {
        }

        /// <summary>
        /// Gets or sets the drop downs.
        /// </summary>
        /// <value>
        /// The drop downs.
        /// </value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DropDownCollection DropDowns
        {
            get
            {
                return _ddls;
            }

            set
            {
                _ddls = value;

                if (_ddls.Count > this._lci.Items.Count)
                {
                    for (int i = this._lci.Items.Count; i < _ddls.Count; i++)
                    {
                        this._lci.Items.Add(_ddls[i].ToListDropDown());
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the drop down items.
        /// </summary>
        /// <value>
        /// The drop down items.
        /// </value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DropDownItemCollection DropDownItems
        {
            get
            {
                return _ddlItems;
            }

            set
            {
                _ddlItems = value;
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            base.CreateChildControls();
            this.Controls.Add(_lci);

            // create xml and populate first ddl
            List<DropDownItem> ddiCheckList = new List<DropDownItem>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<cddl></cddl>");
            XmlElement root = xmlDoc.DocumentElement;

            List<string> ddlLevels = new List<string>();

            // Populate top level ddl
            for (int i = 0; i < _ddlItems.Count; i++)
            {
                // Populate top level ddl
                DropDownItem ddi = _ddlItems[i];

                if (string.IsNullOrEmpty(ddi.ParentItemValue))
                {
                    ListItem li = new ListItem();
                    li.Text = ddi.ItemText;
                    li.Value = ddi.ItemValue;
                    _ddls[0].DropDownList.Items.Add(li);
                    ddiCheckList.Add(ddi);

                    if (!ddlLevels.Contains(_ddls[0].ID))
                    {
                        ddlLevels.Add(_ddls[0].ID);
                    }
                }
                else
                {
                    // check whether any top level drop downs have been defined
                    if (ddlLevels.Count == 0)
                    {
                        throw new Exception("There are no top level DropDowns defined");
                    }

                    // check drop down exists
                    bool _ddlNamecheck = false;
                    for (int j = 0; j < _ddls.Count; j++)
                    {
                        if (_ddls[j].ID == ddi.DropDownID)
                        {
                            _ddlNamecheck = true;
                            break;
                        }
                    }

                    if (!_ddlNamecheck)
                    {
                        throw new Exception(string.Format("DropDownItem: {0} has an non-existant DropDownID: {1}", ddi.ItemText, ddi.DropDownID));
                    }

                    // create main container node
                    XmlNode node = XMLHelper.CreateElementNode(xmlDoc, "ddlitem");
                    
                    // create child attributes
                    XMLHelper.AppendAttributeNode(xmlDoc, node, "ddlid", ddi.DropDownID);
                    XMLHelper.AppendAttributeNode(xmlDoc, node, "ddltext", ddi.ItemText);
                    XMLHelper.AppendAttributeNode(xmlDoc, node, "ddlvalue", ddi.ItemValue);
                    XMLHelper.AppendAttributeNode(xmlDoc, node, "ddlparentvalue", ddi.ParentItemValue);
                    root.AppendChild(node);
                }
            }

            XmlNode nodeSummary = XMLHelper.CreateElementNode(xmlDoc, "summary");
            XMLHelper.AppendAttributeNode(xmlDoc, nodeSummary, "ddlcount", _ddls.Count.ToString());
            root.AppendChild(nodeSummary);

            XmlNode nodeSummaryByName = XMLHelper.CreateElementNode(xmlDoc, "summarybyname");

            for (int k = 0; k < _ddls.Count; k++)
            {
                XMLHelper.AppendAttributeNode(xmlDoc, nodeSummaryByName, _ddls[k].DropDownList.ID, k.ToString());
            }

            root.AppendChild(nodeSummaryByName);

            XmlNode nodeSummaryByPos = XMLHelper.CreateElementNode(xmlDoc, "summarybypos");

            for (int l = 0; l < _ddls.Count; l++)
            {
                XMLHelper.AppendAttributeNode(xmlDoc, nodeSummaryByPos, string.Format("pos{0}", l), _ddls[l].DropDownList.ID);
            }

            root.AppendChild(nodeSummaryByPos);

            XmlNode nodeSummaryOfChildren = XMLHelper.CreateElementNode(xmlDoc, "summaryofchildren");

            for (int m = 0; m < _ddls.Count; m++)
            {
                if (!string.IsNullOrEmpty(_ddls[m].ParentDropDownId))
                {
                    XMLHelper.AppendAttributeNode(xmlDoc, nodeSummaryOfChildren, _ddls[m].ParentDropDownId, _ddls[m].DropDownList.ID);
                }
            }

            root.AppendChild(nodeSummaryOfChildren);

            Panel p = new Panel();
            p.Style.Add("display", "none");
            p.ID = "X";
            Xml xmlArea = new Xml();
            xmlArea.DocumentContent = xmlDoc.InnerXml;
            p.Controls.Add(xmlArea);
            this.Controls.Add(p);
        }

        /// <summary>
        /// Handle control pre-render (before html generation, after all events)
        /// </summary>
        /// <param name="e">Event Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string a = _ddls[0].DropDownList.ClientID;
            if (this.Page != null)
            {
                this.Page.ClientScript.RegisterClientScriptResource(typeof(CascadingDropDown), "Codentia.Common.WebControls.CascadingDropDown.js");
            }
        }

        /// <summary>
        /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl"/> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter"/> object, for display on the client.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            string script = string.Format("RefreshDDLs(document.getElementById('{0}'), '{1}');", _ddls[0].DropDownList.ClientID, _ddls[0].DropDownList.ID);

            if (this.Page != null)
            {
                this.Page.ClientScript.RegisterStartupScript(typeof(CascadingDropDown), "SU", script, true);
            }
        }
    }
}
