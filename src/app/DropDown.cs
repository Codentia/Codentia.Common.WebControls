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
    /// DropDown class
    /// </summary>
    public class DropDown
    {
        private string _id;
        private string _labelText;
        private DropDownList _ddl;
        private string _parentDropDownId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDown"/> class.
        /// </summary>
        public DropDown()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDown"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="labelText">The label text.</param>
        /// <param name="parentDropDownId">The parent drop down id.</param>
        public DropDown(string id, string labelText, string parentDropDownId)
        {
            _id = id;
            _labelText = labelText;
            _parentDropDownId = parentDropDownId;
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public string ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>
        /// The label text.
        /// </value>
        public string LabelText
        {
            get
            {
                return _labelText;
            }

            set
            {
                _labelText = value;
            }
        }

        /// <summary>
        /// Gets the drop down list.
        /// </summary>
        public DropDownList DropDownList
        {
            get
            {
                return _ddl;
            }
        }

        /// <summary>
        /// Gets or sets the parent drop down id.
        /// </summary>
        /// <value>
        /// The parent drop down id.
        /// </value>
        public string ParentDropDownId
        {
            get
            {
                return _parentDropDownId;
            }

            set
            {
                _parentDropDownId = value;
            }
        }

        /// <summary>
        /// Toes the list drop down.
        /// </summary>
        /// <returns>LabelledControlListItem reference</returns>
        public LabelledControlListItem ToListDropDown()
        {
            LabelledControlListItem item = new LabelledControlListItem();
            item.Label = _labelText;

            // create control and add it
            _ddl = new DropDownList();

            _ddl.ID = string.Format("ddl_{0}", _id);
            _ddl.Attributes.Add("onChange", string.Format("RefreshDDLs(this, '{0}')", _ddl.ID));

            WebControl dropDownControl = _ddl;
            item.Control = dropDownControl;

            return item;
        }
    }
}