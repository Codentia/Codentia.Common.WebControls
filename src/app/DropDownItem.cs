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
    /// DropDownItem class
    /// </summary>
    public class DropDownItem
    {
        private string _dropDownId;
        private string _itemValue;
        private string _itemText;
        private string _parentItemValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownItem"/> class.
        /// </summary>
        public DropDownItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DropDownItem"/> class.
        /// </summary>
        /// <param name="dropDownId">The drop down id.</param>
        /// <param name="itemValue">The item value.</param>
        /// <param name="itemText">The item text.</param>
        /// <param name="parentItemValue">The parent item value.</param>
        public DropDownItem(string dropDownId, string itemValue, string itemText, string parentItemValue)
        {
            _dropDownId = dropDownId;
            _itemValue = itemValue;
            _itemText = itemText;
            _parentItemValue = parentItemValue;
        }

        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public string DropDownID
        {
            get
            {
                return _dropDownId;
            }

            set
            {
                _dropDownId = value;
            }
        }

        /// <summary>
        /// Gets or sets ItemValue
        /// </summary>
        public string ItemValue
        {
            get
            {
                return _itemValue;
            }

            set
            {
                _itemValue = value;
            }
        }

        /// <summary>
        /// Gets or sets ItemText
        /// </summary>
        public string ItemText
        {
            get
            {
                return _itemText;
            }

            set
            {
                _itemText = value;
            }
        }

        /// <summary>
        /// Gets or sets ParentItemValue
        /// </summary>
        public string ParentItemValue
        {
            get
            {
                return _parentItemValue;
            }

            set
            {
                _parentItemValue = value;
            }
        }
    }    
}