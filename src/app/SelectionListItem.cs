using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codentia.Common.WebControls
{
    /// <summary>
    /// An item in a selection list
    /// </summary>
    public class SelectionListItem
    {
        private string _description;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionListItem"/> class.
        /// </summary>
        public SelectionListItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionListItem"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public SelectionListItem(string description)
        {
            _description = description;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }
    }
}
