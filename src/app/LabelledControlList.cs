using System.Web.UI;
using System.Web.UI.WebControls;
using Codentia.Common.WebControls.HtmlElements;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// Control which manifests as a list of labelled controls (similar to form view)
    /// </summary>
    [ToolboxData("<{0}:LabelledControlList runat=server></{0}:LabelledControlList>")]
    public class LabelledControlList : CECompositeControl
    {
        private LabelledControlListItemCollection _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlList"/> class.
        /// </summary>
        public LabelledControlList()
            : this(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelledControlList"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public LabelledControlList(int capacity)
        {
            _items = new LabelledControlListItemCollection(capacity);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public LabelledControlListItemCollection Items
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Add a control to the display
        /// </summary>
        /// <param name="label">Label text to apply</param>
        /// <param name="control">Control to be shown</param>
        public void Add(string label, Control control)
        {
            _items.Add(new LabelledControlListItem(label, control));
        }

        /// <summary>
        /// Update this control based upon any changes to its item collection
        /// </summary>
        public void UpdateChildControls()
        {
            CreateChildControls();
        }

        /// <summary>
        /// Create the child controls which compose this control
        /// </summary>
        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            Ul list = new Ul();
            list.ID = "list";

            for (int i = 0; i < _items.Count; i++)
            {
                Li item = new Li();
                item.ID = string.Format("item{0}", i);

                Label currentLabel = new Label();
                currentLabel.Text = _items[i].Label;

                item.Controls.Add(currentLabel);

                if (_items[i].Control != null)
                {
                    item.Controls.Add(_items[i].Control);
                    currentLabel.AssociatedControlID = _items[i].Control.ID;
                }

                if (_items[i].Validators != null)
                {
                    for (int j = 0; j < _items[i].Validators.Count; j++)
                    {
                        item.Controls.Add((Control)_items[i].Validators[j]);
                    }
                }

                list.Controls.Add(item);
            }

            this.Controls.Add(list);
        }
    }
}
