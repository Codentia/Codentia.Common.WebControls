using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.WebControls.HtmlElements;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// SelectionList control
    /// </summary>
    public class SelectionList : CECompositeControl
    {
        private string _candidateTitle;
        private string _selectionTitle;
        private string _saveLinkText = "Save";        

        private SelectionListItemCollection _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionList"/> class.
        /// </summary>
        public SelectionList()
        {
            _items = new SelectionListItemCollection();
        }

        /// <summary>
        /// Handler for SelectionList event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="indexes">The indexes.</param>
        public delegate void SelectionListEventHandler(SelectionList sender, int[] indexes);

        /// <summary>
        /// Occurs when [selected index changed].
        /// </summary>
        public event SelectionListEventHandler SelectedIndexChanged;
        
        /// <summary>
        /// Occurs when [selection cancelled].
        /// </summary>
        public event SelectionListEventHandler SelectionCancelled;

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SelectionListItemCollection Items
        {
            get
            {
                return _items;
            }

            set
            {
                _items = value;
            }
        }

        /// <summary>
        /// Sets the selection title.
        /// </summary>
        /// <value>The selection title.</value>
        public string SelectionTitle
        {
            set
            {
                _selectionTitle = value;
            }
        }

        /// <summary>
        /// Sets the candidate title.
        /// </summary>
        /// <value>The candidate title.</value>
        public string CandidateTitle
        {
            set
            {
                _candidateTitle = value;
            }
        }

        /// <summary>
        /// Sets the save link text.
        /// </summary>
        /// <value>The save link text.</value>
        public string SaveLinkText
        {
            set
            {
                _saveLinkText = value;
            }
        }

        /// <summary>
        /// Handle the PreRender event
        /// </summary>
        /// <param name="e">The Arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.Page != null)
            {
                CECompositeControl.RegisterClientScriptResource(this, typeof(SelectionList), "Codentia.Common.WebControls.SelectionList.js");

                Control addList = this.FindChildControl("add");
                Control optionsList = this.FindChildControl("optionsList");
                Control selection = this.FindChildControl("selection");

                string definitions = "var selectedIndexes = ''; var selectedValues = ''; var addList = document.getElementById('" + addList.ClientID + "'); var optionsList = document.getElementById('" + optionsList.ClientID + "'); var selection = document.getElementById('" + selection.ClientID + "');";
                CECompositeControl.RegisterStartupScript(this, typeof(SelectionList), "Globals", definitions, true);

                string registerClickHandlers = "for(i=0;i<optionsList.childNodes.length;i++) { optionsList.childNodes[i].childNodes[0].onclick=moveFromCandidatesToChanges; }";
                CECompositeControl.RegisterStartupScript(this, typeof(SelectionList), "RegisterClickHandlers", registerClickHandlers, true);
            }
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            HiddenField selection = new HiddenField();
            selection.ID = "selection";
            selection.Value = string.Empty;

            this.Controls.Add(selection);

            // available items (not chosen)
            Div candidates = new Div();

            if (!string.IsNullOrEmpty(_candidateTitle))
            {
                Label candidateLabel = new Label();
                candidateLabel.Text = _candidateTitle;
                candidates.Controls.Add(candidateLabel);
                candidates.Controls.Add(new Br());
            }

            TextBox filterBox = new TextBox();
            filterBox.ID = "filter";
            filterBox.Attributes.Add("onkeyup", "filterCandidates(this.value);");

            Label filterLabel = new Label();
            filterLabel.Text = "Filter:";
            filterLabel.AssociatedControlID = "filter";

            candidates.Controls.Add(filterLabel);
            candidates.Controls.Add(filterBox);

            Ul optionsList = new Ul();
            optionsList.ID = "optionsList";

            for (int i = 0; i < _items.Count; i++)
            {
                Li item = new Li();

                HyperLink itemLink = new HyperLink();
                itemLink.ID = string.Format("_{0}", i.ToString());
                itemLink.NavigateUrl = "#";
                itemLink.Text = _items[i].Description;

                item.Controls.Add(itemLink);

                optionsList.Controls.Add(item);
            }

            candidates.Controls.Add(optionsList);
            this.Controls.Add(candidates);

            // changes
            Div selections = new Div();

            if (!string.IsNullOrEmpty(_candidateTitle))
            {
                Label selectionLabel = new Label();
                selectionLabel.Text = _selectionTitle;
                selections.Controls.Add(selectionLabel);
            }

            Ul addList = new Ul();
            addList.ID = "add";
            selections.Controls.Add(addList);
            this.Controls.Add(selections);

            P linkContainer = new P();

            LinkButton cancelLink = new LinkButton();
            cancelLink.ID = "cancel";
            cancelLink.Text = "Cancel";
            cancelLink.Click += new EventHandler(cancelLink_Click);
            linkContainer.Controls.Add(cancelLink);

            LinkButton saveLink = new LinkButton();
            saveLink.ID = "save";
            saveLink.Text = _saveLinkText;
            saveLink.Click += new EventHandler(saveLink_Click);
            linkContainer.Controls.Add(saveLink);

            this.Controls.Add(linkContainer);
        }

        /// <summary>
        /// Handles the Click event of the cancelLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void cancelLink_Click(object sender, EventArgs e)
        {
            HiddenField hidden = (HiddenField)this.FindChildControl("selection");
            hidden.Value = string.Empty;

            if (SelectionCancelled != null)
            {
                SelectionCancelled(this, null);
            }
        }

        /// <summary>
        /// Handles the Click event of the saveLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void saveLink_Click(object sender, EventArgs e)
        {
            HiddenField hidden = (HiddenField)this.FindChildControl("selection");
            string value = hidden.Value;
            hidden.Value = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                if (SelectedIndexChanged != null)
                {
                    string[] parts = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    int[] indexes = new int[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                    {
                        indexes[i] = Convert.ToInt32(parts[i]);
                    }

                    Array.Sort(indexes);

                    SelectedIndexChanged(this, indexes);
                }
            }
            else
            {
                cancelLink_Click(sender, e);
            }
        }
    }
}
