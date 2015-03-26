using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Enumerated type containing the configuration options for PagingToolbar
    /// </summary>
    public enum PagingToolbarOption
    {
        /// <summary>
        /// Hide the 'first' control
        /// </summary>
        HideFirstPageControl = 1,

        /// <summary>
        /// Hide the 'previous' control
        /// </summary>
        HidePreviousPageControl = 2,

        /// <summary>
        /// Hide the 'next' control
        /// </summary>
        HideNextPageControl = 4,

        /// <summary>
        /// Hide the 'last' control
        /// </summary>
        HideLastPageControl = 8,

        /// <summary>
        /// Base paging routines on an item based set of data rather than page based
        /// </summary>
        IsItemBased = 16,

        /// <summary>
        /// Hide the current page number label
        /// </summary>
        HidePageNumberLabel = 32,

        /// <summary>
        /// Use images for the buttons rather than text
        /// </summary>
        UseImages = 64
    }

    /// <summary>
    /// Paging tool bar (and paging management) control
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:PagingToolbar runat=server></{0}:PagingToolbar>")]
    public class PagingToolbar : CECompositeControl
    {
        private PagingManager _manager;

        private string _imageBasePath;
        private string _imageBeginning;
        private string _imagePrevious;
        private string _imageNext;
        private string _imageEnd;

        private string _textBeginning = "First";
        private string _textPrevious = "Last";
        private string _textNext = "Next";
        private string _textEnd = "Previous";

        private string _pageNumberFormatString = "Page {current} of {total}";

        private ListItemCollection _itemsPerPageOptions;

        private int _options = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingToolbar"/> class.
        /// </summary>
        public PagingToolbar()
        {
            _itemsPerPageOptions = new ListItemCollection();
        }

        /// <summary>
        /// Delegate used in paging related events
        /// </summary>
        /// <param name="sender">PagingToolbar raising the event</param>
        /// <param name="args">Arguments describing the event</param>
        public delegate void PagingOperationEvent(PagingToolbar sender, PagingEventArgs args);

        /// <summary>
        /// Event raised when the 'first' control is clicked
        /// </summary>
        public event PagingOperationEvent FirstLinkClick;

        /// <summary>
        /// Event raised when the 'previous' control is clicked
        /// </summary>
        public event PagingOperationEvent PreviousLinkClick;

        /// <summary>
        /// Event raised when the 'next' control is clicked
        /// </summary>
        public event PagingOperationEvent NextLinkClick;

        /// <summary>
        /// Event raised when the 'last' control is clicked
        /// </summary>
        public event PagingOperationEvent LastLinkClick;

        /// <summary>
        /// Event raised when the number of items shown per page is changed
        /// </summary>
        public event PagingOperationEvent ItemsPerPageChanged;
        
        /// <summary>
        /// Gets or sets the options bitmask
        /// </summary>
        public int Options
        {
            get
            {
                return _options;
            }

            set
            {
                _options = value;

                CreateChildControls();
            }
        }

        /// <summary>
        /// Gets or sets the base image path for control images
        /// </summary>
        public string ImagePath
        {
            get
            {
                return _imageBasePath;
            }

            set
            {
                _imageBasePath = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the First control
        /// </summary>
        public string BeginningControlText
        {
            get
            {
                return _textBeginning;
            }

            set
            {
                _textBeginning = value;
            }
        }

        /// <summary>
        /// Gets or sets the image for the First control
        /// </summary>
        public string BeginningControlImage
        {
            get
            {
                return _imageBeginning;
            }

            set
            {
                _imageBeginning = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the previous control
        /// </summary>
        public string PreviousControlText
        {
            get
            {
                return _textPrevious;
            }

            set
            {
                _textPrevious = value;
            }
        }

        /// <summary>
        /// Gets or sets the image for the previous control
        /// </summary>
        public string PreviousControlImage
        {
            get
            {
                return _imagePrevious;
            }

            set
            {
                _imagePrevious = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the next control
        /// </summary>
        public string NextControlText
        {
            get
            {
                return _textNext;
            }

            set
            {
                _textNext = value;
            }
        }

        /// <summary>
        /// Gets or sets the image for the next control
        /// </summary>
        public string NextControlImage
        {
            get
            {
                return _imageNext;
            }

            set
            {
                _imageNext = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the last control
        /// </summary>
        public string EndControlText
        {
            get
            {
                return _textEnd;
            }

            set
            {
                _textEnd = value;
            }
        }

        /// <summary>
        /// Gets or sets the image for the last control
        /// </summary>
        public string EndControlImage
        {
            get
            {
                return _imageEnd;
            }

            set
            {
                _imageEnd = value;
            }
        }

        /// <summary>
        /// Gets or sets the current page number
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _manager.CurrentPage;
            }

            set
            {
                _manager.CurrentPage = value;

                SetControlStates();
            }
        }

        /// <summary>
        /// Gets or sets the total number of pages
        /// </summary>
        public int TotalPages
        {
            get
            {
                return _manager.TotalPages;
            }

            set
            {
                EnsureChildControls();

                _manager.TotalPages = value;

                SetControlStates();
            }
        }

        /// <summary>
        /// Gets or sets the number of items per page
        /// </summary>
        public int ItemsPerPage
        {
            get
            {
                return _manager.ItemsPerPage;
            }

            set
            {
                EnsureChildControls();

                _manager.ItemsPerPage = value;

                SetControlStates();
            }
        }

        /// <summary>
        /// Gets or sets the total number of items
        /// </summary>
        public int TotalItems
        {
            get
            {
                return _manager.TotalItems;
            }

            set
            {
                EnsureChildControls();

                _manager.TotalItems = value;

                SetControlStates();
            }
        }

        /// <summary>
        /// Gets or sets the items per page options.
        /// </summary>
        /// <value>
        /// The items per page options.
        /// </value>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ListItemCollection ItemsPerPageOptions
        {
            get
            {
                return _itemsPerPageOptions;
            }

            set
            {
                _itemsPerPageOptions = value;

                CreateChildControls();
            }
        }

        /// <summary>
        /// Create the child controls which compose this control
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            _manager = new PagingManager();
            _manager.ID = string.Format("{0}Manager", this.ID);
            _manager.UseItems = this.HasOption(PagingToolbarOption.IsItemBased);
            this.Controls.Add(_manager);

            // first/previous/next/last
            if (HasOption(PagingToolbarOption.UseImages))
            {
                ImageButton beginningLink = new ImageButton();
                beginningLink.ImageUrl = string.Format("{0}{1}", _imageBasePath, _imageBeginning);
                beginningLink.AlternateText = _textBeginning;
                beginningLink.ID = "beginning";
                beginningLink.Click += new ImageClickEventHandler(beginningLink_Click);

                this.Controls.Add(beginningLink);

                ImageButton previousLink = new ImageButton();
                previousLink.ImageUrl = string.Format("{0}{1}", _imageBasePath, _imagePrevious);
                previousLink.AlternateText = _textPrevious;
                previousLink.ID = "previous";
                previousLink.Click += new ImageClickEventHandler(previousLink_Click);

                this.Controls.Add(previousLink);

                ImageButton nextLink = new ImageButton();
                nextLink.ImageUrl = string.Format("{0}{1}", _imageBasePath, _imageNext);
                nextLink.AlternateText = _textNext;
                nextLink.ID = "next";
                nextLink.Click += new ImageClickEventHandler(nextLink_Click);

                this.Controls.Add(nextLink);

                ImageButton endLink = new ImageButton();
                endLink.ImageUrl = string.Format("{0}{1}", _imageBasePath, _imageEnd);
                endLink.AlternateText = _textEnd;
                endLink.ID = "end";
                endLink.Click += new ImageClickEventHandler(endLink_Click);

                this.Controls.Add(endLink);
            }
            else
            {
                LinkButton beginningLink = new LinkButton();
                beginningLink.Text = _textBeginning;
                beginningLink.ID = "beginning";
                beginningLink.Click += new EventHandler(beginningLink_Click);

                this.Controls.Add(beginningLink);

                LinkButton previousLink = new LinkButton();
                previousLink.Text = _textPrevious;
                previousLink.ID = "previous";
                previousLink.Click += new EventHandler(previousLink_Click);

                this.Controls.Add(previousLink);

                LinkButton nextLink = new LinkButton();
                nextLink.Text = _textNext;
                nextLink.ID = "next";
                nextLink.Click += new EventHandler(nextLink_Click);

                this.Controls.Add(nextLink);

                LinkButton endLink = new LinkButton();
                endLink.Text = _textEnd;
                endLink.ID = "end";
                endLink.Click += new EventHandler(endLink_Click);

                this.Controls.Add(endLink);
            }

            // show items per page drop down, if enabled
            if (HasOption(PagingToolbarOption.IsItemBased))
            {
                DropDownList itemsPerPageDropDown = new DropDownList();
                itemsPerPageDropDown.ID = "itemsPerPage";
                itemsPerPageDropDown.AutoPostBack = true;

                for (int i = 0; i < _itemsPerPageOptions.Count; i++)
                {
                    itemsPerPageDropDown.Items.Add(_itemsPerPageOptions[i]);
                }

                if (itemsPerPageDropDown.Items.Count == 0)
                {
                    itemsPerPageDropDown.Visible = false;
                }

                itemsPerPageDropDown.SelectedIndexChanged += new EventHandler(itemsPerPageDropDown_SelectedIndexChanged);

                this.Controls.Add(itemsPerPageDropDown);
            }

            if (!HasOption(PagingToolbarOption.HidePageNumberLabel))
            {
                Label pageNumberLabel = new Label();
                pageNumberLabel.ID = "pageNumber";
                this.Controls.Add(pageNumberLabel);
            }
        }

        /// <summary>
        /// Save the state of this control to page viewstate
        /// </summary>
        /// <returns>The object</returns>
        protected override object SaveViewState()
        {
            object[] state = new object[1];
            state[0] = _options;

            return (object)state;
        }

        /// <summary>
        /// Load the saved state of this control
        /// </summary>
        /// <param name="savedState">state to load</param>
        protected override void LoadViewState(object savedState)
        {
            object[] state = (object[])savedState;

            _options = Convert.ToInt32(state[0]);

            SetControlStates();
        }
        
        private void SetControlStates()
        {
            // set pagenumber label text
            if (!HasOption(PagingToolbarOption.HidePageNumberLabel))
            {
                ((Label)this.FindControl("pageNumber")).Text = _pageNumberFormatString.Replace("{current}", Convert.ToString(_manager.CurrentPage)).Replace("{total}", Convert.ToString(_manager.TotalPages));
            }

            bool navigationControlsVisible = _manager.TotalPages > 1;

            ((WebControl)this.FindControl("beginning")).Visible = navigationControlsVisible & !HasOption(PagingToolbarOption.HideFirstPageControl);
            ((WebControl)this.FindControl("previous")).Visible = navigationControlsVisible & !HasOption(PagingToolbarOption.HidePreviousPageControl);
            ((WebControl)this.FindControl("next")).Visible = navigationControlsVisible & !HasOption(PagingToolbarOption.HideNextPageControl);
            ((WebControl)this.FindControl("end")).Visible = navigationControlsVisible & !HasOption(PagingToolbarOption.HideLastPageControl);

            if (navigationControlsVisible)
            {
                ((WebControl)this.FindControl("beginning")).Enabled = _manager.CurrentPage != 1;
                ((WebControl)this.FindControl("previous")).Enabled = _manager.CurrentPage != 1;
                ((WebControl)this.FindControl("next")).Enabled = _manager.CurrentPage != _manager.TotalPages;
                ((WebControl)this.FindControl("end")).Enabled = _manager.CurrentPage != _manager.TotalPages;
            }

            if (HasOption(PagingToolbarOption.IsItemBased))
            {
                DropDownList itemsPerPageControl = (DropDownList)this.FindControl("itemsPerPage");
                if (itemsPerPageControl != null)
                {
                    int selectedItemIndex = itemsPerPageControl.Items.IndexOf(itemsPerPageControl.Items.FindByValue(Convert.ToString(_manager.ItemsPerPage)));
                    itemsPerPageControl.SelectedIndex = selectedItemIndex;
                }
            }
        }

        private bool HasOption(PagingToolbarOption option)
        {
            return (_options & (int)option) > 0;
        }

        private void beginningLink_Click(object sender, EventArgs e)
        {
            _manager.CurrentPage = 1;
            SetControlStates();

            if (FirstLinkClick != null)
            {
                FirstLinkClick(this, new PagingEventArgs(_manager.CurrentPage, _manager.TotalPages, _manager.ItemsPerPage, PagingOperationType.First));
            }
        }

        private void previousLink_Click(object sender, EventArgs e)
        {
            _manager.CurrentPage--;
            SetControlStates();

            if (PreviousLinkClick != null)
            {
                PreviousLinkClick(this, new PagingEventArgs(_manager.CurrentPage, _manager.TotalPages, _manager.ItemsPerPage, PagingOperationType.Previous));
            }
        }

        private void nextLink_Click(object sender, EventArgs e)
        {
            _manager.CurrentPage++;
            SetControlStates();

            if (NextLinkClick != null)
            {
                NextLinkClick(this, new PagingEventArgs(_manager.CurrentPage, _manager.TotalPages, _manager.ItemsPerPage, PagingOperationType.Next));
            }
        }

        private void endLink_Click(object sender, EventArgs e)
        {
            _manager.CurrentPage = _manager.TotalPages;
            SetControlStates();

            if (LastLinkClick != null)
            {
                LastLinkClick(this, new PagingEventArgs(_manager.CurrentPage, _manager.TotalPages, _manager.ItemsPerPage, PagingOperationType.Last));
            }
        }

        private void itemsPerPageDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            _manager.ItemsPerPage = Convert.ToInt32(((DropDownList)sender).SelectedValue);
            SetControlStates();

            if (ItemsPerPageChanged != null)
            {
                ItemsPerPageChanged(this, new PagingEventArgs(_manager.CurrentPage, _manager.TotalPages, _manager.ItemsPerPage, PagingOperationType.ItemsPerPageChange));
            }
        }
    }
}